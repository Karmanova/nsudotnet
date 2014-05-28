using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Project1.model
{
    class RC2Crypto : CryptoInterface
    {
        private byte[] key;
        private byte[] iv;

        public void Encrypt(Stream streamIn, Stream streamOut, Stream streamKey)
        {
            using (RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider())
            {
                key = rc2.Key;
                iv = rc2.IV;

                ICryptoTransform encryptor = rc2.CreateEncryptor(key, iv);

                CryptoStream cryptoStream = new CryptoStream(streamOut, encryptor, CryptoStreamMode.Write);

                streamIn.CopyTo(cryptoStream);


                StringBuilder builder = new StringBuilder();
                builder.AppendLine(Convert.ToBase64String(iv));
                builder.AppendLine(Convert.ToBase64String(key));



                byte[] keyfile = System.Text.Encoding.Default.GetBytes(builder.ToString());

                streamKey.Write(keyfile, 0, keyfile.Length);
            }
        }

        public void Decrypt(Stream streamIn, Stream streamOut, StreamReader streamKey)
        {
            using (RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider())
            {
                rc2.Padding = PaddingMode.None;
                
                rc2.IV = Convert.FromBase64String(streamKey.ReadLine().TrimEnd(new Char[] {' '}));
                rc2.Key = Convert.FromBase64String(streamKey.ReadLine().TrimEnd(new Char[] {' '}));



                //ICryptoTransform decryptor = aes.CreateDecryptor();
                using (ICryptoTransform decryptor = rc2.CreateDecryptor(rc2.Key, rc2.IV))
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
