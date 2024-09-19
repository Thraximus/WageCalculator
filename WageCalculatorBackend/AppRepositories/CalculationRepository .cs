using Microsoft.EntityFrameworkCore;
using WageCalculatorBackend.DbData;
using WageCalculatorBackend.Models;
using WageCalculatorBackend.Repositories;

namespace WageCalculatorBackend.AppRepositories
{
    public class CalculationRepository : ICalculationRepository
    {
        private readonly ApplicationDbContext _context;

        public CalculationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Calculation>> GetAllCalculationsAsync()
        {
            return await _context.Calculations.ToListAsync();
        }
    }
}
