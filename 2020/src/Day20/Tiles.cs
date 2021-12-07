using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace _2020
{
    public class Tiles
    {
        private Dictionary<long, Tile> _masters;

        public Tiles(Dictionary<long, Tile> masters)
        {
            _masters = masters;
            Masters = masters.Values.ToImmutableHashSet();
            ComputeLinks();
        }
        private void ComputeLinks()
        {
            var commonBorders = new ConcurrentDictionary<long, HashSet<long>>();
            foreach (var t in Masters)
            foreach (var variant in t.Variants)
            foreach (var border in variant.AllBorders)
                commonBorders.AddOrUpdate(border.Hash, _ => new HashSet<long>() {variant.Id}, (_, v) => new HashSet<long>(v) {variant.Id});

            var links = commonBorders.Where(kv => kv.Value.Count() == 2)
                .Select(kv => (kv.Value.First(), kv.Value.Last()));

            foreach (var (idTile1, idTile2) in links)
            {
                this[idTile1].AddNeighbor(this[idTile2]);
                this[idTile2].AddNeighbor(this[idTile1]);
            }
        }

        public Tile PutInPlaceTopLeftCorner()
        {
            foreach (var corner in Corners)
            {
                var (l1,l2) = (corner.Neighbors[0], corner.Neighbors[1]);
                var l1Borders = l1.Variants.SelectMany(l => l.AllBorders).ToHashSet();
                var l2Borders = l1.Variants.SelectMany(l => l.AllBorders).ToHashSet();
                var cornerVariants = corner.Variants;
                foreach (var cornerVariant in cornerVariants)
                {
                    var bottomRightBorders = cornerVariant.AllBorders.Where(b=>b.Edge==Edge.Bottom || b.Edge==Edge.Right).Select(b=>b).ToArray();
                    var topLeftBorders = cornerVariant.AllBorders.Where(b=>b.Edge==Edge.Top || b.Edge==Edge.Left).Select(b=>b).ToArray();
                    var isTopLeft = bottomRightBorders.Intersect(l1Borders).Any() &&
                                    bottomRightBorders.Intersect(l2Borders).Any() &&
                                    !topLeftBorders.Intersect(l1Borders).Any() &&
                                    !topLeftBorders.Intersect(l2Borders).Any();


                    if (isTopLeft)
                    {
                        corner.ChangeOrientation(cornerVariant);
                        CreateLink(corner, corner.Neighbors[0]);
                        CreateLink(corner, corner.Neighbors[1]);
                        return corner;
                    }
                }
                
            }

            throw new Exception();
        }

        
        public Tile[][] ReconstructImage()
        {
            var result = new Tile[3][];
            var topLeft = PutInPlaceTopLeftCorner();
            var current = topLeft.NeighborRight;
            var nextRowLeft = topLeft.NeighborBottom;
            while (current!=null)
            {
                var vr = Masters.SelectMany(m => m.Variants).SingleOrDefault(v => v.HasBorder(current.BorderRight.Hash));
                var vb = Masters.SelectMany(m => m.Variants).SingleOrDefault(v => v.HasBorder(current.BorderBottom.Hash));
                if (vr != null)
                {
                    CreateLink(current, vr);
                }
                else
                {
                    
                }
                CreateLink(current,vb);
            }

            return default;
        }

        public Tile ReconstructImage2()
        {
            var topLeft = PutInPlaceTopLeftCorner();
            var queue = new Queue<(Tile,Tile)>(new []{(topLeft,topLeft.Neighbors[0]),(topLeft,topLeft.Neighbors[1])});
            var marked = new HashSet<Tile>();
            
            
            while (queue.Count>0)
            {
                var (t1, t2) = queue.Dequeue();
                Console.WriteLine($"Visiting {t1}");
                marked.Add(t1);
                CreateLink(t1,t2);
                if (!marked.Contains(t2))
                    foreach (var neighbor in t2.Neighbors)
                        queue.Enqueue((t2, neighbor));
            }

            foreach (var tile in Masters)
            {
                tile.Neighbors.ToList().ForEach(n => CreateLink(tile,n));
            }

            if (!Masters.All(m => m.IsInPlace)) throw new Exception();
            return default;
        }


        public void CreateLink(Tile fixedTile, Tile neighbor)
        {
            var t2borders = neighbor.Variants.SelectMany(v => v.AllBorders).ToHashSet();
            var commonBorder = t2borders.Intersect(fixedTile.AllBorders).Single();
            neighbor = commonBorder.Tile;
            if (commonBorder != null)
            {
                var fixedBorder = fixedTile.AllBorders.Single(b => b.Hash == commonBorder.Hash);
                var neighborBorder = neighbor.AllBorders.FirstOrDefault(b => b.Hash == commonBorder.Hash);
                if(fixedBorder != null && neighborBorder!=null && Link.IsValid(fixedBorder,neighborBorder))
                    fixedTile.AddLink(fixedBorder, neighborBorder);
                else
                {
                    //Console.WriteLine($"Link impossible {fixedBorder.Tile.Id} - {fixedBorder.Edge} et {neighborBorder.Tile.Id} - {neighborBorder.Edge}");
                    while(neighbor.ChangeOrientation())
                    {
                        neighborBorder = neighbor.AllBorders.FirstOrDefault(b => b.Hash == commonBorder.Hash);
                        if (neighborBorder != null && Link.IsValid(fixedBorder!, neighborBorder))
                        {
                            fixedTile.AddLink(fixedBorder, neighborBorder);
                            return;
                        }
                    }
                }
            }
        }
        
        public ImmutableHashSet<Tile> Masters { get; }
        

        public Tile[] Corners => Masters.Where(m => m.IsCorner()).ToArray();

        public Tile this[long id] => _masters[id];
    }
}