using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VEE_app.Models;


namespace VEE_app.Pages
{
    public class PrivacyModel : PageModel
    {
        public const string SessionKeyName = "_Name";
        public const string SessionKeyAge = "_Age";
        const string SessionKeyTime = "_Time";

        public string SessionInfo_Name { get; private set; }
        public string SessionInfo_Age { get; private set; }
        public string SessionInfo_CurrentTime { get; private set; }
        public string SessionInfo_SessionTime { get; private set; }
        public string SessionInfo_MiddlewareValue { get; private set; }

        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public string Message { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public int Age { get; set; }
         public List<string> Movie { get; set; }
        public void OnGet()
        {
            Message = "Введите данные";
            Movie = new List<string> { "asd", "zxc" };
        }
        public void OnPost()
        {
            Message = $"Имя: {Name}  Возраст: {Age}";
            Movie = new List<string> { "asd", "zxc" };
        }

        /*       public void OnGet()
               {
                   if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
                   {
                       HttpContext.Session.SetString(SessionKeyName, "The Doctor");
                       HttpContext.Session.SetInt32(SessionKeyAge, 773);
                       ViewData["Message"] = "Hello ASP.NET Core";
                   }
                   else
                   {
                       var name = HttpContext.Session.GetString(SessionKeyName);
                       var age = HttpContext.Session.GetInt32(SessionKeyAge);
                       ViewData["Message"] = name;
                   }
                   List<string> countries = new List<string> { "Бразилия", "Аргентина", "Уругвай", "Чили" };


               }
        */
    }
}
