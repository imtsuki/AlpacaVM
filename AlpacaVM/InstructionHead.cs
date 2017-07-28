namespace AlpacaVM
{
    enum InstructionHead
    {
        StackOperation = 0,
        HeapOperation = 1,
        ArithmeticOperation = 2,
        IOOperation = 3,
        FlowOperation = 4,
    };
    enum StackOperation
    {
        PushN = 0,
        Duplicate = 1,
        CopyN = 2,
        Swap = 3,
        Discard = 4,
        SlideN = 5,
    };
    enum HeapOperation
    {
        Store = 0,
        Retrieve = 1,
    }
    enum ArithmeticOperation
    {
        Addition = 0,
        Subtraction = 1,
        Multiplication = 2,
        IntegerDivision = 3,
        Modulo = 4,
    }
    enum IOOperation
    {
        OutputAsChar = 0,
        OutputAsNumber = 1,
        InputAsChar = 2,
        InputAsNumber = 3,

    }
    enum FlowOperation
    {
        MakeLocationN = 0,
        CallSubN = 1,
        JumpN = 2,
        JumpIfZeroN = 3,
        JumpIfNegN = 4,
        EndSub = 5,
        EndProgram = 6,
    }
}
