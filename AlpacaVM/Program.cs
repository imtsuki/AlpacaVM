using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlpacaVM
{

    class Program
    {


        static Encoding GetEncoding(FileStream f)
        {
            BinaryReader br = new BinaryReader(f);
            Byte[] buffer = br.ReadBytes(2);
            f.Position = 0;
            if (buffer[0] >= 0xEF)
            {
                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                {
                    return Encoding.UTF8;
                }
                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                {
                    return Encoding.BigEndianUnicode;
                }
                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                {
                    return Encoding.Unicode;
                }
                else
                {
                    return Encoding.Default;
                }
            }
            else
            {
                return Encoding.Default;
            }

        }

        static void Main(string[] args)
        {
            FileStream f;
            StreamReader s;
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("No input files. ");
                    Environment.Exit(1);
                }
                f = new FileStream(args[0], FileMode.Open, FileAccess.Read);
                s = new StreamReader(f, GetEncoding(f));
                Compiler c = new Compiler(s, new InstructionSet());
                Stack stack = new Stack();
                VM vm = new VM(stack, new Heap(stack), c.Set);
                Console.WriteLine("The program ended successfully with exit code " + vm.Start()+ ". ");
               /* FileStream t = new FileStream("t.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter tt = new StreamWriter(t);
                for(int i = 0; i <= 40; i++)
                {
                    tt.WriteLine("                case "+i+":");
                    tt.WriteLine("                    break;");
                }*/

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());// "File \"" + inputFilePath + "\" not found.");
                System.Environment.Exit(1);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString()); //Console.WriteLine("File \"" + inputFilePath + "\" is not reachable.");
                System.Environment.Exit(1);
            }

        }
    }
}
