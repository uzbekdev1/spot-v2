namespace MessageBroker.Core
{
    public class ApiResponse<T>
    {

        public string Error { get; set; }

        public bool Success { get; set; }

        public T Data { get; set; }

    }

    public class ApiResponse : ApiResponse<object>
    {

    }

}