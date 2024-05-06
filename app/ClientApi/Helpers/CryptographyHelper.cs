using System.Security.Cryptography;
using System.Text;

namespace ClientApi.Helpers
{
    public class CryptographyHelper
    {

        public string Decrypt(string text)
        {
            var rsa = new RSACryptoServiceProvider(1024);

            try
            {
                var privateKey = @"<RSAParameters><D>TU5bvCaUYUVjjy6np9qtnSc715tCIHq3X/RzMYCmlp9xvCCbOmocuof1slwUOTtdFyl93xwbF1Eekg8NoM7rIj0yNL12vv90X1eHNH8nin7DEp5wx408BTBaNZINpf7CjL9q7W0cNzSrobok04LJixGjJb00kUXVuF4itgEg6a63/hYG3ZHAJXlfqzyJjhHxiA9P5So7Uqe4aOvHBvFoKmX7BEWbPLi2HAbRjpdFyFOsg/t3vUfpTDjUCDp2JuATpRl45TQIDMYMOpE6dnyVBbk3iqvRxncoiMXYUzAFEZDJdKTnTlSYQzJoJyiLvDsaarefb4rtAfPbOlkZ6EtECQ==</D><DP>gHHjBEi43QApjdcq5n1j0deqWF8/UoE5U11Egdr4iBnrcp72NAb6ncmcVrzIT1ckyNqdHPz97BNiJCi20zOPVeWDZCjZCZ2FguHSAWvLSb12/OG+eiA+C0evnx6jFM6In0xBbbzhoseb0PmmTQdua4+gJFPNIH1sKoLbCX70aIs=</DP><DQ>RPJ7UZysdRJiSdj3Z6xKT1Qned685WRPw2MB/QDJPqwwVSd1MInVlO7iHGao++O66fI4xBpnGuffh5nmdcGtWoVQXU6QLMXBHaZoUX/z7R4F+feENE06L/SyncDOTbV1nBuPqg/trPlAUkAkqqK0FwSiyT3bVdJS+RQsr8yuEX0=</DQ><Exponent>AQAB</Exponent><InverseQ>iL6Zn98+3FT1f0/PTAqrdjZSTLfFKFilHZ6zAdzuHqh4UQRj9sP8vjqiRKkuAq2cxkeSaVaX7gD+hyr/GKSlSYy+SfD9VbBQmte1gwdKKfo4zQdp1OIbNAOVRJh2t2cBW4ZivXt7SC8b2TTucs1Tds8eI6txzokNkq9wVtNe0bM=</InverseQ><Modulus>qtPmnB3ETTeujk38f/obyvB9HZldykiKMyrbm7KpEnjhxJluohS+SJTh04KV/xBwBvZS+TKv8b4AMzesB38/oDmhGia8G1RuIWvVFU5kzGKE1kmJhU8C3pBaGO5DgCZ3vDTBHdB4d5vleC+0KJmwQkAINkeGc7hzlKeNIUzC5POLeNQchDgrmyM0mLPFiiUgF6UgTy3ezaL2/Vi0UaAyfsETbfYxOwBjVudbBgQ7znKLznw27BOY9M3jZYiPPa8eUb1cT84qmGI9kPMzCtBBi1oTb2vt/9VBnazCR0/KSkekrdXHh7lbae+hLHKkpAduuE0HxwNpcQxMCR0FuTl5iQ==</Modulus><P>1/WsERL7P/FU36VfFzE2njYG0UTBLcy7XY9WfyIHi/PpnvxLcUDphiVAoml09sF4Z4348ZkNOkiCFrrKRQPpSRjJxNO/oIX5XozGz4KCnTiZHsYxqePMt3wkD8b0bkSkJoS14lezPDa2XGPZgUTiZJ3U4GcYX9oLGU/ToZ97Oa8=</P><Q>yoAVVxO706gRByxeFTKpstcqrazJUq9/mFG/KCm9f51qdVbrpcSkXZN1Q8/gdOQnWO0Cb5iZhMW5XlgiI3mTm+XIo+iRJNlnowU7LLkvNeu0vVC5KnzxQoFlDlWKXZ0k5BbOERzLE2BcpauA0tBIQFjF2vlInjQYROru/03rpkc=</Q></RSAParameters>";

                rsa.FromXmlString(privateKey);

                var bytesToDescrypt = Convert.FromBase64String(text);
                var decryptedBytes = rsa.Decrypt(bytesToDescrypt, true);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch
            {
                return null;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
         
    }

}
