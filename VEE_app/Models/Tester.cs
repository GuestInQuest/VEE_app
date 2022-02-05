using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VEE_app.Models
{
    public class Tester
    {
        public int currentNumber { get; set; }
        public List<int> numbHistory { get; set; }

        public Tester()
        {
            numbHistory = new List<int>();
        }

        public bool AddNumber(int numb)
        {
            if (numb is < 10 or > 99)
                return false;
            currentNumber = numb;
            return true;
        }

        public void ArchiveNumber()
        {
            if(currentNumber != -1)
                numbHistory.Insert(0, currentNumber);
            currentNumber = -1;
        }
    }
}
