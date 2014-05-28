using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LinesCounter
{
    class FileParser
    {
        private List<string> extensionList;
        private StringBuilder stringBuilder;
        private StreamReader stream;
        private System.IO.FileInfo file;

        public FileParser(List<string> extensionList)
        {
            this.extensionList = extensionList;
        }

        public string ParseFile(string dir)
        {
            stringBuilder = new StringBuilder();
            
            file = new System.IO.FileInfo(dir);

            if (extensionList.Contains(file.Extension))
            {
                stringBuilder.Append(file.Name);
                stringBuilder.Append(" : ");
                stringBuilder.Append(CountLines(dir));

                return stringBuilder.ToString();
            }

            return null;
        }

        private int CountLines(string dir)
        {
            int state = 0;
            int count = 0;

            using (stream = new StreamReader(dir))
            {
                string tmp;
                while (stream.Peek() > 0)
                {
                    tmp = stream.ReadLine();

                    switch (state)
                    {
                        case 0:
                            if (tmp.StartsWith("//"))
                            {
                               //just skip 
                            }
                            else if (tmp.StartsWith("/*"))
                            {
                                state = 1;
                            }
                            else if (tmp.Contains("/*"))
                            {

                                count++;
                                char[] openComment = {'/', '*'};
                                int open = tmp.LastIndexOfAny(openComment);
                                char[] closeComment = { '*', '/' };
                                int close = tmp.LastIndexOfAny(closeComment);
                                if (close > open)
                                {
                                    //
                                }
                                else
                                {
                                    state = 1;
                                }
                            }
                            else if (!tmp.Equals(""))
                            {
                                count++;
                            }
                            break;
                        case 1 :
                            if (tmp.EndsWith("*/"))
                            {
                                state = 0;
                            }
                            else if (tmp.Contains("*/"))
                            {
                                if (tmp.Contains("/*"))
                                {
                                    char[] openComment = { '/', '*' };
                                    int open = tmp.LastIndexOfAny(openComment);
                                    char[] closeComment = { '*', '/' };
                                    int close = tmp.LastIndexOfAny(closeComment);
                                    if (close > open)
                                    {
                                        count++;
                                        state = 0;
                                    }
                                }
                                else
                                {
                                    count++;
                                }
                            }
                            break;
                        default:break;

                    }
                }
            }

            return count;
        }
    }
}
