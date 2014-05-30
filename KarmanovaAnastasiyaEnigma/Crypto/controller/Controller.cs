using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Project1.model;

namespace Project1.controller
{
    class Controller
    {
        private string algorithm;
        private string crypt;
        private Parser parser = null;
        private CryptoInterface cryptor;

        public Controller(string[] args)
        {
            
            try
            {
                parser = new Parser(args);
                
                 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (!(e is FileNotFoundException))
                {
                   CloseStreams();
                }
                return;
            }



            if ("encrypt".Equals(parser.Crypt))
            {
                if (Algorithms.Aes.ToString().ToLower().Equals(parser.Algorithm))
                {
                    System.Console.Write("AES");
                    cryptor = new AESCrypto();
                    cryptor.Encrypt(parser.StreamIn, parser.StreamOut, parser.StreamKeyEncrypt);
                }
                else if (Algorithms.Des.ToString().ToLower().Equals(parser.Algorithm))
                {
                    System.Console.Write("DES");
                    cryptor = new DESCrypto();
                    cryptor.Encrypt(parser.StreamIn, parser.StreamOut, parser.StreamKeyEncrypt);
                    
                }
                else if (Algorithms.Rc2.ToString().ToLower().Equals(parser.Algorithm))
                {
                    System.Console.Write("RC2");
                    cryptor = new RC2Crypto();
                    cryptor.Encrypt(parser.StreamIn, parser.StreamOut, parser.StreamKeyEncrypt);

                }
                else if (Algorithms.Rijndael.ToString().ToLower().Equals(parser.Algorithm))
                {
                    System.Console.Write("rijndael".ToUpper());
                    cryptor = new RIJNDAELCrypto();
                    cryptor.Encrypt(parser.StreamIn, parser.StreamOut, parser.StreamKeyEncrypt);

                    parser.StreamIn.Dispose();
                    parser.StreamOut.Dispose();
                    parser.StreamKeyDecrypt.Dispose();
                }
                else
                {
                    throw new Exception("Wrong name of algorithm!");
                }
            }
            else if ("decrypt".Equals(parser.Crypt))
            {
                if (Algorithms.Aes.ToString().ToLower().Equals(parser.Algorithm))
                {
                    System.Console.Write("AES");
                    cryptor = new AESCrypto();
                    cryptor.Decrypt(parser.StreamIn, parser.StreamOut, parser.StreamKeyDecrypt);
                }
                else if (Algorithms.Des.ToString().ToLower().Equals(parser.Algorithm))
                {
                    System.Console.Write("DES");
                    cryptor = new DESCrypto();
                    cryptor.Decrypt(parser.StreamIn, parser.StreamOut, parser.StreamKeyDecrypt);

                }
                else if (Algorithms.Rc2.ToString().ToLower().Equals(parser.Algorithm))
                {
                    System.Console.Write("RC2");
                    cryptor = new RC2Crypto();
                    cryptor.Decrypt(parser.StreamIn, parser.StreamOut, parser.StreamKeyDecrypt);

                }
                else if (Algorithms.Rijndael.ToString().ToLower().Equals(parser.Algorithm))
                {
                    System.Console.Write("rijndael".ToUpper());
                    cryptor = new RIJNDAELCrypto();
                    cryptor.Decrypt(parser.StreamIn, parser.StreamOut, parser.StreamKeyDecrypt);
                    parser.StreamIn.Dispose();
                    parser.StreamOut.Dispose();
                    parser.StreamKeyEncrypt.Dispose();
                }
                else
                {
                    throw new Exception("Wrong name of algorithm!");
                }

            }
            else{
                throw new Exception("Wrong operation name!");
            }
           // CloseStreams();
            
        }

        private void CloseStreams()
        {
            parser.StreamIn.Dispose();
            parser.StreamOut.Dispose();
            parser.StreamKeyDecrypt.Dispose();
            parser.StreamKeyEncrypt.Dispose();
        }

        
    }

}
