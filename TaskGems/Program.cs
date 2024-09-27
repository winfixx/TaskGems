using TaskGems.Core.Services;
using TaskGems.Infrastructure.ConsoleAPI.Controllers;

namespace TaskGems
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new ConsoleController(new QuadraticEquationsService()).Run();
        }
    }
}
