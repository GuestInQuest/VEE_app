using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VEE_app.Models
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
    public class Storage
    {
        private IHttpContextAccessor httpContextAccessor;
        private readonly IFactory factory;
        /*
        public const string SessionKeyEspers = "_Espers";
        public const string SessionKeyErrorMessage = "_ErrorMessage";
        public const string SessionKeyHasError = "_HasError";
        public const string SessionKeyGameState = "_GameState";
        public const string SessionKeyEspersCount = "_EspersCount";
        public const string SessionKeyTester = "_Tester";
        */
        public const string SessionKeyGame = "_Game";
        
        public Storage(IFactory factory)
        {
            httpContextAccessor = new HttpContextAccessor();
            this.factory = factory;
        }

        private Game InitData(IGame game)
        {
            return factory.Create().NewGame();
        }

        private IGame LoadData(IGame game)
        {/*
            game.espers = new List<Esper>();
            game.espersCount = httpContextAccessor.HttpContext.Session.Get<int>(SessionKeyEspersCount);
            game.Tester = httpContextAccessor.HttpContext.Session.Get<Tester>(SessionKeyTester);
            for (int i = 0; i < game.espersCount; ++i)
            {
                game.espers.Add(httpContextAccessor.HttpContext.Session.Get<Esper>(i + SessionKeyEspers));
            }

            game.hasError = httpContextAccessor.HttpContext.Session.Get<bool>(SessionKeyHasError);
            game.errorMessage = httpContextAccessor.HttpContext.Session.Get<string>(SessionKeyErrorMessage);
            game.gameState = httpContextAccessor.HttpContext.Session.Get<GameStates>(SessionKeyGameState);
            */
            game = httpContextAccessor.HttpContext.Session.Get<Game>(SessionKeyGame);
            return game;
        }

        public void SaveGameData(Game game)
        {
            /*
            var i = 0;
            foreach (Esper e in game.espers)
            {
                httpContextAccessor.HttpContext.Session.Set<Esper>(i + SessionKeyEspers, e);
                i++;
            }
            httpContextAccessor.HttpContext.Session.Set<Tester>(SessionKeyTester, game.Tester);
            httpContextAccessor.HttpContext.Session.Set<int>(SessionKeyEspersCount, game.espersCount);
            httpContextAccessor.HttpContext.Session.Set<bool>(SessionKeyHasError, game.hasError);
            httpContextAccessor.HttpContext.Session.Set<string>(SessionKeyErrorMessage, game.errorMessage);
            httpContextAccessor.HttpContext.Session.Set<GameStates>(SessionKeyGameState, game.gameState);
            */
            httpContextAccessor.HttpContext.Session.Set<Game>(SessionKeyGameState, game);
        }

        public void getGameData(Game game)
        {
            if (httpContextAccessor.HttpContext.Session.Get<int>(SessionKeyEspersCount) == default)
            {
                InitData(game);
                SaveGameData(game);
            }
            else
            {
                LoadData(game);
            }
        }
    }
}
