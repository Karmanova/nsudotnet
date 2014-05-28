using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinesCounter
{
    class Controller
    {
        private string[] args;
        private List<string> extensionList;
        private TreeTraverser treeTraverser;

        public Controller(string[] args)
        {
            this.args = args;
            extensionList = new List<string>();
            try
            {
                ArgParser(args);
                
                treeTraverser = new TreeTraverser(extensionList);
            }
            catch (Exception)
            {
                return;
            }
            foreach (string str in extensionList)
            {
                System.Console.WriteLine(str);
            }
            
        }

        private void ArgParser(string[] args)
        {
            foreach (string str in args)
            {
                System.Console.WriteLine(str);
                if (!str.StartsWith("*."))
                {
                    throw new Exception("Wrong format of extension!");
                }
                extensionList.Add(str.Substring(1, str.Length - 1));
            }
        }
    }
}
