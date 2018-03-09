using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.CSharp;
using Microsoft.Win32;

namespace FileCommander000
{
    public class SimRunner
    {
        void RunEfsm()
        {
            //Process.Start("C:\\Users\\georgek\\Documents\\EmbeddedFiniteStateMachine\\EFSM.Sample\\main.exe",
            //    "C:\\\\Users\\\\georgek\\\\Documents\\\\EmbeddedFiniteStateMachine\\\\EFSM.Sample\\\\efsmDebugInterfaceFile\n\n");
            //Process.Start("C:\\Users\\georgek\\Documents\\EmbeddedFiniteStateMachine\\EFSM.Sample\\main.exe",
            //    "C:\\\\Users\\\\georgek\\\\Documents\\\\EmbeddedFiniteStateMachine\\\\EFSM.Sample\\\\efsmDebugInterfaceFile");


            Process.Start("C:\\Users\\georgek\\Documents\\EmbeddedFiniteStateMachine\\EFSM.Sample\\efsm_core.exe",
               "C:\\\\Users\\\\georgek\\\\Documents\\\\EmbeddedFiniteStateMachine\\\\EFSM.Sample\\\\efsmEmbeddedStatusTx"+
               " C:\\\\Users\\\\georgek\\\\Documents\\\\EmbeddedFiniteStateMachine\\\\EFSM.Sample\\\\efsmEmbeddedCommandRx");
        }

        public void Run()
        {
            //Thread runner = new Thread(RunEfsm);
            Task runner = new Task(RunEfsm);
            runner.Start();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Process.Start(msvc.exe, "C:\\Users\\georgek\\Documents\\CompileTest\\CompileTest.c");
            //FileStream controlFile = File.Open("C:\\Users\\georgek\\Documents\\EmbeddedFiniteStateMachine\\EFSM.Sample\\ctrlFile", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //BinaryWriter writer = new BinaryWriter(controlFile);
            //BinaryReader reader = new BinaryReader(controlFile);
            Console.WriteLine("Hello World");

            Console.ReadKey();

            
            //SimRunner simRunner = new SimRunner();

            //simRunner.Run();

            //Console.WriteLine("Please enter text.");

            //bool cont = true;

            //while (cont)
            //{
            //    string entry = Console.ReadLine();

            //    if (entry == "cycle")
            //    {
            //        FileStream controlFile = File.Open("C:\\Users\\georgek\\Documents\\EmbeddedFiniteStateMachine\\EFSM.Sample\\efsmEmbeddedCommandRx", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //        BinaryWriter writer = new BinaryWriter(controlFile);
            //        Console.WriteLine("Cycling");
            //        writer.Write((byte)0x01);
            //        writer.Write((byte)0x00);
            //        writer.Write((byte)0x02);
            //        writer.Write((byte)0x00);
            //        writer.Write((byte)0x00);
            //        controlFile.Close();
            //    }
            //    else if (entry == "restart")
            //    {
            //        FileStream controlFile = File.Open("C:\\Users\\georgek\\Documents\\EmbeddedFiniteStateMachine\\EFSM.Sample\\efsmEmbeddedCommandRx", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //        BinaryWriter writer = new BinaryWriter(controlFile);
            //        Console.WriteLine("restarting");
            //        writer.Write((byte)0x02);
            //        writer.Write((byte)0x00);
            //        controlFile.Close();
            //    }
            //    else if (entry == "exit")
            //    {
            //        Console.WriteLine("Exiting");
            //        cont = false;
            //    }
            //}
        }
    }
}
