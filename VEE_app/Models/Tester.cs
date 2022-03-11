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
        private bool numberIsArchived;

        public Tester()
        {
            numbHistory = new List<int>();
            numberIsArchived = false;
        }

        public bool ValidateNumber(int number)
        {
            if (number is < 10 or > 99)
                return false;
            else return true;
        }

        public void AddNumber(int number)
        {
            currentNumber = number;
            numberIsArchived = false;
        }

        public void ArchiveNumber()
        {
            if (!numberIsArchived)
            {
                numbHistory.Insert(0, currentNumber);
                numberIsArchived = true;
            }
        }
    }
}
