using System;

namespace AlpacaVM
{
    class Stack
    {
        private int[] data = new int[65536];
        private UInt32 begin = 0;
        private UInt32 end = 65535;
        private int count = 0;
        public void Push(int i)
        {
            if (end == 65535)
            {
                end = 0;
                data[end] = i;
            }
            else
            {
                data[++end] = i;
            }
        }
        public int GetTheTopItem()
        {
            try
            {
                if (count != 0)
                {
                    return data[end];
                }
                else
                {
                    throw new System.InvalidOperationException();
                }
            }catch(System.InvalidOperationException e)
            {
                Console.WriteLine(e.ToString());
                System.Environment.Exit(1);
                return 0;
            }
        }
        public int GetTheNthItem(UInt32 n)
        {
            return data[end + 1 - n];
        }
        public void CopyTheNthItemOntoTheTop(UInt32 n)
        {
            this.Push(GetTheNthItem(n));
        }
        public void CopyTheTopItemOntoTheTop(UInt32 n)
        {
            this.Push(GetTheTopItem());
        }
        public void Swap()
        {
            try
            {
                if (count >= 2)
                {
                    int temp = data[end];
                    data[end] = data[end - 1];
                    data[end - 1] = temp;
                }
                else
                {
                    throw new System.InvalidOperationException();
                }
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine(e.ToString());
                System.Environment.Exit(1);
            }
        }
    public void Discard()
        {
            try
            {
                if (count > 0)
                {
                    end--;
                    count--;
                }
                else
                {
                    throw new System.InvalidOperationException();
                }
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine(e.ToString());
                System.Environment.Exit(1);
            }
        }


    }
}
