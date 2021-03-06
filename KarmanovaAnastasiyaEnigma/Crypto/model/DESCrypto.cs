﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Project1.model
{
    class DESCrypto : CryptoInterface
    {
        private byte[] key;
        private byte[] iv;

        public void Encrypt(Stream streamIn, Stream streamOut, Stream streamKey)
        {
            using (TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider())
            {
                key = des.Key;
                iv = des.IV;

                using (ICryptoTransform encryptor = des.CreateEncryptor(key, iv))
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
            using (TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider())
            {
                des.Padding = PaddingMode.None;
                //tmpiv = 
                des.IV = Convert.FromBase64String(streamKey.ReadLine().TrimEnd(new Char[] {' '}));
                des.Key = Convert.FromBase64String(streamKey.ReadLine().TrimEnd(new Char[] {' '}));



                //ICryptoTransform decryptor = aes.CreateDecryptor();
                using (ICryptoTransform decryptor = des.CreateDecryptor(des.Key, des.IV))
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
