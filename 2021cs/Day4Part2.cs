using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NFluent;
using NUnit.Framework;

namespace _2021cs
{
    public class Cell
    {
        public Cell(int n)
        {
            Number = n;
            Marked = false;
        }
        public bool Marked;
        public int Number;
    }

    public class Board
    {
        const int ROWS = 5;
        const int COLS = 5;
        public Board(Cell[] cells)
        {
            this.cells = cells;
            this.cellsIndex = cells.ToDictionary(c => c.Number, c => c);
            this.won = false;
        }
        
        Cell[] cells;
        private readonly Dictionary<int,Cell> cellsIndex;
        private bool won;

        public bool HasWon()
        {
            if (won) return won;

            if (cells.Chunk(5).Any(row => row.All(cell => cell.Marked))) 
                won = true;

            if (Columns(ROWS,COLS).Any(cols => cols.All(c => cells[c].Marked)))
                won = true;

            return won;
        }

        public bool Mark(int n)
        {
            if (cellsIndex.TryGetValue(n, out var cell))
            {
                cell.Marked = true;
            }
            return HasWon();
        }

        public int Score(int n) => 
            cells.Where(c => !c.Marked).Select(c=>c.Number).Sum() * n;


        public static IEnumerable<IEnumerable<int>> Columns(int rows, int cols)
        {
            return Enumerable.Range(0, cols)
                .Select(c => Enumerable.Range(0, rows).Select(r => c + r * cols));
        }
    }


    public class Solution
    {
        public static int Play(int[] drawnNumbers, Board[] boards)
        {
            var (winner, n) = _play(drawnNumbers, boards).Last();
            return winner.Score(n);
        }

        private static IEnumerable<(Board board, int number)> _play(int[] drawnNumbers, Board[] boards)
        {
            foreach (var number in drawnNumbers)
            foreach (var board in boards)
                if (!board.HasWon() && board.Mark(number))
                    yield return (board, number);
        }
    }
    
    public class Day4Part2
    {
        [Test]
        public void part2_example()
        {
            var (drawNumbers, boards) = ParseInput("example.txt");
            Check.That(Solution.Play(drawNumbers, boards)).IsEqualTo(1924);
        }
        
        [Test]
        public void part2_solution()
        {
            var (drawNumbers, boards) = ParseInput("input.txt");
            Check.That(Solution.Play(drawNumbers, boards)).IsEqualTo(21184);
        }

        private Board parseBoard(string[] lines)
        {
            var cells = lines.SelectMany(l => l.Split(' '))
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => int.Parse(s))
                .Select(i => new Cell(i)).ToArray();

            return new Board(cells);
        }

        private (int[], Board[]) ParseInput(string path)
        {
            var allLines = File.ReadLines(path).ToArray();
            var drawNumbers = allLines[0].Split(',').Select(s => int.Parse(s.Trim())).ToArray();
            var boards = allLines.Skip(1).Chunk(6).Select(lines => parseBoard(lines.Skip(1).ToArray())).ToArray();
            return (drawNumbers, boards);
        }
        
        [Test]
        public void METHOD()
        {
            var ints = new[]
            {
                2, 3, 4,
                5, 6, 7,
                8, 9, 0
            };

            var colsIdx = Board.Columns(3, 3).ToArray();

            Check.That(colsIdx[0]).ContainsExactly(0, 3, 6);
            Check.That(colsIdx[1]).ContainsExactly(1, 4, 7);
            Check.That(colsIdx[2]).ContainsExactly(2, 5, 8);
        }
    }
}