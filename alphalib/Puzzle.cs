using System;

namespace com.corporealabstract.alpha
{
    public class Puzzle
    {
        public PuzzleType Type { get; set; }
        
        public EncodingType EncodingType { get; set; }

        public string ClueText { get; set; }

        public string InitialState { get; set; }
        
        public string WinState { get; set; }
    }
}