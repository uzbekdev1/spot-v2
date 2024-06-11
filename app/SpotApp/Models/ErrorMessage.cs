using System;
using System.Net;

namespace SpotApp.Models
{
    internal class ErrorMessage
    {
        public Exception AppException { get; set; } = new Exception("");

        public bool haveError { get; set; }

        public string ErrorKeyName { get; set; } = "";

        public double? ApiElapsedTime { get; set; } = null;

        public string ErrorText
        {
            get
            {
                string errorText;

                if (ExceptionTypeName == "WebException")
                {
                    var webException = AppException as WebException;

                    if (webException.Status == WebExceptionStatus.ProtocolError)
                        if (((HttpWebResponse)webException.Response).StatusCode == (HttpStatusCode)429)
                            errorText = "Слишком много запросов. Пожалуйста, повторите попытку позже!";
                        else
                            errorText = AppException.Message;

                    else if (webException.Status == WebExceptionStatus.Timeout)
                        errorText = "Время операции истекло. Проверьте подключения интернета и VPN Kerio Control";

                    else if (webException.Status == WebExceptionStatus.ConnectFailure)
                        errorText = "Невозможно подключиться к серверу. Проверьте подключения интернета и VPN Kerio Control";

                    else
                        errorText = AppException.Message;
                }
                else
                    errorText = AppException.Message;

                return errorText;
            }
        }

        public string ExceptionTypeName
        {
            get
            {
                return AppException.GetType().Name;
            }
        }
    }
}
