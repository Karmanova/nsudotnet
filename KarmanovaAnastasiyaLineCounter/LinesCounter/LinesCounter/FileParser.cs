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
                while (!stream.EndOfStream)
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
                                if (IsCommentOpen(tmp, false))
                                {
                                    state = 1;
                                }

                            }
                            else if (tmp.Contains("/*"))
                            {
                                count++;
                                if (IsCommentOpen(tmp, false))
                                {
                                    state = 1;
                                }
                            }
                            else if (!String.IsNullOrWhiteSpace(tmp))
                            {
                                count++;
                            }
                            break;
                        case 1 :
                            if (tmp.Contains("*/"))
                            {
                                if (tmp.Contains("/*"))
                                {
                                    if (IsCommentOpen(tmp, true))
                                    {
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

        private bool IsCommentOpen(string str, bool startState)
        {
            int state;
            bool isCommentOpen;
            if (startState)
            {
                state = 2;
                isCommentOpen = true;
            }
            else
            {
                state = 0;
                isCommentOpen = false;
            }
            
            foreach (char c in str)
            {
                switch (state)
                {
                    case 0:
                        if (c.Equals('/'))
                        {
                            state = 1;
                        }
                        break;
                    case 1:
                        if (c.Equals('*'))
                        {
                            isCommentOpen = true;
                            state = 2;
                        }
                        else if (!c.Equals('/'))
                        {
                            state = 0;
                        }
                        break;
                    case 2:
                        if (c.Equals('*'))
                        {
                            state = 3;
                        }
                        break;
                    case 3:
                        if (c.Equals('/'))
                        {
                            state = 0;
                            isCommentOpen = false;
                        }
                        else if (!c.Equals('*'))
                        {
                            state = 2;
                        }
                        break;
                    default:
                        break;
                }
            }
            return isCommentOpen;
        }
    }
}
