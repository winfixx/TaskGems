using TaskGems.Core.Models;

namespace TaskGems.Core.Services
{
    public class QuadraticEquationsService()
    {
        public RootQuadraticEquation? SolutionQuadraticEquation(QuadraticEquation quadraticEquation)
        {
            var rootQuadraticEquation = quadraticEquation.SolutionQuadraticEquation();

            return rootQuadraticEquation.HasValue
                ? rootQuadraticEquation.Value
                : null;
        }

        public IEnumerable<RootQuadraticEquation?> SolutionQuadraticEquation(IEnumerable<QuadraticEquation> quadraticEquations)
        {
            List<RootQuadraticEquation?> roots = [];

            foreach (var quadraticEquation in quadraticEquations)
            {
                var rootQuadraticEquation = quadraticEquation.SolutionQuadraticEquation();

                roots.Add(rootQuadraticEquation);
            }

            return roots;
        }
    }
}
