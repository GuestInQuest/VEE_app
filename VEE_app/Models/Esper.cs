using System.Collections.Generic;

namespace VEE_app.Models
{
    public class Esper
    {
        public int reliabilityLevel { get; set; }
        public int currentGuess { get; set; }
        public string name { get; set; }
        public System.Collections.Generic.List<int> guessedNumbers { get; set; }
        private int reliabilityStep;
        public Esper()
        {
            reliabilityLevel = 100;
            currentGuess = -1;
            this.name = "Случайный экстрасенс №" + (new System.Random()).Next().ToString();
            guessedNumbers = new List<int>();
            reliabilityStep = 5;
        }

        public Esper(string name)
        {
            reliabilityLevel = 100;
            currentGuess = -1;
            this.name = name;
            guessedNumbers = new List<int>();
            reliabilityStep = 5;
        }

        public Esper(string Name, int ReliabilityLevel)
        {
            this.reliabilityLevel = ReliabilityLevel;
            currentGuess = -1;
            this.name = Name;
            guessedNumbers = new List<int>();
        }

        public Esper(int ReliabilityLevel)
        {
            this.reliabilityLevel = ReliabilityLevel;
            currentGuess = -1;
            this.name = "Случайный экстрасенс №" + (new System.Random()).Next().ToString(); 
            guessedNumbers = new List<int>();
        }

        public void GuessNumber()
        {
            currentGuess = new System.Random().Next(10,100);
        }
        
        public void ReliabilityCheck(int CurrentNumb)
        {
            if (currentGuess == CurrentNumb)
                reliabilityLevel += reliabilityStep;
            else
                reliabilityLevel -= reliabilityStep;
            guessedNumbers.Insert(0, currentGuess);
            currentGuess = -1;
        }
    }
}
