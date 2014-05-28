using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Project1.model
{
    class RIJNDAELCrypto : CryptoInterface
    {
        private byte[] key;
        private byte[] iv;

        public void Encrypt(Stream streamIn, Stream streamOut, Stream streamKey)
        {
            Rijndael rij = Rijndael.Create();
            key = rij.Key;
            iv = rij.IV;

            ICryptoTransform encryptor = rij.CreateEncryptor(key, iv);

            CryptoStream cryptoStream = new CryptoStream(streamOut, encryptor, CryptoStreamMode.Write);

            streamIn.CopyTo(cryptoStream);


            StringBuilder builder = new StringBuilder();
            builder.AppendLine(Convert.ToBase64String(iv));
            builder.AppendLine(Convert.ToBase64String(key));



            byte[] keyfile = System.Text.Encoding.Default.GetBytes(builder.ToString());

            streamKey.Write(keyfile, 0, keyfile.Length);
        }

        public void Decrypt(Stream streamIn, Stream streamOut, StreamReader streamKey)
        {
            Rijndael rij = Rijndael.Create();
            rij.Padding = PaddingMode.None;
            //tmpiv = 
            rij.IV = Convert.FromBase64String(streamKey.ReadLine().TrimEnd(new Char[] { ' ' }));
            rij.Key = Convert.FromBase64String(streamKey.ReadLine().TrimEnd(new Char[] { ' ' }));



            //ICryptoTransform decryptor = aes.CreateDecryptor();
            ICryptoTransform decryptor = rij.CreateDecryptor(rij.Key, rij.IV);

            CryptoStream cryptoStream = new CryptoStream(streamIn, decryptor, CryptoStreamMode.Read);
            //StreamReader srDecrypt = new StreamReader(cryptoStream);
            //Console.WriteLine(srDecrypt.ReadToEnd());
            cryptoStream.CopyTo(streamOut);
        }
    }
}
