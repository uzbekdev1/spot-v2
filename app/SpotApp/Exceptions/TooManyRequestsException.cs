using System;

namespace SpotApp.Exceptions
{
    internal class TooManyRequestsException : Exception
    {
        public TooManyRequestsException() : base("Удаленный сервер возвратил ошибку: (429) Too Many Requests.")
        {

        }
    }
}
