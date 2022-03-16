using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VEE_app.Models
{
    public class Tester : IValidatableObject
    {

        [Range(0, 99)]
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
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CurrentNumber is < 10 or > 99)
            {
                yield return new ValidationResult(
                    $"Попробуйте ещё раз, загадать нужно было двузначное число, а вы загадали {CurrentNumber}.",
                    new[] { nameof(CurrentNumber) });
            }
        }

        public bool ValidateNumber(int number)
        {
            if (number is < 10 or > 99)
                return false;
            else return true;
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
