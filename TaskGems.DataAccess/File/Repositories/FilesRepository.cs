using Spectre.Console;
using TaskGems.Core.Exceptions;
using TaskGems.Core.Models;
using TaskGems.Core.Repositories;
using TaskGems.DataAccess.Helpers;

namespace TaskGems.DataAccess.File.Repositories
{
    public class FilesRepository : IQuadraticEquationsRepository
    {
        public QuadraticEquation GetQuadraticEquation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuadraticEquation> GetQuadraticEquations()
        {
            var currentDirectory = Environment.CurrentDirectory;
            const string needFileExtension = ".txt";

            var fileName = AnsiConsole
                .Prompt(new TextPrompt<string>("Напишите название файла:")
                    .Validate(t => string.IsNullOrEmpty(t.Trim())
                        ? throw new FieldNullException("Необходимо название файла")
                        : ValidationResult.Success()));

            if (string.IsNullOrEmpty(fileName))
                throw new FieldNullException("Необходимо название файла");

            var fileNameWithoutExtension = Path
                .GetFileNameWithoutExtension(fileName);

            var finalyPath = Path
                .Combine(currentDirectory,
                        fileNameWithoutExtension + needFileExtension);

            if (!System.IO.File.Exists(finalyPath))
                throw new FileNotFoundException(
                    $"Файла с таким ({fileName}) именем не существует", fileName);


            string[] lines = System.IO.File.ReadAllLines(finalyPath);
            List<QuadraticEquation> quadraticEquations = [];

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                else
                {
                    var equation = line.Split(' ');
                    double[] parameters = new double[3];

                    for (int i = 0; i < equation.Length; i++)
                    {
                        if (DoubleHelper.TryToDouble(equation[i], out double parameter))
                            parameters[i] = parameter;
                        else throw new Exception("Не удалось распарсить строку");
                    }

                    quadraticEquations.Add(new QuadraticEquation(parameters[0], parameters[1], parameters[2]));
                }
            }


            return quadraticEquations;
        }
    }
}
