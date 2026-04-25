using System;
using System.Collections.Generic;
namespace DelegateParameters
{
    public delegate bool NumberCheck(int number);
    public class NumberFilters
    {
        public static bool IsEven(int n) => n % 2 == 0;
        public static bool IsOdd(int n) => n % 2 != 0;
    }
}