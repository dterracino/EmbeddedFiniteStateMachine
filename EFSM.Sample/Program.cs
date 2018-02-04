using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileCommander000
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream controlFile = File.Open("ctrlFile", FileMode.Open);

            Console.WriteLine("Please enter text.");

            bool cont = true;

            while (cont)
            {
                string entry = Console.ReadLine();

                if (entry == "cycle")
                {
                    Console.WriteLine("Cycling");

                }
                else if (entry == "exit")
                {
                    Console.WriteLine("Exiting");
                    cont = false;
                }
            }            
        }
    }
}
