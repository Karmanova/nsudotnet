using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project1.controller
{
    
    class Parser
    {
        private string[] args = null;

        public FileStream streamIn { get; set; }
        public FileStream streamOut { get; set; }
        public Stream streamKeyEncrypt { get; set; }
        public StreamReader streamKeyDecrypt { get; set; }

        private string inputFilePath { get; set; }
        private string outputFilePath { get; set; }
        private string keyFilePath { get; set; }

        public string algorithm { get; set; }
        public string crypt { get; set; }

        private string extension = "txt";

        public Parser(string[] args)
        {
            this.args = args;
            Parse();
        }

        private void Parse()
        {
            if (args.Length <= 0)
            {
                throw new Exception("There is no arguments!");
            }
            
            if (args[0].Equals("encrypt"))
            {
                crypt = "encrypt";
                if (args.Length != 4)
                {
                    throw new Exception("wrong number of parameters for encryption");
                }

                inputFilePath = args[1];
                outputFilePath = args[3];
                algorithm = args[2];
                

                if (!File.Exists(inputFilePath))
                {
                    throw new Exception("There is no such input file in this directory!");
                }

                streamIn = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
                streamOut = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Path.GetPathRoot(inputFilePath));
                stringBuilder.Append(Path.GetPathRoot(inputFilePath));
                stringBuilder.Append(Path.GetFileNameWithoutExtension(inputFilePath));
                stringBuilder.Append(".key.");
                stringBuilder.Append(extension);

                streamKeyEncrypt = new FileStream(stringBuilder.ToString(), FileMode.OpenOrCreate, FileAccess.Write);

            }

            else if (args[0].Equals("decrypt"))
            {
                crypt = "decrypt";
                if (args.Length != 5)
                {
                    throw new Exception("wrong number of parameters for decryption");
                } 

                inputFilePath = args[1];
                outputFilePath = args[4];
                keyFilePath = args[3];
                algorithm = args[2];

                if (!File.Exists(inputFilePath) || !File.Exists(keyFilePath))
                {
                    throw new Exception("There is no such file in this directory!");
                }

                streamIn = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);//output.bin
                streamOut = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write);

                streamKeyDecrypt = new StreamReader(keyFilePath);
            }
            else
            {
                throw new Exception("Wrong type of operation!");
            }
        }

        
    }
}
