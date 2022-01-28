namespace VEE_app.Models
{
    public class Esper
    {
        public int Reliability_lvl { get; set; }
        public int Current_guess { get; set; }
        public string Name { get; set; }
        public System.Collections.Generic.List<int> Guessed_numbers { get; set; }
    }
}
