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

    public interface IFactory
    {
        IGame Create();
    }

    public interface IGame
    {
        Tester Tester { get; }
        List<Esper> Espers { get; }
        GameStates State { get; }
        bool HasError { get; }
        string ErrorMessage { get; }
        void GuessNumberByEspers();
        void ResolveEspersGuesses(int submittedNumber);
    }

    public class GameFactory : IFactory
    {
        public IGame Create()
        {
            return new Game();
        }
    }

    public class Game: IGame
    {
        public Tester Tester { get; set; }
        public List<Esper> Espers { get; set; }
        public GameStates State { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        private int espersCount { get; set; }

        public void NewGame()
        {
            List<string> names = new List<string> { "Всевидящая",
                                                    "Прорицатель",
                                                    "Потомственный красный колдун",
                                                    "Шаман",
                                                    "Великий экстрасенс",
                                                    "Отшельник",
                                                    "Иллюзионист",
                                                    "Психолог",
                                                    "Обманщик",
                                                    "Шарлатан"};
            var rand = new System.Random();
            espersCount = rand.Next(2, 11);
            Espers = new List<Esper>();
            //При создании экстрасенсов удаляем из списка уже использованные имена
            for (int i = 0, j = 0; i < espersCount; i++)
            {
                j = rand.Next(0, names.Count);
                Espers.Add(new Esper(names[j]));
                names.RemoveAt(j);
            }
            HasError = false;
            Tester = new Tester();
        }

        public void GuessNumberByEspers()
        {
            HasError = false;
            foreach (Esper e in Espers)
                e.GuessNumber();
            State = GameStates.EspersGuessedNumber;
        }

        public void ResolveEspersGuesses(int submittedNumber)
        {
            if (!Tester.ValidateNumber(submittedNumber))
            {
                State = GameStates.EspersGuessedNumber;
                HasError = true;
                ErrorMessage = "Попробуйте ещё раз, загадать нужно было двузначное число";
                return;                
            }
            HasError = false;
            Tester.AddNumber(submittedNumber);
            foreach (Esper e in Espers)
            {
                e.ReliabilityCheck(Tester.currentNumber);
                e.PrepareToGuess();
            }
            Tester.ArchiveNumber();
            State = GameStates.EspersReadyToGuess;
        }
    }
}
