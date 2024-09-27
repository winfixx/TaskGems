using Spectre.Console;
using TaskGems.Core.Exceptions;
using TaskGems.Core.Models;
using TaskGems.Core.Repositories;
using TaskGems.DataAccess.Helpers;

namespace TaskGems.DataAccess.ConsoleAPI.Repositories
{
    public class ConsoleRepository : IQuadraticEquationsRepository
    {
        public QuadraticEquation GetQuadraticEquation()
        {
            string[] fields = ["a", "b", "c"];
            double[] parameters = new double[3];

            for (int i = 0; i < fields.Length; i++)
            {
                var fieldValue = AnsiConsole
                    .Prompt(new TextPrompt<string>($"{fields[i]}:")
                        .Validate(f => string.IsNullOrEmpty(f)
                            ? throw new FieldNullException($"Параметр {fields[i]} пуст, повторите попытку")
                            : ValidationResult.Success()));

                if (DoubleHelper.TryToDouble(fieldValue, out double parameter))
                    parameters[i] = parameter;
                else throw new Exception("Не удалось распарсить строку");
            }

            return new QuadraticEquation(parameters[0], parameters[1], parameters[2]);
        }

        public IEnumerable<QuadraticEquation> GetQuadraticEquations()
        {
            throw new NotImplementedException();
        }
    }
}
