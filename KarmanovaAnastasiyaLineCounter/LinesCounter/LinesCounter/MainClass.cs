using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinesCounter
{
    class MainClass
    {
        private static Controller controller;
        public static void Main(string[] args)
        {
            controller = new Controller(args);
        }
    }
}
