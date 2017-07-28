namespace AlpacaVM
{
    class Heap
    {
        Stack stack;
        int[] data = new int[65536];
        public Heap(Stack s)
        {
            stack = s;
        }
        public void Store()
        {
            int x = stack.Pop();
            int y = stack.Pop();
            data[y] = x;
        }
        public void Retrieve()
        {
            int y = stack.Pop();
            stack.PushN(data[y]);
        }
    }
}
