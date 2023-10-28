using System;

namespace simplex
{
    internal class Program
    {
        public static void Main()
        {
            Simplex problem = new Simplex();
            problem.enter_values();
            problem.enter_cj_values();
            problem.claculate_zj_cj();
            problem.max();
            problem.max_result();

            //int[] aa = new int[] { 8848481, 47472, 44, 455, 5, 6 };
            //int[] bb = new int[aa.Length];
            //Array.Copy(aa, 0, bb, 0, aa.Length);
            //Array.Sort(bb);
            //int x = bb[0];
            //int index = Array.IndexOf(aa, x);
            //for (int i = 0; i < aa.Length; i++)
            //{
            //    Console.WriteLine(bb[i]);
            //}
            //Console.WriteLine(index);
        }
    }
}
