using Microsoft.EntityFrameworkCore;
using WageCalculatorBackend.DbData;
using WageCalculatorBackend.Models;
using WageCalculatorBackend.Repositories;

namespace WageCalculatorBackend.AppRepositories
{
    public class CalculationRepository : ITimeRuleRepository
    {
        private readonly ApplicationDbContext _context;

        public CalculationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeRule>> GetAllTimeRulesAsync()
        {
            return await _context.Calculations.ToListAsync();
        }
        public async Task<TimeRule> GetTimeRuleByIdAsync(int id)
        {
            try
            {
                var calculation = await _context.Calculations
                                                .FirstOrDefaultAsync(c => c.Id == id);

                if (calculation == null)
                {
                    throw new Exception($"Calculation with ID {id} not found.");
                }

                return calculation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching calculation: {ex.Message}", ex);
            }
        }

        public async Task<TimeRule> GetDefaultTimeRuleAsync()
        {
            try
            {
                var calculation = await _context.Calculations.FirstAsync();

                if (calculation == null)
                {
                    throw new Exception($"Default calculation not found.");
                }

                return calculation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching calculation: {ex.Message}", ex);
            }
        }

    }
}
