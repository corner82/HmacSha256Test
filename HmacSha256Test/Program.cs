using System;
using System.Security.Cryptography;
using System.Text;


namespace HmacSha256Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //string nonce = "rz8LuOtFBXphj9WQfvFh";
            Account account = new Account
            {
                Name = "John Doe",
                Email = "john@microsoft.com",
                Nonce = "rz8LuOtFBXphj9WQfvFh",
                //DOB = new DateTime(1980, 2, 20, 0, 0, 0, DateTimeKind.Utc),
            };
            var privateKey = "7b604e01-3b17-4d7b-b57c-3bf724962d0b";
            //string hash = string.Join("", new string[] { nonce });
            string hash = Newtonsoft.Json.JsonConvert.SerializeObject(account);
            Console.WriteLine("hash edilecek object->" + hash);
            Console.WriteLine("hash secret key->" + privateKey);
            var hashTest = "";
            byte[] hashLeftByte;
            using (HMACSHA256 hmac = new HMACSHA256())
            {
                hmac.Key = Encoding.UTF8.GetBytes(privateKey) ;
                hashLeftByte = hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                //hashTest = Convert.ToBase64String(hashLeftByte);
                hashTest = BitConverter.ToString(hashLeftByte).ToLower().Replace("-", string.Empty);

            }

            string hashRight = string.Join(":", new string[] { account.Name, account.Email });
            Console.WriteLine("concat değer right->"+hashRight);
            string hashConc = string.Join(":", hashTest, hashRight);
            Console.WriteLine("concat değer ->" + hashConc);
            string hashBaseEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(hashConc));

            
            Console.WriteLine("hash değer->" + hashTest);
            Console.WriteLine("base64 encoded hash->"+Convert.ToBase64String(Encoding.UTF8.GetBytes(hashTest)));
            //Console.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(hashBaseEncoded)));
            Console.WriteLine("base64 encoded hash concat->" + hashBaseEncoded);
            Console.ReadLine();
        }
    }
}
