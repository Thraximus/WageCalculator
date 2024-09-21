using Swashbuckle.AspNetCore.Filters;
using WageCalculatorBackend.Models;

namespace WageCalculatorBackend.SwaggerExamples
{
    public class TimeRuleCreateExample : IExamplesProvider<TimeRule>
    {
        public TimeRule GetExamples()
        {
            return new TimeRule
            {
                Name = "Test Name",
                Description = "Test Description",
                RegularStartTime = 8,
                NightTimeStartTime = 16,
                MidnightStartTime = 21
            };
        }
    }

    public class TimeRulePatchExample : IExamplesProvider<TimeRule>
    {
        public TimeRule GetExamples()
        {
            return new TimeRule
            {
                Id = 2,
                Name = "Test Name",
                Description = "Test Description",
                RegularStartTime = 8,
                NightTimeStartTime = 16,
                MidnightStartTime = 21
            };
        }
    }
}
