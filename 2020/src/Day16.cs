using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Environment;

namespace _2020
{
    public class Day16
    {
        public static int Part1(PuzzleInput input)
        {
            var (fields,nearbyTickets) = (input.Fields, input.NearByTickets);
            return nearbyTickets.SelectMany(t => t.Where(f => !fields.Any(r => r.IsValid(f)))).Sum();
        }
        
        public static long Part2(PuzzleInput input)
        {
            var (fields,nearbyTickets) = (input.Fields.ToList(), input.NearByTickets);
            
            var fieldsMatrix = nearbyTickets
                .Where(t => t.All(f => fields.Any(r => r.IsValid(f))))
                .ToArray();
            
            var fieldsMatrixT = Transpose(fieldsMatrix).ToList();
            var columnsIndexes = Enumerable.Range(0, fieldsMatrixT.Count).ToList();            
            var fieldMap = new Dictionary<string,int>();

            do
            {
                foreach (var index in columnsIndexes)
                {
                    var candidates = AllMatching(fields, fieldsMatrixT[index]);
                    if (candidates.Length == 1)
                    {
                        var selected = candidates[0];
                        fieldMap[selected.Label] = index;
                        fields.Remove(selected);
                        columnsIndexes.Remove(index);
                        break;
                    }
                }
            } while (columnsIndexes.Any());

            return fieldMap
                .Where(kv => kv.Key.StartsWith("departure"))
                .Select(kv => kv.Value).ToArray()
                .Aggregate(1L, (acc, n) => acc * input.MyTicket[n]);
        }

        private static Field[] AllMatching(IEnumerable<Field> rules, int[] values) => 
            rules.Where(r => values.All(r.IsValid)).ToArray();

        private static IEnumerable<int[]> Transpose(int[][] m)
        {
            var cols = m[0].Length;
            for (int i = 0; i < cols; i++)
            {
               yield return m.Select(r => r[i]).ToArray();
            }
        }
        public static PuzzleInput Dataset(string filePath)
        {
            var data = File
                .ReadAllText(filePath)
                .Split(NewLine + NewLine);
            return PuzzleInput.Create(
                data[0].Split(NewLine).ToArray(),
                data[1].Split(NewLine).ToArray(),
                data[2].Split(NewLine).ToArray()
                );
        }
    }

    public class PuzzleInput
    {
        public static PuzzleInput Create(string[] rulesData, string[] myTicketData, string[] nearByTicketsData)
        {
            var rules = rulesData.Select(Field.Create).ToArray();
            var myTicket = myTicketData[1].Split(',').Select(int.Parse).ToArray();
            var nearByTickets = nearByTicketsData[1..].Select(s => s.Split(',').Select(int.Parse).ToArray()).ToArray();
            return new PuzzleInput(rules,myTicket,nearByTickets);
        }

        public Field[] Fields { get; }
        public int[] MyTicket { get; }
        public int[][] NearByTickets { get; }

        private PuzzleInput(Field[] fields, int[] myTicket, int[][] nearByTickets)
        {
            Fields = fields;
            MyTicket = myTicket;
            NearByTickets = nearByTickets;
        }
    }

    public class Field : IEquatable<Field>
    {
        public static Field Create(string data)
        {
            var n = data.Split(':');
            var label = n[0];
            var p = n[1].Split(' ');
            var r1 = p[1].Split('-').Select(int.Parse).ToArray();
            var r2 = p[3].Split('-').Select(int.Parse).ToArray();
            return new Field(label, (r1[0],r1[1]), (r2[0],r2[1]));
        }

        public string Label { get; }
        public (int from, int to) Range1 { get; }
        public (int from, int to) Range2 { get; }

        private Field(string label, (int @from, int to) range1, (int @from, int to) range2)
        {
            Label = label;
            Range1 = range1;
            Range2 = range2;
        }
        
        public bool IsValid(int n) =>
            (n >= Range1.from && n <= Range1.to) ||
            (n >= Range2.from && n <= Range2.to);

        public bool Equals(Field? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Label == other.Label;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Field) obj);
        }

        public override int GetHashCode()
        {
            return Label.GetHashCode();
        }

        public static bool operator ==(Field? left, Field? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Field? left, Field? right)
        {
            return !Equals(left, right);
        }
    }
}