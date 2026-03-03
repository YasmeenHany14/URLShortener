namespace URLShortener.Helpers.ErrorsAndResults;

public interface IResult
{
    bool IsSuccess { get; }
    Error Error { get; }
}
