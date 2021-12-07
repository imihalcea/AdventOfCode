using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using static _2020.Tools;

namespace _2020
{
    public class Day20
    {
        public static Dictionary<long, TileOld> Dataset(string filePath) =>
            File.ReadAllText(filePath).Split(Environment.NewLine + Environment.NewLine).Select(
                tileDef =>
                {
                    var lines = tileDef.Split(Environment.NewLine);
                    var id = int.Parse(lines[0].Substring(5, 4));
                    var pixels = lines[1..].Select(line => line.Select(c => c == '#' ? 1 : 0).ToArray()).ToArray();
                    return new TileOld(id, pixels);
                }).ToDictionary(t=>t.Id, t=>t);

        public static long Part1(Dictionary<long,TileOld> tiles)
        {
            var tileLinks = ComputeLinks(tiles);
            return tileLinks.Where(kv => kv.Value.Count() == 2).Select(kv => kv.Key).Aggregate(1L, (acc, n) => acc * n);
        }
        
        public static TileOld[][] ReconstructImage(Dictionary<long,TileOld> tiles, Dictionary<long, HashSet<long>> links)
        {
            var size = (int)Math.Sqrt(links.Keys.Count);
            var tilesReconstructed = new TileOld[size][];
            var headTile = PickTopLeftCorner(tiles,links);
            var linkedTiles = new HashSet<TileOld>();
            BuildLinks(headTile, tiles, links, linkedTiles);

            var currentTile = headTile;
            tilesReconstructed[0] = new TileOld[size];
            int row = 0, col = 0;
            while (currentTile!=null)
            {
                tilesReconstructed[row][col] = currentTile;
                currentTile = linkedTiles.Single(t=>t==currentTile.LinkRight);
                col += 1;
                if (currentTile == null)
                {
                    headTile = linkedTiles.Single(t=>t==headTile!.LinkBottom);
                    currentTile = headTile;
                    if(currentTile==null) break; 
                    row += 1;
                    tilesReconstructed[row] = new TileOld[size];
                    col = 0;
                }
            }

            return tilesReconstructed;
        }

 
        private static void BuildLinks(TileOld current,Dictionary<long,TileOld> tiles, Dictionary<long, HashSet<long>> links , HashSet<TileOld> marked)
        {
            if (marked.Count == links.Count || marked.Contains(current)) return;
            marked.Add(current);
            foreach (var linkId in links[current.Id])
            {
                var link = tiles[linkId];
                if(current.Links.Any(l=>l.Id==link.Id)) continue;
                foreach (var tile in link.PossibleTiles())
                {
                    var commonBorder = current.BordersHashes.Intersect(tile.BordersHashes).SingleOrDefault();
                    if(commonBorder == 0) continue;
                    var currentEdge = current.GetEdge(commonBorder);
                    var tileEdge = tile.GetEdge(commonBorder);
                    var canLink = (currentEdge, tileEdge) switch
                    {
                        (Edge.Bottom, Edge.Top) => true,
                        (Edge.Top, Edge.Bottom) => true,
                        (Edge.Right, Edge.Left) => true,
                        (Edge.Left, Edge.Right) => true,
                        _ => false
                    };
                    if (canLink)
                    {
                        current.Link(currentEdge,tile);
                        tile.Link(tileEdge, current);
                        BuildLinks(tile,tiles, links,marked);
                    }
                }
            }
        }

        public static TileOld PickTopLeftCorner(Dictionary<long,TileOld> tiles, Dictionary<long, HashSet<long>> links)
        {
            var cornerTiles = links.Where(kv => kv.Value.Count() == 2).Select(kv => kv.Key).ToArray();
            foreach (var cornerTileId in cornerTiles)
            {
                var cornerTile = tiles[cornerTileId];
                var (l1,l2) = (tiles[links[cornerTileId].First()],tiles[links[cornerTileId].Last()]);
                var l1Borders = l1.PossibleTiles().SelectMany(l => l.BordersHashes).ToHashSet();
                var l2Borders = l2.PossibleTiles().SelectMany(l => l.BordersHashes).ToHashSet();
                
                var allBorders = cornerTile.AllBorders.Union(l1.AllBorders).Union(l2.AllBorders).GroupBy(b => b.Hash, b => b.Edge); 
                foreach (var tile in cornerTile.PossibleTiles())
                {
                    var bottomRightBorders = tile.AllBorders.Where(b=>b.Edge==Edge.Bottom || b.Edge==Edge.Right).Select(b=>b.Hash).ToArray();
                    var topLeftBorders = tile.AllBorders.Where(b=>b.Edge==Edge.Top || b.Edge==Edge.Left).Select(b=>b.Hash).ToArray();
                    var isTopLeft = bottomRightBorders.Intersect(l1Borders).Any() &&
                                    bottomRightBorders.Intersect(l2Borders).Any() &&
                                    !topLeftBorders.Intersect(l1Borders).Any() &&
                                    !topLeftBorders.Intersect(l2Borders).Any();
                            
                            
                    if(isTopLeft)
                        return tile;
                }
            }
            throw new Exception();
        }

