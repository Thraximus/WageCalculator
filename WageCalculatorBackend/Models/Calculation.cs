namespace WageCalculatorBackend.Models
{
    public class Calculation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public TimeSpan RegularStartTime { get; set; }
        public TimeSpan RegularEndTime { get; set; }


        public TimeSpan NightTimeStartTime { get; set; }
        public TimeSpan NightTimeEndTime { get; set; }

        public TimeSpan MidnightStartTime { get; set; }
        public TimeSpan MidnightEndTime { get; set; }


    }
}
