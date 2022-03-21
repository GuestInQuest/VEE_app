using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VEE_app.Models
{
    public class Tester : NumberMemorizer
    {
        public Tester() : base()
        { }

        public Tester(TesterDTO TesterDTO)
        {
            CurrentNumber = TesterDTO.CurrentNumber;
            numberHistory = TesterDTO.NumberHistory;
            numberIsArchived = TesterDTO.NumberIsArchived;
        }

        public void AddNumber(int number)
        {
            CurrentNumber = number;
            numberIsArchived = false;
        }

        public TesterDTO GetDTO()
        {
            TesterDTO TesterDTO = new()
            {
                CurrentNumber = CurrentNumber,
                NumberHistory = numberHistory,
                NumberIsArchived = numberIsArchived    
            };
            return TesterDTO;
        }
    }

    public class TesterDTO
    {
        public int CurrentNumber { get; set; }
        public List<int> NumberHistory { get; set; }
        public bool NumberIsArchived { get; set; }
    }
}
