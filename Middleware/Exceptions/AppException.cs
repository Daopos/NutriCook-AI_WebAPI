namespace NutriCook_AI_WebAPI.Middleware.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; set; }

        protected AppException(string message, int statusCode) :base(message) 
        {
            this.StatusCode = statusCode;
        }
    }
}
