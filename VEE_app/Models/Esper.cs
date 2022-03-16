using System.Collections.Generic;

namespace VEE_app.Models
{
    public class Esper
    {
        public string name { get; set; }
        public int reliabilityLevel { get; set; }
        public int currentGuess { get; set; }
        public System.Collections.Generic.List<int> guessedNumbers { get; set; }
        public bool guessIsMade { get; set; }
        private int reliabilityStep;
        public Esper()
        {
            reliabilityLevel = 100;
            guessIsMade = false;
            this.name = "Случайный экстрасенс №" + (new System.Random()).Next().ToString();
            guessedNumbers = new List<int>();
            reliabilityStep = 5;
        }

        public Esper(string name)
        {
            _ = new Esper();
            this.name = name;
        }

        public Esper(string name, int reliabilityLevel)
        {
            _ = new Esper(name);
            this.reliabilityLevel = reliabilityLevel;
        }

        public Esper(int reliabilityLevel)
        {
            _ = new Esper();
            this.reliabilityLevel = reliabilityLevel;
        }

        public void GuessNumber()
        {
            currentGuess = new System.Random().Next(10,100);
            guessIsMade = true;
        }
        
        public void ReliabilityCheck(int CurrentNumb)
        {
            if (currentGuess == CurrentNumb)
                reliabilityLevel += reliabilityStep;
            else
                reliabilityLevel -= reliabilityStep;
        }

        public void PrepareToGuess()
        {
            guessedNumbers.Insert(0, currentGuess);
            guessIsMade = false;
        }
    }
}
