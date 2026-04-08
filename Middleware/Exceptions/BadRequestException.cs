namespace NutriCook_AI_WebAPI.Middleware.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message) : base(message, 400) { }
    }
}
