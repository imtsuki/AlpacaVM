using System.IO;

namespace AlpacaVM
{
    class StreamBundle
    {
        StreamReader stream;
        public int Count = 0;
        public StreamBundle(StreamReader s)
        {
            stream = s;
        }
        public int Read()
        {
            
            int result;
            while (true)
            {
                result = stream.Read();
                Count++;
                if (result == Word.草 || result == Word.泥 || result == Word.马 || result == Word.河 || result == Word.蟹 || result == -1)
                    break;
            }
            System.Console.WriteLine(result);
            return result;
        }
    }
    class Compiler
    {
        StreamBundle stream;
        public InstructionSet Set;
        int ReadInteger()
        {
            int sign = stream.Read();
            if (sign == Word.草)
                sign = 1;
            else
                sign = -1;
            int result = 0;
            int temp = stream.Read();
            while (temp != Word.马)
            {
                temp = (temp == Word.泥) ? 1 : 0;
                result = result * 2 + temp;
                temp = stream.Read();
            }
            result *= sign;
            return result;
        }
        int ReadUnsignedInteger()
        {
            int result = 0;
            int temp = stream.Read();
            while (temp != Word.马)
            {
                temp = (temp == Word.泥) ? 1 : 0;
                result = result * 2 + temp;
                temp = stream.Read();
            }
            return result;
        }
        public Compiler(StreamReader sr,InstructionSet s)
        {
            int number;
            stream = new StreamBundle(sr);
            Set = s;
            int word = 0;
            int stat = -1;
            bool isFirstReachEnd = true;
            try
            {
                while (stat != -2)
                {
                    System.Console.WriteLine(stat);
                    word = stream.Read();
                    
                    if (word == -1)
                    {
                        if (isFirstReachEnd)
                        {
                            isFirstReachEnd = false;
                        }
                        else
                        {
                            if (stat == -1)
                            {
                                stat = -2;
                                continue;
                            }
                            else
                            {
                                throw new System.Exception();
                            }
                        }
                    }
//                    if (word != Word.草 && word != Word.泥 && word != Word.马 && word != Word.河 && word != Word.蟹 && word != -1)
//                        continue;
                    
                    switch (stat)
                    {
                        case -1:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 0;
                                    break;
                                case Word.泥:
                                    stat = 1;
                                    break;
                                case Word.马:
                                    stat = 2;
                                    break;
                                case Word.河:
                                    stat = 39;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 0:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 3;
                                    break;
                                case Word.泥:
                                    stat = 4;
                                    break;
                                case Word.马:
                                    stat = 5;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 1:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 11;
                                    break;
                                case Word.泥:
                                    stat = 12;
                                    break;
                                case Word.马:
                                    stat = 13;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 2:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 29;
                                    break;
                                case Word.泥:
                                    stat = 30;
                                    break;
                                case Word.马:
                                    stat = 31;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 3:
                            number = ReadInteger();

                            //Push
                            stat = -1;
                            break;
                        case 4:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 6;
                                    break;
                                case Word.马:
                                    stat = 7;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 5:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 8;
                                    break;
                                case Word.泥:
                                    stat = 9;
                                    break;
                                case Word.马:
                                    stat = 10;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 6:
                            //CopyN
                            number = ReadInteger();
                            stat = -1;
                            break;
                        case 7:
                            //Slide
                            number = ReadInteger();
                            stat = -1;
                            break;
                        case 8:
                            //Duplicate
                            stat = -1;
                            break;
                        case 9:
                            //Swap
                            stat = -1;
                            break;
                        case 10:
                            //Discard
                            stat = -1;
                            break;
                        case 11:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 14;
                                    break;
                                case Word.泥:
                                    stat = 15;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 12:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 16;
                                    break;
                                case Word.泥:
                                    stat = 17;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 13:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 18;
                                    break;
                                case Word.泥:
                                    stat = 19;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 14:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 20;
                                    break;
                                case Word.泥:
                                    stat = 21;
                                    break;
                                case Word.马:
                                    stat = 22;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 15:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 23;
                                    break;
                                case Word.泥:
                                    stat = 24;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 16:
                            //Store
                            stat = -1;
                            break;
                        case 17:
                            //Retrieve
                            stat = -1;
                            break;
                        case 18:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 25;
                                    break;
                                case Word.泥:
                                    stat = 26;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 19:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 27;
                                    break;
                                case Word.泥:
                                    stat = 28;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 20:
                            //Addition
                            stat = -1;
                            break;
                        case 21:
                            //Subtraction
                            stat = -1;
                            break;
                        case 22:
                            //Multiplication
                            stat = -1;
                            break;
                        case 23:
                            //IntegerDivision
                            stat = -1;
                            break;
                        case 24:
                            //Modulo
                            stat = -1;
                            break;
                        case 25:
                            //OutputAsChar
                            stat = -1;
                            break;
                        case 26:
                            //OutputAsNumber
                            stat = -1;
                            break;
                        case 27:
                            //InputAsChar
                            stat = -1;
                            break;
                        case 28:
                            //OutputAsNumber
                            stat = -1;
                            break;
                        case 29:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 32;
                                    break;
                                case Word.泥:
                                    stat = 33;
                                    break;
                                case Word.马:
                                    stat = 34;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 30:
                            switch (word)
                            {
                                case Word.草:
                                    stat = 35;
                                    break;
                                case Word.泥:
                                    stat = 36;
                                    break;
                                case Word.马:
                                    stat = 37;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 31:
                            switch (word)
                            {
                                case Word.马:

                                    stat = 38;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 32:
                            //MakeLocation
                            number = ReadUnsignedInteger();
                            stat = -1;
                            break;
                        case 33:
                            //CallSub
                            number = ReadUnsignedInteger();
                            stat = -1;
                            break;
                        case 34:
                            //Jump
                            number = ReadUnsignedInteger();
                            stat = -1;
                            break;
                        case 35:
                            //JumpIfZero
                            number = ReadUnsignedInteger();
                            stat = -1;
                            break;
                        case 36:
                            //JumpIfNeg
                            number = ReadUnsignedInteger();
                            stat = -1;
                            break;
                        case 37:
                            //EndSub
                            stat = -1;
                            break;
                        case 38:
                            //EndProgram
                            stat = -1;
                            break;
                        case 39:
                            switch (word)
                            {
                                case Word.蟹:
                                    stat = 38;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                    }
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Syntax Error!"+stat);

                System.Environment.Exit(1);
            }
            
            
        }
    }
}
