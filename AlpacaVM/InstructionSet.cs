namespace AlpacaVM
{
    class InstructionSet
    {
        System.Collections.ArrayList set = new System.Collections.ArrayList();
//        Instruction[] data = new Instruction[6];
        public Instruction NextInstruction()
        {
            return new Instruction(InstructionHead.ArithmeticOperation);//占位
        }

        public Instruction GetInstruction()
        {
            return new Instruction(InstructionHead.ArithmeticOperation);//占位
        }

        public Instruction this[int index] => (Instruction)set[index];
        public int Count => set.Count;
        public void Add(Instruction i)
        {
            set.Add(i);
        }
    }
}
