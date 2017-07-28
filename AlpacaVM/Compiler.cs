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
            //System.Console.WriteLine(result);
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
//            bool isFirstReachEnd = true;
            try
            {
                while (stat != -2)
                {
//                    System.Console.WriteLine(stat);
                    word = stream.Read();
                    
                    if (word == -1)
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
                                    number = ReadInteger();
                                    Set.Add(new Instruction(StackOperation.PushN,number));
                                    //PushN
                                    stat = -1;
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
                        case 4:
                            switch (word)
                            {
                                case Word.草:
                                    //CopyN
                                    number = ReadInteger();
                                    Set.Add(new Instruction(StackOperation.CopyN, number));
                                    stat = -1;
                                    break;
                                case Word.马:
                                    //SlideN
                                    number = ReadInteger();
                                    Set.Add(new Instruction(StackOperation.SlideN, number));
                                    stat = -1;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 5:
                            switch (word)
                            {
                                case Word.草:
                                    //Duplicate
                                    Set.Add(new Instruction(StackOperation.Duplicate));
                                    stat = -1;
                                    break;
                                case Word.泥:
                                    //Swap
                                    Set.Add(new Instruction(StackOperation.Swap));
                                    stat = -1;
                                    break;
                                case Word.马:
                                    //Discard
                                    Set.Add(new Instruction(StackOperation.Discard));
                                    stat = -1;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
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
                                    //Store
                                    Set.Add(new Instruction(HeapOperation.Store));
                                    stat = -1;
                                    break;
                                case Word.泥:
                                    //Retrieve
                                    Set.Add(new Instruction(HeapOperation.Retrieve));
                                    stat = -1;
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
                                    //Addition
                                    Set.Add(new Instruction(ArithmeticOperation.Addition));
                                    stat = -1;
                                    break;
                                case Word.泥:
                                    //Subtraction
                                    Set.Add(new Instruction(ArithmeticOperation.Subtraction));
                                    stat = -1;
                                    break;
                                case Word.马:
                                    //Multiplication
                                    Set.Add(new Instruction(ArithmeticOperation.Multiplication));
                                    stat = -1;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 15:
                            switch (word)
                            {
                                case Word.草:
                                    //IntegerDivision
                                    Set.Add(new Instruction(ArithmeticOperation.IntegerDivision));
                                    stat = -1;
                                    break;
                                case Word.泥:
                                    //Modulo
                                    Set.Add(new Instruction(ArithmeticOperation.IntegerDivision));
                                    stat = -1;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 18:
                            switch (word)
                            {
                                case Word.草:
                                    //OutputAsChar
                                    Set.Add(new Instruction(IOOperation.OutputAsChar));
                                    stat = -1;
                                    break;
                                case Word.泥:
                                    //OutputAsNumber
                                    Set.Add(new Instruction(IOOperation.OutputAsNumber));
                                    stat = -1;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 19:
                            switch (word)
                            {
                                case Word.草:
                                    //InputAsChar
                                    Set.Add(new Instruction(IOOperation.InputAsChar));
                                    stat = -1;
                                    break;
                                case Word.泥:
                                    //InputAsNumber
                                    Set.Add(new Instruction(IOOperation.InputAsNumber));
                                    stat = -1;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 29:
                            switch (word)
                            {
                                case Word.草:
                                    //MakeLocation
                                    number = ReadUnsignedInteger();
                                    Set.Add(new Instruction(FlowOperation.MakeLocationN,number));
                                    stat = -1;
                                    break;
                                case Word.泥:
                                    //CallSub
                                    number = ReadUnsignedInteger();
                                    Set.Add(new Instruction(FlowOperation.CallSubN, number));
                                    stat = -1;
                                    break;
                                case Word.马:

                                    //Jump
                                    number = ReadUnsignedInteger();
                                    Set.Add(new Instruction(FlowOperation.JumpN, number));
                                    stat = -1;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 30:
                            switch (word)
                            {
                                case Word.草:
                                    //JumpIfZero
                                    number = ReadUnsignedInteger();
                                    Set.Add(new Instruction(FlowOperation.JumpIfZeroN, number));
                                    stat = -1;
                                    break;
                                case Word.泥:
                                    //JumpIfNeg
                                    number = ReadUnsignedInteger();
                                    Set.Add(new Instruction(FlowOperation.JumpIfNegN, number));
                                    stat = -1;
                                    break;
                                case Word.马:
                                    //EndSub
                                    Set.Add(new Instruction(FlowOperation.EndSub));
                                    stat = -1;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 31:
                            switch (word)
                            {
                                case Word.马:
                                    //EndProgram
                                    Set.Add(new Instruction(FlowOperation.EndProgram));
                                    stat = -1;
                                    break;
                                default:
                                    throw new System.Exception();
                            }
                            break;
                        case 39:
                            switch (word)
                            {
                                case Word.蟹:
                                    //EndProgram
                                    Set.Add(new Instruction(FlowOperation.EndProgram));
                                    stat = -1;
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
                System.Console.WriteLine("Syntax Error! Error Code " + stat);
                System.Environment.Exit(1);
            }
            
        }
    }
}
