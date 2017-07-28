namespace AlpacaVM
{
    class VM
    {
        Stack stack;
        Stack callStack = new Stack();
        Heap heap;
        bool end = false;
        System.Collections.Generic.Dictionary<int, int> locations = new System.Collections.Generic.Dictionary<int, int>();
        InstructionSet set;
        int currentLocation = 0;
        private VM() { }
        public VM(Stack _stack,Heap _heap,InstructionSet _set)
        {
            stack = _stack;
            heap = _heap;
            set = _set;
        }

        void run(Instruction i)
        {
            switch (i.Head)
            {
                case InstructionHead.StackOperation:
                    switch (i._StackOperation)
                    {
                        case StackOperation.CopyN:
                            stack.CopyTheNthItemOntoTheTop(i.Number);
                            break;
                        case StackOperation.Discard:
                            stack.Discard();
                            break;
                        case StackOperation.Duplicate:
                            stack.Duplicate();
                            break;
                        case StackOperation.PushN:
                            stack.PushN(i.Number);
                            break;
                        case StackOperation.SlideN:
                            //Undone
                            break;
                        case StackOperation.Swap:
                            stack.Swap();
                            break;
                    }
                    currentLocation++;
                    break;

                case InstructionHead.ArithmeticOperation:
                    int a, b;
                    switch (i._ArithmeticOperation)
                    {
                        case ArithmeticOperation.Addition:
                            b = stack.Pop();
                            a = stack.Pop();
                            stack.PushN(a + b);
                            break;
                        case ArithmeticOperation.Subtraction:
                            b = stack.Pop();
                            a = stack.Pop();
                            stack.PushN(a - b);
                            break;
                        case ArithmeticOperation.Multiplication:
                            b = stack.Pop();
                            a = stack.Pop();
                            stack.PushN(a * b);
                            break;
                        case ArithmeticOperation.IntegerDivision:
                            b = stack.Pop();
                            if (b == 0)
                            {
                                System.Console.WriteLine();
                                System.Console.WriteLine("Runtime Error: Zero cannot be a divisor. ");
                                System.Environment.Exit(1);
                            }
                            a = stack.Pop();
                            stack.PushN(a / b);
                            break;
                        case ArithmeticOperation.Modulo:
                            b = stack.Pop();
                            if (b == 0)
                            {
                                System.Console.WriteLine();
                                System.Console.WriteLine("Runtime Error: Zero cannot be a divisor. ");
                                System.Environment.Exit(1);
                            }
                            a = stack.Pop();
                            stack.PushN(a % b);
                            break;
                    }
                    currentLocation++;
                    break;
                case InstructionHead.HeapOperation:
                    switch (i._HeapOperation)
                    {
                        case HeapOperation.Store:
                            heap.Store();
                            break;
                        case HeapOperation.Retrieve:
                            heap.Retrieve();
                            break;
                    }
                    currentLocation++;
                    break;
                case InstructionHead.IOOperation:
                    switch (i._IOOperation)
                    {
                        case IOOperation.InputAsChar:
                            stack.PushN(System.Console.Read());
                            break;
                        case IOOperation.InputAsNumber:
                            stack.PushN(System.Convert.ToInt32(System.Console.ReadLine()));
                            break;
                        case IOOperation.OutputAsChar:
                            System.Console.Write((char)stack.Pop());
                            break;
                        case IOOperation.OutputAsNumber:
                            System.Console.Write(stack.Pop());
                            break;
                    }
                    currentLocation++;
                    break;
                case InstructionHead.FlowOperation:
                    switch (i._FlowOperation)
                    {
                        case FlowOperation.CallSubN:
                            callStack.PushN(currentLocation + 1);
                            currentLocation = locations[i.Number];
                            break;
                        case FlowOperation.EndProgram:
                            end = true;
                            break;
                        case FlowOperation.EndSub:
                            currentLocation = callStack.TopItem;
                            break;
                        case FlowOperation.JumpIfNegN:
                            if (stack.Pop() < 0)///////
                                currentLocation = locations[i.Number];
                            else
                                currentLocation++;
                            break;
                        case FlowOperation.JumpIfZeroN:
                            if (stack.Pop() == 0)///////
                                currentLocation = locations[i.Number];
                            else
                                currentLocation++;
                            break;
                        case FlowOperation.JumpN:
                            currentLocation = locations[i.Number];
                            break;
                        case FlowOperation.MakeLocationN:
                            currentLocation++;
                            break;
                    }

                    break;
            }
        }

        void initialize()
        {
            bool endFlag = false;
            for (int i = 0; i < set.Count; i++)
            {
                if (set[i].Head == InstructionHead.FlowOperation)
                {
                    if (set[i]._FlowOperation == FlowOperation.MakeLocationN)
                    {
                        locations.Add(set[i].Number, i);
                    }
                    if (set[i]._FlowOperation == FlowOperation.EndProgram)
                    {
                        endFlag = true;
                    }
                }
            }
            if (!endFlag)
            {
                System.Console.WriteLine("The program contains no end instruction(\"马马马\" or \"河蟹\"). Please check it again. ");
                System.Environment.Exit(1);
            }
        }

        public int Start()
        {
            initialize();
            while (!end)
            {
                this.run(set[currentLocation]);
            }
            return 0;
        }
    }
}
