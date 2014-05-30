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

            object o1 = new string("awesome example".ToCharArray());
            object o2 = new string("awesome example".ToCharArray());
            System.Console.Write(o1 == o2);
            System.Console.Write(o1.Equals(o2));
            Console.Read();
        }

        
    }
}
//decrypt output.bin aes input.key.txt result.txt
//encrypt input.txt aes output.bin