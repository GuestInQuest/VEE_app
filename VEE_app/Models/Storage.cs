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

        public const string SessionKeyEspers = "_Espers";
        public const string SessionKeyErrorMessage = "_ErrorMessage";
        public const string SessionKeyHasError = "_HasError";
        public const string SessionKeyGameState = "_GameState";
        public const string SessionKeyEspersCount = "_EspersCount";
        public const string SessionKeyTester = "_Tester";

        public Storage()
        {
            httpContextAccessor = new HttpContextAccessor();
        }

        private IAbstractGame InitData(IAbstractGame game)
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
            game.espersCount = rand.Next(2, 11);
            game.espers = new List<Esper>();
            //При создании экстрасенсов удаляем из списка уже использованные имена
            for (int i = 0, j = 0; i < game.espersCount; i++)
            {
                j = rand.Next(0, names.Count);
                game.espers.Add(new Esper(names[j]));
                names.RemoveAt(j);
            }
            game.hasError = false;
            game.tester = new Tester();
            return game;
        }

        private IAbstractGame LoadData(IAbstractGame game)
        {
            game.espers = new List<Esper>();
            game.espersCount = httpContextAccessor.HttpContext.Session.Get<int>(SessionKeyEspersCount);
            game.tester = httpContextAccessor.HttpContext.Session.Get<Tester>(SessionKeyTester);
            for (int i = 0; i < game.espersCount; ++i)
            {
                game.espers.Add(httpContextAccessor.HttpContext.Session.Get<Esper>(i + SessionKeyEspers));
            }

            game.hasError = httpContextAccessor.HttpContext.Session.Get<bool>(SessionKeyHasError);
            game.errorMessage = httpContextAccessor.HttpContext.Session.Get<string>(SessionKeyErrorMessage);
            game.gameState = httpContextAccessor.HttpContext.Session.Get<GameStates>(SessionKeyGameState);
            return game;
        }

        public IAbstractGame SaveGameData(IAbstractGame game)
        {
            var i = 0;
            foreach (Esper e in game.espers)
            {
                httpContextAccessor.HttpContext.Session.Set<Esper>(i + SessionKeyEspers, e);
                i++;
            }
            httpContextAccessor.HttpContext.Session.Set<Tester>(SessionKeyTester, game.tester);
            httpContextAccessor.HttpContext.Session.Set<int>(SessionKeyEspersCount, game.espersCount);
            httpContextAccessor.HttpContext.Session.Set<bool>(SessionKeyHasError, game.hasError);
            httpContextAccessor.HttpContext.Session.Set<string>(SessionKeyErrorMessage, game.errorMessage);
            httpContextAccessor.HttpContext.Session.Set<GameStates>(SessionKeyGameState, game.gameState);
            return game;
    }

        public IAbstractGame getGameData(IAbstractGame game)
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
            return game;
        }
    }
}
