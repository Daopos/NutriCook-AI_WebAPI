namespace NutriCook_AI_WebAPI.Middleware.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message, 404)
        {
        }
    }
}
