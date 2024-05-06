namespace SpotApp.Core
{

    internal class ApiResponse : ApiResponse<object>
    {

    }

    internal class ApiResponse<T> where T : class
    {

        public string Error { get; set; }

        public bool Success { get; set; }

        public T Data { get; set; }

    }

}