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

        public GameManager()
        {
            Puzzles = new List<Puzzle>()
            {
                new Puzzle()
                {
                    Type = PuzzleType.Decode,
                    EncodingType = EncodingType.Variable,
                    ClueText = "ABC",
                    InitialState = "ABC",
                    WinState = "ABC"
                },
                new Puzzle()
                {
                    Type = PuzzleType.Decode,
                    EncodingType = EncodingType.Variable,
                    ClueText = "DEF",
                    InitialState = "DEF",
                    WinState = "DEF",
                },
                new Puzzle()
                {
                    Type = PuzzleType.Decode,
                    EncodingType = EncodingType.Variable,
                    ClueText = "GHI",
                    InitialState = "GHI",
                    WinState = "GHI",
                },
                new Puzzle()
                {
                    Type = PuzzleType.Decode,
                    EncodingType = EncodingType.Variable,
                    ClueText = "JKL",
                    InitialState = "JKL",
                    WinState = "JKL",
                }
            };
            
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
            PuzzleEnumerator = null;
            Puzzles = null;
        }
    }
}