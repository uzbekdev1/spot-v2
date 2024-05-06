using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;

namespace StressTests
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Stress Tests";

            Console.WriteLine("Enter clicks:");

            var clicks = int.TryParse(Console.ReadLine(), out var value) ? value : 0;
            if (clicks == 0)
            {
                Console.WriteLine("Not number!");
                return;
            }

            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJrNDU1IiwibmFtZWlkIjoiNTg5OTUiLCJuYmYiOjE3MDQ3MTgyNTEsImV4cCI6MTcwNDgwNDY1MSwiaWF0IjoxNzA0NzE4MjUxfQ.wbiIPuCCBpPEQsfi4j6FSgf_C6daSS4cBbkozSeul6w";
            var timer = new Stopwatch();

            timer.Start();

            for (var i = 0; i < clicks; i++)
            {
                CallApi(i + 1, token);
            }

            timer.Stop();

            Console.WriteLine("DONE!({0})", timer.Elapsed.ToString("g"));
            Console.ReadLine();
        }

        private static void CallApi(int counter, string token)
        {
            var client = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("http://oldspot-api.uzex.uz"),
                RemoteCertificateValidationCallback = (a, b, c, d) => true
            });
            var request = new RestRequest("/api/Cabinet/CreateOrder", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");

            var json = JsonConvert.SerializeObject(new
            {
                contractId = 52500,
                uid = Guid.NewGuid().ToString(),
                inp = 5041,
                kolvo = 1,
                price = 6894552.45,
                clientDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff")
            });
            request.AddJsonBody(new
            {
                raw = CryptographyHelper.Encrypt(json)
            });

            var response = client.Execute(request);

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse>(response.Content);

            Console.WriteLine();

            if (!resut.Success)
            {
                Console.WriteLine($"Call#{counter}: {resut.Error}");
            }
            else
            {
                Console.WriteLine($"Call#{counter}: OK...");
            }

            Console.WriteLine(json);
            Console.WriteLine();
        }

    }
}
