using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project1.view;

namespace Project1.controller
{
    class MainClass
    {
        private static Controller controller;
        private static ConsoleApp consoleApp;
        public static void Main(string[] args)
        {
            controller = new Controller(args);
            consoleApp  = new ConsoleApp(controller);
        }
    }
}
//decrypt output.bin aes input.key.txt result.txt
//encrypt input.txt aes output.bin