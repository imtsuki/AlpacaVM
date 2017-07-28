namespace AlpacaVM
{
    class Instruction
    {
        public InstructionHead Head;
        public StackOperation _StackOperation;
        public HeapOperation _HeapOperation;
        public ArithmeticOperation _ArithmeticOperation;
        public IOOperation _IOOperation;
        public FlowOperation _FlowOperation;
        public int Number;
        public Instruction(InstructionHead i)
        {
            Head = i;
        }
        public Instruction(StackOperation s) : this(InstructionHead.StackOperation)
        {
            _StackOperation = s;
        }
        public Instruction(StackOperation s,int number) : this(s)
        {
            Number = number;
        }
        public Instruction(HeapOperation h) : this(InstructionHead.HeapOperation)
        {
            _HeapOperation = h;
        }
        public Instruction(IOOperation i) : this(InstructionHead.IOOperation)
        {
            _IOOperation = i;
        }
        public Instruction(ArithmeticOperation a) : this(InstructionHead.ArithmeticOperation)
        {
            _ArithmeticOperation = a;
        }
        public Instruction(FlowOperation f) : this(InstructionHead.FlowOperation)
        {
            _FlowOperation = f;
        }
        public Instruction(FlowOperation f,int number) : this(f)
        {
            Number = number;

        }
    }
}
