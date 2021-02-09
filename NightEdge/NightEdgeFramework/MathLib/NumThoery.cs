using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFrameworks.MathLib
{
    public class NumThoery
    {
        // 使用特性进行类型检查！
        public static int GetGreatCommonDivisor(int num1, int num2)
        {
            int t = num2;
            if (num1 < num2)
            {
                num2 = num1; num1 = t;
            }
            t = num1 % num2;
            if (t == 0)
                return num2;
            return GetGreatCommonDivisor(num2, t);
        }

        public static int[] GetAllPrimeDivisors(int num)
        {
            List<int> list = new List<int>();
            for (int i = 2; i <= num; i++)
            {
                if (num % i == 0)
                {
                    num /= i;
                    list.Add(i);
                    i--;
                }
            }
            return list.ToArray();
        }
    }

}
