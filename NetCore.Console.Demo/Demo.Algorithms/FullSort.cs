using System;

namespace Demo.Algorithm;

public static class FullSort
{
    public static void run()
    {
        int[] arr = { 1, 2, 3 };//这里可以放你的变量例如Nums = new int[] { a, b, c, d, e };赋值前确保变量有值。
        Permutation(arr, 0, arr.Length);

        foreach (var item in result)
        {
            for (int i = 0; i < item.Length; i++)
                Console.Write(item[i]);
            Console.WriteLine();
        }
    }
    public static List<int[]> result = new List<int[]>();

    /// <summary>
    /// 递归实现全排序并输出
    /// </summary>
    /// <param name="arr">待排序的字符数组</param>
    /// <param name="m">输出字符数组的起始位置</param>
    /// <param name="n">输出字符数组的长度</param>
    public static void Permutation(int[] arr, int m, int n)
    {
        if (m < n - 1)
        {
            Console.WriteLine("P m:" + m + " n:" + n);
            Permutation(arr, m + 1, n);
            for (var i = m + 1; i < n; i++)
            {
                
                exchange(ref arr[m], ref arr[i]);
                Console.WriteLine("P m:" + m + " n:" + n);
                Permutation(arr, m + 1, n);
                exchange(ref arr[m], ref arr[i]);                  
            }
        }
        else
        {
            Console.WriteLine("O m:" + m + " n:" + n);
            foreach (var item in arr)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }
    }

    public static void exchange(ref int a, ref int b)
    {
        int t = a;
        a = b;
        b = t;
    }
}
