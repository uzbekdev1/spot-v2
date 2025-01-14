using Newtonsoft.Json;

namespace ClientApi.Helpers
{
    public sealed class LogGenerate
    {

        private static LogGenerate _instance = null;

        public static LogGenerate Instance
        {
            get
            {
                if (_instance == null)
                {
                    return _instance = new LogGenerate();
                }

                return _instance;
            }
        }

        public string GenerateLogError(string methodStage, string errorMessage, object innerException, object stackTrace, object methodParams)
        {
            return JsonConvert.SerializeObject
                (
                    new
                    {
                        MethodStage = methodStage,
                        ErrorMessage = errorMessage,
                        InnerException = innerException,
                        StackTrace = stackTrace,
                        MethodParams = methodParams
                    },
                    formatting: Formatting.None
                );
        }

    }
}
