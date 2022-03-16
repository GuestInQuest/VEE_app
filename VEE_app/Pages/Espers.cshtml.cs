using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using VEE_app.Models;


namespace VEE_app.Pages
{
    
    public class EspersModel : PageModel
    {
        private readonly ILogger<EspersModel> _logger;

        [BindProperty]
        public int SubmittedNumber { get; set; }
        IAbstractGame game { get; set; }

        public void SaveGameData()
        {
            (new Storage()).SaveGameData(game);
        }

        public void GetGameData()
        {
            (new Storage()).getGameData(game);
        }

        public EspersModel(ILogger<EspersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            game = (new GameFactory()).Create();
            GetGameData();
            if (game.hasError)
                ModelState.AddModelError("SubmittedNumber", game.errorMessage);
        }

        public IActionResult OnPostGuess()
        {
            game = (new GameFactory()).Create();
            GetGameData();
            game.GuessNumberByEspers();
            SaveGameData();
            return RedirectToPage("/Espers");
        }

        public IActionResult OnPostUnveil()
        {
            game = (new GameFactory()).Create();
            GetGameData();
            game.ResolveEspersGuesses(SubmittedNumber);
            SaveGameData();
            return RedirectToPage("/Espers");
        }

        //Следующие три метода добавил из-за того, что на самой странице "'EspersModel.game' is inaccessible due to its protection level"
        public List<Esper> GetEspers()
        {
            return game.espers;
        }
        
        public Tester GetTester()
        {
            return game.Tester;
        }

        public GameStates GetGameState()
        {
            return game.gameState;
        }
    }
}
