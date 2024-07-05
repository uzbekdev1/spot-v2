using System;
using System.Security.Cryptography;
using System.Text;

namespace SpotApp.Helpers
{
    internal static class CryptographyHelper
    {

        public static string Encrypt(string text)
        {
            var rsa = new RSACryptoServiceProvider(1024);

            try
            {
                var key = "<RSAParameters><Exponent>AQAB</Exponent><Modulus>qtPmnB3ETTeujk38f/obyvB9HZldykiKMyrbm7KpEnjhxJluohS+SJTh04KV/xBwBvZS+TKv8b4AMzesB38/oDmhGia8G1RuIWvVFU5kzGKE1kmJhU8C3pBaGO5DgCZ3vDTBHdB4d5vleC+0KJmwQkAINkeGc7hzlKeNIUzC5POLeNQchDgrmyM0mLPFiiUgF6UgTy3ezaL2/Vi0UaAyfsETbfYxOwBjVudbBgQ7znKLznw27BOY9M3jZYiPPa8eUb1cT84qmGI9kPMzCtBBi1oTb2vt/9VBnazCR0/KSkekrdXHh7lbae+hLHKkpAduuE0HxwNpcQxMCR0FuTl5iQ==</Modulus></RSAParameters>";

                rsa.FromXmlString(key);

                var bytesToEncrypt = Encoding.UTF8.GetBytes(text);
                var encryptedData = rsa.Encrypt(bytesToEncrypt, true);

                return Convert.ToBase64String(encryptedData);
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

        public static string EncryptV2(string text)
        {
            var rsa = new RSACryptoServiceProvider(512);

            try
            {
                var key = "<RSAKeyValue><Modulus>l2LEmHZboKriygRXmuFj60yueqIGQVXcRT8L0yEiLZHaoP7gyvb2nXv/wJmwF+HFpZKsNLNFNEJjbuydDoq5mQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

                rsa.FromXmlString(key);

                var bytesToEncrypt = Encoding.UTF8.GetBytes(text);
                var encryptedData = rsa.Encrypt(bytesToEncrypt, true);

                return Convert.ToBase64String(encryptedData);
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
