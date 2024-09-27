using TaskGems.Core.Models;
using TaskGems.Core.Services;
using Xunit;

namespace TaskGems.XUnitTest.ServiceTests
{
    public class QuadraticEquationsServiceTest
    {
        [Fact]
        public void SolutionQuadraticEquationCorrectOutput()
        {
            QuadraticEquationsService service = new();

            QuadraticEquation equation = new(2, 4, 2);
            RootQuadraticEquation? root = service.SolutionQuadraticEquation(equation);

            Assert.Equal(new RootQuadraticEquation(-1, -1), root);
        }
    }
}
