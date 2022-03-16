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

    public interface IStorage
    {
        IGame LoadGame();
        void SaveGame(IGame iGame);
        bool IsGameStateSaved();
    }

    public class Storage : IStorage
    {
        private int isGameStateSaved;
        private IHttpContextAccessor httpContextAccessor;
        private IGameFactory gameFactory;

        public const string SessionKeyGameDTO = "_GameDTO";
        public const string SessionKeyIsGameStateSaved = "_IsGameStateSaved";

        public Storage(IGameFactory gameFactory)
        {
            httpContextAccessor = new HttpContextAccessor();
            this.gameFactory = gameFactory;
        }

        public IGame LoadGame()
        {
            IGame game = gameFactory.Create(httpContextAccessor.HttpContext.Session.Get<GameDTO>(SessionKeyGameDTO));
            return game;
        }

        public void SaveGame(IGame iGame)
        {
            httpContextAccessor.HttpContext.Session.Set(SessionKeyGameDTO, iGame.GetDTO());
            isGameStateSaved = 1;
            httpContextAccessor.HttpContext.Session.Set(SessionKeyIsGameStateSaved, isGameStateSaved);
        }

        public bool IsGameStateSaved()
        {
            if (httpContextAccessor.HttpContext.Session.Get<int>(SessionKeyIsGameStateSaved) == default)
                return false;
            return true;
        }
    }
}
