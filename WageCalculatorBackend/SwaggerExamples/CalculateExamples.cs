using Swashbuckle.AspNetCore.Filters;
using WageCalculatorBackend.Models;

namespace WageCalculatorBackend.SwaggerExamples
{
    public class CalculateExamples : IExamplesProvider<CalculateRequest>
    {
        public CalculateRequest GetExamples()
        {
            return new CalculateRequest
            {
                RegularRate = 1000,
                NightTimeRate = 1300,
                MidnightRate = 1500,
                NumberOfDays = 4,
                Days = new List<DayRange>
                {
                    new DayRange { Start = 0, End = 9 },
                    new DayRange { Start = 9, End = 17 },
                    new DayRange { Start = 17, End = 22 },
                    new DayRange { Start = 22, End = 23 }
                },
                TimeRule = new TimeRule
                {
                    Id = 1,
                    Name = "Default",
                    Description = "Default calculating rate",
                    RegularStartTime = 9,
                    NightTimeStartTime = 17,
                    MidnightStartTime = 22
                }
            };
        }
    }

    public class CalculateStandardExample : IExamplesProvider<CalculateRequestStandard>
    {
        public CalculateRequestStandard GetExamples()
        {
            return new CalculateRequestStandard
            {
                RegularRate = 1300,
                NightTimeRate = 1500,
                MidnightRate = 1700,
                NumberOfDays = 7,
                Days = new List<DayRange>
                {
                    new DayRange { Start = 8, End = 19 },
                    new DayRange { Start = 9, End = 20 },
                    new DayRange { Start = 10, End = 21 },
                    new DayRange { Start = 11, End = 22 },
                    new DayRange { Start = 0, End = 23 },
                    new DayRange { Start = 20, End = 22 },
                    new DayRange { Start = 0, End = 21 }
                }
            };
        }
    }



}
