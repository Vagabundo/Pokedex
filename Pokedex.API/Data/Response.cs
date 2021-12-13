namespace Pokedex.API.Data
{
    public class Response<T>
    {
        public string ErrorMessage { get; private set; }
        public int ErrorCode { get; private set; }
        public T Body { get; private set; }

        public Response (T data)
        {
            Body = data;
        }

        public Response (int code, string message)
        {
            ErrorMessage = message;
            ErrorCode = code;
        }
    }
}