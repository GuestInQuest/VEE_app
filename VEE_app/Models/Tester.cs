using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VEE_app.Models
{
    public class Tester
    {
        public int CurrentNumber { get; private set; }
        public List<int> NumbHistory { get; private set; }
        private bool numberIsArchived;

        public Tester()
        {
            NumbHistory = new List<int>();
            numberIsArchived = false;
        }

        public Tester(TesterDTO TesterDTO)
        {
            CurrentNumber = TesterDTO.CurrentNumber;
            NumbHistory = TesterDTO.NumbHistory;
            numberIsArchived = TesterDTO.NumberIsArchived;
        }

        public void AddNumber(int number)
        {
            CurrentNumber = number;
            numberIsArchived = false;
        }

        public void ArchiveNumber()
        {
            if (!numberIsArchived)
            {
                NumbHistory.Insert(0, CurrentNumber);
                numberIsArchived = true;
            }
        }
        public TesterDTO GetDTO()
        {
            TesterDTO TesterDTO = new()
            {
                CurrentNumber = CurrentNumber,
                NumbHistory = NumbHistory,
                NumberIsArchived = numberIsArchived    
            };
            return TesterDTO;
        }
    }


    public class TesterDTO
    {
        public int CurrentNumber { get; set; }
        public List<int> NumbHistory { get; set; }
        public bool NumberIsArchived { get; set; }
    }
}
