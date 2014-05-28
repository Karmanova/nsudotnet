using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Project1.model;


namespace Project1
{
    class AESCrypto : CryptoInterface
    {
        
        private byte[] key;
        private byte[] iv;

        public void Encrypt(Stream streamIn, Stream streamOut, Stream streamKey) 
        {
            using (Aes aes = Aes.Create())
            {
                key = aes.Key;
                iv = aes.IV;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv))
                {

                    using (CryptoStream cryptoStream = new CryptoStream(streamOut, encryptor, CryptoStreamMode.Write))
                    {
                        streamIn.CopyTo(cryptoStream);


                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine(Convert.ToBase64String(iv));
                        builder.AppendLine(Convert.ToBase64String(key));



                        byte[] keyfile = System.Text.Encoding.Default.GetBytes(builder.ToString());

                        streamKey.Write(keyfile, 0, keyfile.Length);
                    }
                }
            }
        }
        public void Decrypt(Stream streamIn, Stream streamOut, StreamReader streamKey)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.None;
                
                aes.IV = Convert.FromBase64String(streamKey.ReadLine().TrimEnd(new Char[] {' '}));
                aes.Key = Convert.FromBase64String(streamKey.ReadLine().TrimEnd(new Char[] {' '}));

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(streamIn, decryptor, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(streamOut);
                    }
                }

            }
        }
    }
}
