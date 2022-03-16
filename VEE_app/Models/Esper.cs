using System.Collections.Generic;

namespace VEE_app.Models
{
    public class Esper
    {
        public string Name { get; private set; }
        public int ReliabilityLevel { get; private set; }
        public int CurrentGuess { get; private set; }
        public System.Collections.Generic.List<int> GuessedNumbers { get; private set; }
        private int reliabilityStep;

        public Esper(string name)
        {
            ReliabilityLevel = 100;
            this.Name = name;
            GuessedNumbers = new List<int>();
            reliabilityStep = 5;
        }

        public Esper(EsperDTO esperDTO):this(esperDTO.Name)
        {
            ReliabilityLevel = esperDTO.ReliabilityLevel;
            GuessedNumbers = esperDTO.GuessedNumbers;
            CurrentGuess = esperDTO.CurrentGuess;
        }

        public void GuessNumber()
        {
            CurrentGuess = new System.Random().Next(10, 100);
        }

        public void ReliabilityCheck(int CurrentNumb)
        {
            if (CurrentGuess == CurrentNumb)
                ReliabilityLevel += reliabilityStep;
            else
                ReliabilityLevel -= reliabilityStep;
        }

        public void PrepareToGuess()
        {
            GuessedNumbers.Insert(0, CurrentGuess);
        }

        public EsperDTO GetDTO()
        {
            EsperDTO EsperDTO = new()
            {
                Name = Name,
                ReliabilityLevel = ReliabilityLevel,
                CurrentGuess = CurrentGuess,
                GuessedNumbers = GuessedNumbers
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
    }
}
