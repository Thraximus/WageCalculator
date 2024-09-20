using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WageCalculatorBackend.DbData;
using WageCalculatorBackend.Models;
using WageCalculatorBackend.Repositories;

namespace WageCalculatorBackend.AppRepositories
{
    public class TimeRuleRepository : ITimeRuleRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeRuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeRule>> GetAllTimeRulesAsync()
        {
            return await _context.TimeRules.ToListAsync();
        }
        public async Task<TimeRule> GetTimeRuleByIdAsync(int id)
        {
            try
            {
                var calculation = await _context.TimeRules
                                                .FirstOrDefaultAsync(c => c.Id == id);

                if (calculation == null)
                {
                    throw new Exception($"TimeRule with ID {id} not found.");
                }

                return calculation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching TimeRule: {ex.Message}", ex);
            }
        }

        public async Task<TimeRule> GetDefaultTimeRuleAsync()
        {
            try
            {
                var calculation = await _context.TimeRules.FirstAsync();

                if (calculation == null)
                {
                    throw new Exception($"Default TimeRule not found.");
                }

                return calculation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching TimeRule: {ex.Message}", ex);
            }
        }

        public async Task<TimeRule> AddTimeRuleAsync(TimeRule timeRule)
        {
            try
            {
                await _context.TimeRules.AddAsync(timeRule);
                await _context.SaveChangesAsync();
                return timeRule;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving TimeRule: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteTimeRuleByIdAsync(int id)
        {
            try
            {
                var calculation = new TimeRule { Id = id }; 

                _context.TimeRules.Attach(calculation);  
                _context.TimeRules.Remove(calculation); 
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"TimeRule with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting TimeRule: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateTimeRuleAsync(TimeRule updatedTimeRule)
        {
            try
            {
                _context.TimeRules.Attach(updatedTimeRule);
                _context.Entry(updatedTimeRule).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"TimeRule with ID {updatedTimeRule.Id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating TimeRule: {ex.Message}", ex);
            }
        }



    }
}
