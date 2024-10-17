namespace CatalogoDeProdutos.Core.Response
{
    public class Response<T>
    {
        public bool Success { get; set; } 
        public T? Data { get; set; } 
        public string? Message { get; set; } 
        public int? StatusCode { get; set; } 

        public Response()
        {
            Success = true; 
        }

        public Response(T data, string message = "", int? statusCode = null)
        {
            Success = true;
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }

        public Response(string message, int statusCode)
        {
            Success = false;
            Message = message;
            StatusCode = statusCode;
        }
    }
}

