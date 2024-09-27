using TaskGems.Core.Models;

namespace TaskGems.Core.Repositories
{
    public interface IQuadraticEquationsRepository
    {
        QuadraticEquation GetQuadraticEquation();
        IEnumerable<QuadraticEquation> GetQuadraticEquations();
    }
}
