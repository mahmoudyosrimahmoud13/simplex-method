using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace simplex
{
    internal class Simplex : ISimplex
    {
        private int no_inequalies;
        private double[,] rows;
        private double[,] temp_rows;
        private double[] arr_zj;
        private double[] cj;
        private double[] Bv;
        private double[] cj_zj_sub;
        private int index_min;
        private int index_max;
        private int ratio_index;
        private double target;

        //Make method to create arrays of table:
        public void Inti_creator ()
        {
            //Calculate the number of columns:
            int no_columns = 2 * no_inequalies + 1;
            //Create a multidimensional array to store rows:
            rows = new double[no_inequalies, no_columns];
            


        }
        public void dimentional_copy(double[,] source, double [,] destination)
        {
            int no_columns = 2 * no_inequalies + 1;

            for (int i = 0; i < no_inequalies; i++, i++)
            {
                for (int j = 0; j<no_columns;j++)
                {
                    destination[i,j] = source[i,j];
                }
            }
        }
        public void enter_cj_values()
        {
            int no_columns = 2 * no_inequalies + 1;
            //creating loop for cj array
            cj = new double[no_columns-1];
            for (int i = 0; i < no_columns-1; i++)
            {
                Console.WriteLine("Enter the value " +(i+1) + " of cj:");
                cj[i] = int.Parse(Console.ReadLine());

            }
            Bv = new double[no_inequalies];

            
        }
        public void enter_values()
        {
            Console.WriteLine("Enter the number of inequalies:");
            no_inequalies = int.Parse(Console.ReadLine());
            int no_columns = 2 * no_inequalies + 1;
            

            

            //intialze the creator:
            Inti_creator();


            //Creating loop to enter the variables:
            for (int i = 0; i < no_inequalies;i++)
            {
                //inner loop to enter var in each cell
                for(int j=0; j< no_columns;j++)
                {
                    Console.WriteLine("Enter value " +( j + 1) + " of an inequality " + (i + 1));
                    int var = int.Parse(Console.ReadLine());
                    rows[i, j] = var;
                }
            }
        }

        public void claculate_zj_cj()
        {
            int no_columns = 2 * no_inequalies + 1;
            //creating empty var for zj , creating empy row array for zj
            
            arr_zj = new double[no_columns];

            //loop for calculating zj
            for (int i = 0; i < no_columns; i++)
            {
                double zj = 0;
                for (int j = 0; j < no_inequalies; j++)
                {
                    zj = zj + (rows[j, i] *Bv[j]);
                }
                arr_zj[i] = zj;
            }


            //calculate cj-zj
            cj_zj_sub = new double[cj.Length];
            for(int i = 0; i < cj.Length-1; i++)
            {
                cj_zj_sub[i] = cj[i] - arr_zj[i];
            }
            

        }

        public void min()
        {
            //creating temprory array

            double[] cj_zj = new double[cj.Length];
            Array.Copy(cj_zj_sub , 0, cj_zj, 0, cj.Length);
            Array.Sort(cj_zj_sub);

            Console.WriteLine("-------------------------------\n");
            for (int i = 0; i < arr_zj.Length; i++)
            {
                
                Console.Write(arr_zj[i] +" ");
                
            }
            Console.WriteLine("-------------------------------\n");

            Console.WriteLine("min ============>" + cj_zj_sub[0]);

            index_min = Array.IndexOf(cj_zj, cj_zj_sub[0]);
            Array.Copy(cj_zj,0, cj_zj_sub,0,cj_zj_sub.Length);

            
            Console.WriteLine("min index = "+index_min);
            ratio(index_min);
        }

        public void max()
        {
            //creating temprory array

            double[] cj_zj = new double[cj.Length];
            Array.Copy(cj_zj_sub, 0, cj_zj, 0, cj.Length);
            Array.Sort(cj_zj_sub);

            Console.WriteLine("-------------------------------\n");
            for (int i = 0; i < arr_zj.Length; i++)
            {

                Console.Write(arr_zj[i] + " ");

            }
            Console.WriteLine("-------------------------------\n");

            Console.WriteLine("max ============>" + cj_zj_sub[cj_zj_sub.Length-1]);

            index_max = Array.IndexOf(cj_zj, cj_zj_sub[cj_zj_sub.Length-1]);
            Array.Copy(cj_zj, 0, cj_zj_sub, 0, cj_zj_sub.Length);


            Console.WriteLine("max index = " + index_max);
            ratio(index_max);
        }

        public void ratio(int index)
        {
            int no_columns = no_inequalies * 2 + 1;

            double [] ratio = new double[no_inequalies];

            for (int i = 0; i < no_inequalies; i++)
            {
                double x;
                // Divide by zero checker:
                Console.WriteLine("real index = "+ index);
                if (rows[i, index] == 0)
                {
                     x = 10000000000;
                }
                else
                {
                     x = rows[i, no_columns-1 ] / rows[i, index];
                }

                if (x>0)
                {
                    ratio[i] = x;
                }
                else
                {
                    //putting it = 10000000 to discard it if it negative
                    ratio[i] = 10000000;
                }
                
            }

            //temp ratio
            double[] temp_ratio = new double[ratio.Length];
            Array.Copy(ratio, 0, temp_ratio, 0, ratio.Length);
            Array.Sort(ratio);
            ratio_index = Array.IndexOf(temp_ratio, ratio[0]);
            ///
           
            Console.WriteLine("lower ratio = " + ratio[0]);
            Array.Copy(temp_ratio, 0, ratio, 0, ratio.Length);
            Console.WriteLine("index =" + ratio_index);

            Console.WriteLine("------------------------------------------=====>>rows before target");
            test_print(rows);
            ////
            target = 0;
            target = rows[index,ratio_index];

            ////
            Console.WriteLine("index =" + ratio_index);
            Console.WriteLine("taeget="+target);

            ///////
            test_print(rows);
        }

        

        public void min_result()
        {
            if(cj_zj_sub[index_min] >= 0)
            {
                
                Console.WriteLine("-----------------------------------------\n Result:");
                Console.WriteLine(arr_zj[arr_zj.Length-1]);
            }
            else
            {
                new_table(index_min,"min",true);
            }
        }

        public void max_result()
        {
            
            if (index_max <= 0)
            {
                new_table (index_max,"max",false);
            }
            else
            {
                new_table(index_max,"max",true);
            }
        }
        public void new_table(int index,string operation , bool cond)
        {
            //temp rows array:
            int no_columns = no_inequalies * 2 + 1;
            temp_rows = new double[no_inequalies,no_columns] ;
            
            //copy:
            dimentional_copy(rows,temp_rows);

            //creating the new row:
            for(int i = 0; i < no_columns; i++)
            {
                double temp = rows[ratio_index,i];
                temp_rows[ratio_index, i] = temp / target;
                rows[ratio_index, i] = temp / target;
            }
            Bv[ratio_index] = cj[index];
            Console.WriteLine("bv----->" + Bv[ratio_index]);
            Console.WriteLine("pre temp");
            test_print(temp_rows);
            Console.WriteLine("================================");
            for(int i = 0; i < no_inequalies; i++)
            {
                //if the new row then continue
                if (i == ratio_index)
                {
                    continue;
                }

                for (int j = 0; j < no_columns; j++)
                {
                    
                    temp_rows[i, j] = rows[i, j] - (rows[i, index] * temp_rows[ratio_index, j]);

                }

            }
            Console.WriteLine("--------temp:");
            test_print(temp_rows);
            dimentional_copy(temp_rows, rows);
            Console.WriteLine("----------rows:");
            test_print(rows);
            
            if(cond == true)
            {
                if (operation == "min")
                {
                    claculate_zj_cj();
                    min();
                    min_result();
                }
                else if (operation == "max")
                {
                    claculate_zj_cj();
                    max();
                    max_result();
                }
            }
            else if (cond == false)
            {
                claculate_zj_cj();
                if (operation == "min")
                {
                    min();
                }
                else if (operation == "max")
                {
                    max();
                }
                claculate_zj_cj();
                Console.WriteLine("-----------------------------------------\n Result:");
                Console.WriteLine(arr_zj[arr_zj.Length - 1]);

            }


        }
        public void test_print (double[,] arr)
        {

            int no_columns = no_inequalies * 2 + 1;
            for(int i = 0; i < no_inequalies; i++ )
            {
                Console.WriteLine("--------------------------\n");
                for (int j = 0; j <no_columns; j++)
                {
                    Console.Write(arr[i,j] + " ");
                }
                
            }
            
            
        }
    }
}
