using System;
using System.Collections.Generic;
using System.Text;

namespace simplex
{
    internal interface ISimplex
    {
        void Inti_creator();
        public void enter_cj_values();
        public void enter_values();
        public void claculate_zj_cj();
        public void min();
        public void max();
        public void ratio (int index);
        public void new_table(int index,string operation,bool cond );
        public void min_result();
        public void max_result();
    }
}
