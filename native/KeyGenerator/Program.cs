using System;
using System.Collections.Generic;
using System.Text;

namespace KeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var bytes = new Guid("3095281d-8329-4082-ad75-0fcfb6bf17d3").ToByteArray();
            var secret = 0;
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i % 2 == 1)
                {
                    secret += (i - 1) * bytes[i];
                }
                else
                {
                    secret += (i + 1) * bytes[i];
                }
            }

            Console.WriteLine("Secret: {0}",secret);
            Console.ReadLine();
        }
    }
}
