using System;
using System.Collections.Generic;
using System.IO;

namespace LinesCounter
{
    class TreeTraverser
    {
        private List<string> extensionList;
        private string currDir;
        private FileParser fileParser;

        public TreeTraverser(List<string> extensionList)
        {
            this.extensionList = extensionList;

            fileParser = new FileParser(extensionList);

            currDir = Directory.GetCurrentDirectory();

            Traverse(currDir);
            
            System.Console.Read();
        }
        private void Traverse(string currentDir)
        {

            if (!System.IO.Directory.Exists(currDir))
            {
                throw new ArgumentException();
            }

            string[] subDirs = null;
            string[] subFiles = null;
                
            try
            {
                subFiles = System.IO.Directory.GetFiles(currentDir);

                foreach (string file in subFiles)
                {
                    try
                    {
                        Console.WriteLine(fileParser.ParseFile(file));
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
            }

            catch (UnauthorizedAccessException e)
            {

                Console.WriteLine(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                subDirs = System.IO.Directory.GetDirectories(currentDir);
                foreach (string str in subDirs)
                {
                    Traverse(str);
                }
                if (subDirs.Length == 0)
                {
                    return;
                }
            }

            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
