using System;

namespace AlpacaVM
{
    class Stack
    {
        private int[] data = new int[65536];
        private int begin = 0;
        private int end = 65535;
        private int count = 0;
        public void PushN(int i)
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
            count++;
        }
        public int TopItem
        {
            get
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
                }
                catch (System.InvalidOperationException e)
                {
                    Console.WriteLine(e.ToString());
                    System.Environment.Exit(1);
                    return 0;
                }
            }
        }

        public int GetTheNthItem(int n)
        {
            return data[end + 1 - n];
        }
        public void CopyTheNthItemOntoTheTop(int n)
        {
            this.PushN(GetTheNthItem(n));
        }
        public void Duplicate()
        {
            this.PushN(TopItem);
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
        public int Pop()
        {
            int result = this.TopItem;
            this.Discard();
            return result;
        }

    }
}
