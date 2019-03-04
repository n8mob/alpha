using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace com.corporealabstract.alpha
{
    [Serializable]
    public class GameManager : IEnumerator<Puzzle>
    {
        private IEnumerator<Puzzle> PuzzleEnumerator { get; set; }

        private IList<Puzzle> Puzzles { get; set; }

        public Puzzle Current => PuzzleEnumerator.Current;

        object IEnumerator.Current => Current;

        public GameManager(IEnumerable<Puzzle> puzzles)
        {
            if (puzzles == null)
            {
                throw new ArgumentException("Can't have a null puzzle list");
            }
            
            Puzzles = new List<Puzzle>(puzzles);

            if (Puzzles.Any())
            {
                throw new ArgumentException("Can't have an empty puzzle list");
            }
            
            PuzzleEnumerator = Puzzles.GetEnumerator();
        }

        public ResultOfGuess Try(string guess)
        {
            var guessMatches = guess.Equals(Current?.WinState, StringComparison.InvariantCultureIgnoreCase);
            
            return guessMatches
                ? new ResultOfGuess(Result.WIN)
                : new ResultOfGuess(Result.TRY_AGAIN);
        }

        public bool MoveNext()
        {
            return PuzzleEnumerator.MoveNext();
        }

        public void Reset()
        {
            PuzzleEnumerator.Reset();
        }

        public void Dispose()
        {
            this.PuzzleEnumerator = null;
            this.Puzzles = null;
        }
    }
}