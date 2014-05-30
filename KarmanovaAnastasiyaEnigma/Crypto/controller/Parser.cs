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

        public FileStream StreamIn { get; set; }
        public FileStream StreamOut { get; set; }
        public Stream StreamKeyEncrypt { get; set; }
        public StreamReader StreamKeyDecrypt { get; set; }

        private string InputFilePath { get; set; }
        private string OutputFilePath { get; set; }
        private string KeyFilePath { get; set; }

        public string Algorithm { get; set; }
        public string Crypt { get; set; }

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

            if ("encrypt".Equals(args[0]))
            {
                Crypt = "encrypt";
                if (args.Length != 4)
                {
                    throw new Exception("wrong number of parameters for encryption");
                }

                InputFilePath = args[1];
                OutputFilePath = args[3];
                Algorithm = args[2];
                

                if (!File.Exists(InputFilePath))
                {
                    throw new Exception("There is no such input file in this directory!");
                }

                StreamIn = new FileStream(InputFilePath, FileMode.Open, FileAccess.Read);
                StreamOut = new FileStream(OutputFilePath, FileMode.OpenOrCreate, FileAccess.Write);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Path.GetPathRoot(InputFilePath));
                stringBuilder.Append(Path.GetPathRoot(InputFilePath));
                stringBuilder.Append(Path.GetFileNameWithoutExtension(InputFilePath));
                stringBuilder.Append(".key.");
                stringBuilder.Append(extension);

                StreamKeyEncrypt = new FileStream(stringBuilder.ToString(), FileMode.OpenOrCreate, FileAccess.Write);

            }

            else if ("decrypt".Equals(args[0]))
            {
                Crypt = "decrypt";
                if (args.Length != 5)
                {
                    throw new Exception("wrong number of parameters for decryption");
                } 

                InputFilePath = args[1];
                OutputFilePath = args[4];
                KeyFilePath = args[3];
                Algorithm = args[2];

                if (!File.Exists(InputFilePath) || !File.Exists(KeyFilePath))
                {
                    throw new FileNotFoundException(); //Exception("There is no such file in this directory!");
                }

                StreamIn = new FileStream(InputFilePath, FileMode.Open, FileAccess.Read);//output.bin
                StreamOut = new FileStream(OutputFilePath, FileMode.OpenOrCreate, FileAccess.Write);

                StreamKeyDecrypt = new StreamReader(KeyFilePath);
            }
            else
            {
                throw new Exception("Wrong type of operation!");
            }
        }

        
    }
}
