using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VEE_app.Models
{

    public enum GameStates
    {   
        EspersReadyToGuess,
        EspersGuessedNumber
    }

    public interface IAbstractFactory
    {
        IAbstractGame CreateGame();
    }

    public interface IAbstractGame
    {
        Tester tester { get; set; }
        List<Esper> espers { get; set; }
        int espersCount { get; set; }
        GameStates gameState { get; set; }
        bool hasError { get; set; }
        string errorMessage { get; set; }
        void GuessNumberByEspers();
        void ResolveEspersGuesses(int submittedNumber);
    }

    public class GameFactory : IAbstractFactory
    {
        public IAbstractGame CreateGame()
        {
            return new Game();
        }
    }

    public class Game: IAbstractGame
    {
        public Tester tester { get; set; }
        public List<Esper> espers { get; set; }
        public int espersCount { get; set; }
        public GameStates gameState { get; set; }
        public bool hasError { get; set; }
        public string errorMessage { get; set; }

        public void GuessNumberByEspers()
        {
            hasError = false;
            foreach (Esper e in espers)
                e.GuessNumber();
            gameState = GameStates.EspersGuessedNumber;
        }

        public void ResolveEspersGuesses(int submittedNumber)
        {
            hasError = false;
            if (tester.ValidateNumber(submittedNumber))
            {
                tester.AddNumber(submittedNumber);
                foreach (Esper e in espers)
                {
                    e.ReliabilityCheck(tester.currentNumber);
                    e.PrepareToGuess();
                }
                tester.ArchiveNumber();
                gameState = GameStates.EspersReadyToGuess;
            }
            else
            {
                gameState = GameStates.EspersGuessedNumber;
                hasError = true;
                errorMessage = "Попробуйте ещё раз, загадать нужно было двузначное число";
            }
        }
    }
}
