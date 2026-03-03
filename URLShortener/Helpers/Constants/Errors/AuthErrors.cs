using URLShortener.Helpers.ErrorsAndResults;

namespace URLShortener.Helpers.Constants.Errors;

public static class AuthErrors
{
    public static readonly Error UserNotActive = new("UserNotActive", "The user account is currently disabled.");
    public static readonly Error Forbidden = new("Forbidden", "Forbidden.");
}
    