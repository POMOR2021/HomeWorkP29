using System;
using System.Runtime.InteropServices;

namespace UnsafeArrayCopy
{
    class Program
    {
        static unsafe void CopyArrayUnsafe(int[] sour, int[] dest)
        {
            if (sour == null || dest == null || sour.Length > dest.Length)
            {
                throw new ArgumentException("Ложные аргументы.");
            }

            fixed (int* sourPt = sour)
            fixed (int* destPt = dest)
            {
                for (int i = 0; i < sour.Length; i++)
                {
                    *(destPt + i) = *(sourPt + i);
                }
            }
        }

        static void Main(string[] args)
        {
            int[] sourArr = { 1, 2, 3, 4, 5 };
            int[] destArr = new int[10]; 

            try
            {
                unsafe
                {
                    CopyArrayUnsafe(sourArr, destArr);
                }

                Console.WriteLine("Copy massive:");
                foreach (int val in destArr)
                {
                    Console.Write(val + " ");
                }
                Console.WriteLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}