        public static Dictionary<long, HashSet<long>> ComputeLinks(Dictionary<long,TileOld> tiles)
        {
            var commonBorders = new ConcurrentDictionary<long, HashSet<long>>();
            foreach (var t in tiles.Values)
            foreach (var tp in t.PossibleTiles())
            foreach (var border in tp.AllBorders)
            {
                commonBorders.AddOrUpdate(border.Hash, _ => new HashSet<long>() {tp.Id}, (_, v) => new HashSet<long>(v) {tp.Id});
            }

            var links = commonBorders.Where(kv => kv.Value.Count() == 2)
                .Select(kv => new {borderHash = kv.Key, tiles1 = kv.Value.First(), title2 = kv.Value.Last()});

            var tileLinks = new Dictionary<long, HashSet<long>>();
            foreach (var tile in tiles.Keys)
            {
                tileLinks[tile] = new HashSet<long>();
            }

            foreach (var link in links)
            {
                var (h, t1, t2) = (link.borderHash, link.tiles1, link.title2);
                tileLinks[t1].Add(t2);
                tileLinks[t2].Add(t1);
            }

            return tileLinks;
        }


        public class TileOld : IEquatable<TileOld>
        {
            public long Id=> _tileNumber;

            private readonly int[][] _pixels;
            private readonly int _orientation;
            private Dictionary<long, Border> _borders;
            private readonly TileOld[] _links;
            private readonly long _tileNumber;

            public IList<TileOld> Links => _links.Where(l=>l!=null).ToList();

            public TileOld(long tileNumber, int[][] pixels, int orientation = 0)
            {
                _tileNumber = tileNumber;
                _pixels = pixels;
                _orientation = orientation;
                int[][] pixelsTransposed = Transpose(_pixels);
                _links = new TileOld[4];
                /*AllBorders = new[]
                {
                    new Border(this,pixelsTransposed[0], Edge.Left),
                    new Border(tileNumber,_pixels[0], Edge.Top),
                    new Border(tileNumber,pixelsTransposed[^1], Edge.Right),
                    new Border(tileNumber, _pixels[^1], Edge.Bottom)
                };*/
                _borders = AllBorders.ToDictionary(b => b.Hash, b => b);
                BordersHashes = _borders.Keys.ToArray();
            }

            public void Link(Edge edge, TileOld tileOld)
            {
                _links[(int) edge] = tileOld;
            }

            public Border[] AllBorders { get; }

            public Edge GetEdge(long borderHash) => _borders[borderHash].Edge;

            public long[] BordersHashes { get; } 
  
            public TileOld? LinkRight => _links[(int) Edge.Right];
            public TileOld? LinkBottom => _links[(int) Edge.Bottom];
            
            


            public TileOld[] PossibleTiles()
            {
                //move elements around 8 axes of symmetry 
                var t0 = _pixels;    
                var t1 = Flip(t0);
                var t2 = Rotate(t1);
                var t3 = Flip(t2);
                var t4 = Rotate(t0);
                var t5 = Rotate(t4);
                var t6 = Flip(t5);
                var t7 = Rotate(t4);
                return new[] {t0, t1, t2, t3, t4, t5, t6, t7}.Select(
                    (px, orientation) => new TileOld(_tileNumber, px, orientation)).ToArray();
            }
            public override string ToString()
            {
                return $"{_tileNumber} -> {_orientation}";
            }

            public bool Equals(TileOld? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return _tileNumber==other._tileNumber;
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((TileOld) obj);
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

            public static bool operator ==(TileOld? left, TileOld? right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(TileOld? left, TileOld? right)
            {
                return !Equals(left, right);
            }
        }
    }
}