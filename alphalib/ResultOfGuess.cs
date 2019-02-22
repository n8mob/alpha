namespace com.corporealabstract.alpha
{
    public class ResultOfGuess
    {
        public Result IsWin { get; }
        public string FurtherHint { get; }

        public ResultOfGuess (Result result, string furtherHint = "")
        {
            IsWin = result;
            FurtherHint = furtherHint;
        }
    }
}