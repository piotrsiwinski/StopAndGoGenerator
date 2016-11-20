using System;
using System.Text;

namespace XorEncrypt
{
    public class XorEncryption
    {
        public string Encrypt(string key, string message)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < message.Length; i++)
            {
                stringBuilder.Append(Convert.ToChar(message[i] ^ key[i]));
            }

            return stringBuilder.ToString();
        }
    }
}