using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VEE_app.Models;


namespace VEE_app.Pages
{
    
    public class EspersModel : PageModel
    {
        private readonly ILogger<EspersModel> logger;
        private readonly IGameFactory gameFactory;
        private readonly IStorage storage;

        [BindProperty]

        [Range(10, 99, ErrorMessage = "Попробуйте ещё раз, загадать нужно было число от 10 до 99")]
        public int SubmittedNumber { get; set; }
        IGame Game { get; set; }

        public EspersModel(ILogger<EspersModel> logger, IGameFactory gameFactory, IStorage storage)
        {
            this.logger = logger;
            this.gameFactory = gameFactory;
            this.storage = storage;
        }       

        public void OnGet()
        {
            if (!storage.IsGameStateSaved())
            {
                Game = gameFactory.Create();
                storage.SaveGame(Game);
            }
            else
            {
                Game = storage.LoadGame();
            }
        }

        public IActionResult OnPostGuess()
        {
            Game = storage.LoadGame();
            Game.GuessNumberByEspers();
            storage.SaveGame(Game);
            return RedirectToPage("/Espers");
        }

        public IActionResult OnPostUnveil()
        {
            Game = storage.LoadGame();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Game.ResolveEspersGuesses(SubmittedNumber);
            storage.SaveGame(Game);
            return RedirectToPage("/Espers");         
        }

        //Следующие три метода добавил из-за того, что на самой странице "'EspersModel.game' is inaccessible due to its protection level"
        public Esper[] GetEspers()
        {
            return Game.Espers;
        }
        
        public Tester GetTester()
        {
            return Game.Tester;
        }

        public GameStates GetGameState()
        {
            return Game.State;
        }
    }
}
