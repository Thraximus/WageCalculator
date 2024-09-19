namespace WageCalculatorBackend.Models
{
    public class Calculation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public TimeSpan RegularStartTime { get; set; }

        public TimeSpan NightTimeStartTime { get; set; }

        public TimeSpan MidnightStartTime { get; set; }


    }
}
