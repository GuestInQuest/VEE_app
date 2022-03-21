using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VEE_app.Models
{
    public class NumberMemorizer
    {
        public int CurrentNumber { get; protected set; }
        public int[] NumberHistory
        {
            get { return numberHistory.ToArray(); }
        }

        protected List<int> numberHistory;
        protected bool numberIsArchived;

        public NumberMemorizer()
        {
            numberHistory = new List<int>();
            numberIsArchived = false;
        }

        public void ArchiveNumber()
        {
            if (numberIsArchived)
                return;
            numberHistory.Insert(0, CurrentNumber);
            numberIsArchived = true;
        }
    }
}
