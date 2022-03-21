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
        private ISession currentSession;
        private IGameFactory gameFactory;

        public const string SessionKeyGameDTO = "_GameDTO";

        public Storage(IGameFactory gameFactory, IHttpContextAccessor httpContextAccessor)
        {
            this.gameFactory = gameFactory;
            currentSession = httpContextAccessor.HttpContext.Session;
        }

        public IGame LoadGame()
        {
            IGame game = gameFactory.Create(currentSession.Get<GameDTO>(SessionKeyGameDTO));
            return game;
        }

        public void SaveGame(IGame iGame)
        {
            currentSession.Set(SessionKeyGameDTO, iGame.GetDTO());
        }

        public bool IsGameStateSaved()
        {
            return currentSession.Keys.Contains(SessionKeyGameDTO);
        }
    }
}
