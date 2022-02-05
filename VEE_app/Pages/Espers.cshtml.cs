using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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
        public const string SessionKeyEspersCount = "_EspersCount";
        public const string SessionKeyTester = "_Tester";

        private readonly ILogger<EspersModel> _logger;

        [BindProperty]
        public int SubmittedNumb { get; set; }
        public Tester tester { get; set; }

        public List<Esper> espers { get; set; }

        public int pageState { get; set; } // Состояние страницы (1-загадываем число, 2-вводим загаданное число)
        public int espersCount { get; set; }

        public EspersModel(ILogger<EspersModel> logger)
        {
            _logger = logger;
        }

        public void InitObjects()
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
            espers = new List<Esper>();
            //При создании экстрасенсов удаляем из списка уже использованные имена
            for (int i = 0, j = 0; i < espersCount; i++)
            {
                j = rand.Next(0, names.Count);
                espers.Add(new Esper(names[j]));
                names.RemoveAt(j);
            }
            tester = new Tester();
            pageState = 1;
        }

        public void SaveObjects()
        {
            var i = 0;
            foreach (Esper e in espers)
            {
                HttpContext.Session.Set<Esper>(i + SessionKeyEspers, e);
                i++;
            }
            HttpContext.Session.Set<Tester>(SessionKeyTester, tester);
            HttpContext.Session.Set<int>(SessionKeyEspersCount, espersCount);
        }

        public void LoadObjects()
        {
            espers = new List<Esper>();
            espersCount = HttpContext.Session.Get<int>(SessionKeyEspersCount);
            tester = HttpContext.Session.Get<Tester>(SessionKeyTester);
            for (int i = 0; i < espersCount; ++i)
            {
                espers.Add(HttpContext.Session.Get<Esper>(i + SessionKeyEspers));
            }
        }

        public void OnGet()
        {            
            if (HttpContext.Session.Get<int>(SessionKeyEspersCount) == default)
            {
                InitObjects();
                SaveObjects();
            }
            else
            {
                LoadObjects();
            }
        }

        public void OnPostGuess()
        {
            LoadObjects();
            {
                foreach (Esper e in espers)
                    e.GuessNumber();
            }
            SaveObjects();
            pageState = 2;
        }

        public void OnPostUnveil()
        {
            LoadObjects();
            {
                if (tester.AddNumber(SubmittedNumb))
                {
                    foreach (Esper e in espers)
                    {
                        e.ReliabilityCheck(tester.currentNumber);
                    }
                    tester.ArchiveNumber();
                    pageState = 1;
                }
                else
                {
                    ModelState.AddModelError("SubmittedNumb", "Попробуйте ещё раз, загадать нужно было двузначное число");
                    pageState = 2;
                }
            }
            SaveObjects();
        }
    }
}
