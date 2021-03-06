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

    public interface IGameFactory
    {
        IGame Create();
        IGame Create(GameDTO gameDTO);
    }

    public interface IGame
    {
        Tester Tester { get; }
        Esper[] Espers { get; }
        GameStates State { get; } 
        void GuessNumberByEspers();
        void ResolveEspersGuesses(int submittedNumber);
        GameDTO GetDTO();
    }

    public class GameFactory : IGameFactory
    {
        public IGame Create()
        {
            return new Game();
        }
        public IGame Create(GameDTO gameDTO)
        {
            return new Game(gameDTO);
        }
    }

    public class Game : IGame
    {
        public Tester Tester { get; private set; }
        public Esper[] Espers
        {
            get { return espers.ToArray(); }
        }
        public GameStates State { get; private set; }

        private List<Esper> espers;

        public Game()
        {
            var names = new List<string> { "Всевидящая",
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
            espers = new();
            //При создании экстрасенсов удаляем из списка уже использованные имена
            for (int i = 0; i < rand.Next(2, 11); i++)
            {
                int j = rand.Next(0, names.Count);
                espers.Add(new Esper(names[j]));
                names.RemoveAt(j);
            }
            Tester = new();
        }

        public Game(GameDTO GameDTO)
        {
            Tester = new(GameDTO.TesterDTO);
            State = GameDTO.State;
            espers = new();
            foreach (EsperDTO e in GameDTO.EspersDTO)
            {
                espers.Add(new Esper(e));
            }
        }

        public void GuessNumberByEspers()
        {
            foreach (Esper esper in espers)
                esper.GuessNumber();            
            State = GameStates.EspersGuessedNumber;
        }

        public void ResolveEspersGuesses(int submittedNumber)
        {
            Tester.AddNumber(submittedNumber);
            foreach (Esper esper in espers)
            {
                esper.ReliabilityCheck(Tester.CurrentNumber);
                esper.ArchiveNumber();
            }
            Tester.ArchiveNumber();
            State = GameStates.EspersReadyToGuess;
        }

        public GameDTO GetDTO()
        {
            GameDTO GameDTO = new()
            {
                TesterDTO = Tester.GetDTO(),
                State = State
            };
            GameDTO.EspersDTO = new();
            foreach (Esper esper in espers)
            {
                GameDTO.EspersDTO.Add(esper.GetDTO());
            }
            return GameDTO;
        }
    }

    public class GameDTO
    {
        public TesterDTO TesterDTO { get; set; }
        public List<EsperDTO> EspersDTO { get; set; }
        public GameStates State { get; set; }
    }

}
