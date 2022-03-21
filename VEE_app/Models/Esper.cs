using System.Collections.Generic;

namespace VEE_app.Models
{
    public class Esper : NumberMemorizer
    {
        public string Name { get; private set; }
        public int ReliabilityLevel { get; private set; }

        private int reliabilityStep;

        public Esper(string name):base()
        {
            ReliabilityLevel = 100;
            this.Name = name;
            reliabilityStep = 5;
        }

        public Esper(EsperDTO esperDTO):this(esperDTO.Name)
        {
            ReliabilityLevel = esperDTO.ReliabilityLevel;
            numberHistory = esperDTO.GuessedNumbers;
            CurrentNumber = esperDTO.CurrentGuess;
            numberIsArchived = esperDTO.NumberIsArchived;
        }

        public void GuessNumber()
        {
            CurrentNumber = new System.Random().Next(10, 100);
            numberIsArchived = false;
        }

        public void ReliabilityCheck(int CurrentNumber)
        {
            if (this.CurrentNumber == CurrentNumber)
                ReliabilityLevel += reliabilityStep;
            else
                ReliabilityLevel -= reliabilityStep;
        }

        public EsperDTO GetDTO()
        {
            EsperDTO EsperDTO = new()
            {
                Name = Name,
                ReliabilityLevel = ReliabilityLevel,
                CurrentGuess = CurrentNumber,
                GuessedNumbers = numberHistory,
                NumberIsArchived = numberIsArchived
            };
            return EsperDTO;
        }
    }


    public class EsperDTO
    {
        public string Name { get; set; }
        public int ReliabilityLevel { get; set; }
        public int CurrentGuess { get; set; }
        public List<int> GuessedNumbers { get; set; }
        public bool NumberIsArchived { get; set; }
    }
}
