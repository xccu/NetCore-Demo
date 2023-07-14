using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Algorithm;

//原文链接：https://blog.csdn.net/ylyg050518/article/details/75215332

public static class Combination
{
    public static void run()
    {
        combine(4,2);
    }

    private static List<List<int>> result = new List<List<int>>();
   
    public static List<List<int>> combine(int n, int k)
    {
        if (n == 0 || k <= 0 || k > n)
            return result;
        Stack<int> c = new Stack<int>();
        generateCombinations(n, k, 1, c);
        return result;
    }

    private static void generateCombinations(int n, int k, int start, Stack<int> c)
    {
        if (c.Count() == k)
        {
            result.Add(new List<int>(c));
            return;
        }
        for (int i = start; i <= n; i++)
        {
            c.Push(i);
            generateCombinations(n, k, i + 1, c);
            c.Pop();
        }
    }
}
