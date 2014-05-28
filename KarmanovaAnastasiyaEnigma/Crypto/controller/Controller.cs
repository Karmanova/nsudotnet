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
        //private enum Algorithms
        //{
        //    aes,
        //    des,
        //    rc2,
        //    rijndael
        //}

        //private FileStream streamIn;
        //private FileStream streamOut;

        //private string inputFilePath;
        //private string outputFilePath;
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
                return;
            }

            

            if (parser.crypt.Equals("encrypt"))
            {
                if (parser.algorithm.Equals(Algorithms.aes.ToString()))
                {
                    System.Console.Write("AES");
                    cryptor = new AESCrypto();
                    cryptor.Encrypt(parser.streamIn, parser.streamOut, parser.streamKeyEncrypt);
                }
                else if (parser.algorithm.Equals(Algorithms.des.ToString()))
                {
                    System.Console.Write("DES");
                    cryptor = new DESCrypto();
                    cryptor.Encrypt(parser.streamIn, parser.streamOut, parser.streamKeyEncrypt);
                    
                }
                else if (parser.algorithm.Equals(Algorithms.rc2.ToString()))
                {
                    System.Console.Write("RC2");
                    cryptor = new RC2Crypto();
                    cryptor.Encrypt(parser.streamIn, parser.streamOut, parser.streamKeyEncrypt);

                }
                else if (parser.algorithm.Equals(Algorithms.rijndael.ToString()))
                {
                    System.Console.Write("rijndael".ToUpper());
                    cryptor = new RIJNDAELCrypto();
                    cryptor.Encrypt(parser.streamIn, parser.streamOut, parser.streamKeyEncrypt);

                }
            }
            else if (parser.crypt.Equals("decrypt"))
            {
                if (parser.algorithm.Equals(Algorithms.aes.ToString()))
                {
                    System.Console.Write("AES");
                    cryptor = new AESCrypto();
                    cryptor.Decrypt(parser.streamIn, parser.streamOut, parser.streamKeyDecrypt);
                }
                else if (parser.algorithm.Equals(Algorithms.des.ToString()))
                {
                    System.Console.Write("DES");
                    cryptor = new DESCrypto();
                    cryptor.Decrypt(parser.streamIn, parser.streamOut, parser.streamKeyDecrypt);

                }
                else if (parser.algorithm.Equals(Algorithms.rc2.ToString()))
                {
                    System.Console.Write("RC2");
                    cryptor = new RC2Crypto();
                    cryptor.Decrypt(parser.streamIn, parser.streamOut, parser.streamKeyDecrypt);

                }
                else if (parser.algorithm.Equals(Algorithms.rijndael.ToString()))
                {
                    System.Console.Write("rijndael".ToUpper());
                    cryptor = new RIJNDAELCrypto();
                    cryptor.Decrypt(parser.streamIn, parser.streamOut, parser.streamKeyDecrypt);

                }
            }
            else{
                throw new Exception("Wrong operation name!");
            }
            
        }

        

        private bool parseKey(string keyFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
