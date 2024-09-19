using WageCalculatorBackend.Models;

namespace WageCalculatorBackend.Repositories
{
    public interface ITimeRuleRepository
    {
        Task<IEnumerable<TimeRule>> GetAllTimeRulesAsync();
        Task<TimeRule> GetTimeRuleByIdAsync(int id);
        Task<TimeRule> GetDefaultTimeRuleAsync(); 
    }
}
