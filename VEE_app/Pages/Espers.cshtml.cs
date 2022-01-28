using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using VEE_app.Models;


namespace VEE_app.Pages
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
    public class EspersModel : PageModel
    {
        public const string SessionKeyEspers = "_Espers";
        public const string SessionKeyNumbHistory = "_NumbHistory";

        public string SessionInfo_CurrentTime { get; private set; }
        public string SessionInfo_SessionTime { get; private set; }
        public string SessionInfo_MiddlewareValue { get; private set; }

        private readonly ILogger<EspersModel> _logger;

        public EspersModel(ILogger<EspersModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public int cur_numb { get; set; }
        public List<int> NumbHistory { get; set; }
        public List<Esper> Espers { get; set; }
        public void InitObjects()
        {
            Espers = new List<Esper>
                { new Esper
                    {
                        Reliability_lvl = 100,
                        Current_guess = -1,
                        Name = "Великий экстрасенс",
                        Guessed_numbers = new List<int>()
                    },
                  new Esper
                    {
                        Reliability_lvl = 100,
                        Current_guess = -1,
                        Name = "Шаман",
                        Guessed_numbers = new List<int>()
                    },
                  new Esper
                    {
                        Reliability_lvl = 100,
                        Current_guess = -1,
                        Name = "Потомственный красный колдун",
                        Guessed_numbers = new List<int>()
                    },
                  new Esper
                    {
                        Reliability_lvl = 100,
                        Current_guess = -1,
                        Name = "Прорицатель",
                        Guessed_numbers = new List<int>()
                    },
                  new Esper
                    {
                        Reliability_lvl = 100,
                        Current_guess = -1,
                        Name = "Всевидящая",
                        Guessed_numbers = new List<int>()
                    },
                };
            NumbHistory = new List<int>();
        }
        public void SaveObjects()
        {
            var i = 0;
            foreach (Esper e in Espers)
            {
                HttpContext.Session.Set<Esper>(i + SessionKeyEspers, e);
                i++;
            }
            HttpContext.Session.Set<List<int>>(SessionKeyNumbHistory, NumbHistory);
        }
        public void LoadObjects()
        {
            Espers = new List<Esper>();
            NumbHistory = new List<int>();
            NumbHistory = HttpContext.Session.Get<List<int>>(SessionKeyNumbHistory);
            for (int i = 0; i < 5; ++i)
            {
                Espers.Add(HttpContext.Session.Get<Esper>(i + SessionKeyEspers));
            }
        }

        public void OnGet()
        {
            
            if (HttpContext.Session.Get<List<int>>(SessionKeyNumbHistory) == default)
            {
                InitObjects();
                SaveObjects();
            }
            else
            {
                LoadObjects();
            }
        }
        public void OnPost()
        {
            LoadObjects();
            if (Espers[0].Current_guess == -1)
            {
                var rand = new Random();
                foreach (Esper e in Espers)
                    e.Current_guess = rand.Next(10);
            }
            else
            {
                NumbHistory.Insert(0, cur_numb);
                foreach (Esper e in Espers)
                {
                    if (e.Current_guess == cur_numb) 
                        e.Reliability_lvl += 5;
                    else
                        e.Reliability_lvl -= 5;
                    e.Guessed_numbers.Insert(0, e.Current_guess);
                    e.Current_guess = -1;
                }
                cur_numb = -1;
            }
            SaveObjects();
        }
    }
}
