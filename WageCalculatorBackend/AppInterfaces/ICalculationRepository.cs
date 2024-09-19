using WageCalculatorBackend.Models;

namespace WageCalculatorBackend.Repositories
{
    public interface ICalculationRepository
    {
        Task<IEnumerable<Calculation>> GetAllCalculationsAsync();
    }
}
