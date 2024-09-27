using Spectre.Console;
using System.Diagnostics;
using TaskGems.Core.Exceptions;
using TaskGems.Core.Models;
using TaskGems.Core.Repositories;
using TaskGems.Core.Services;
using TaskGems.DataAccess.ConsoleAPI.Repositories;
using TaskGems.DataAccess.File.Repositories;
using TaskGems.Infrastructure.ConsoleAPI.Models;

namespace TaskGems.Infrastructure.ConsoleAPI.Controllers
{
    public class ConsoleController(QuadraticEquationsService quadraticEquationsService)
    {
        public static readonly IQuadraticEquationsRepository consoleRepository = new ConsoleRepository();
        public static readonly IQuadraticEquationsRepository filesRepository = new FilesRepository();
        public readonly QuadraticEquationsService quadraticEquationsService = quadraticEquationsService;

        public void Run()
        {
            Console.Title = "Решение квадратных уравнений";
            AnsiConsole.MarkupLine("[bold]Решение квадратных уравнений[/]\n");

            const string op1 = "C клавиатуры";
            const string op2 = "C файла";
            const string op3 = "Выход";

            var options = new Options([
                new(op1, KeyboardInputAction),
                new(op2, FileInputAction),
                new(op3, () => Environment.Exit(0))]);

            while (true)
            {
                var select = AnsiConsole
                    .Prompt(new SelectionPrompt<string>()
                        .Title("Выберите [green]способ ввода[/] исходных данных?")
                        .AddChoices(options.GetNameOption()));

                switch (select)
                {
                    case op1:
                        options.GetActionByName(select)();
                        break;
                    case op2:
                        options.GetActionByName(select)();
                        break;
                    case op3:
                        options.GetActionByName(select)();
                        break;
                    default:
                        break;
                }
            }
        }

        private void KeyboardInputAction()
        {
            Console.WriteLine("Введите входные данные\n");

            while (true)
            {
                try
                {
                    var quadraticEquation = consoleRepository.GetQuadraticEquation();
                    var rootQuadraticEquation = quadraticEquationsService
                        .SolutionQuadraticEquation(quadraticEquation);

                    AnsiConsole.MarkupLine($"\n[lime]{quadraticEquation.ToViewQuadraticEquation()}[/]");
                    OutputResult(rootQuadraticEquation);

                    break;
                }
                catch (FieldNullException ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.NoStackTrace);
                    continue;
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.Default);
                    break;
                }
            }
        }

        private void FileInputAction()
        {
            var currentDirectory = Environment.CurrentDirectory;
            const string needFileExtension = ".txt";

            var rule = new Rule("Создание файла с уравнением")
                .Centered()
                .RuleStyle("green");
            AnsiConsole.Write(rule);

            AnsiConsole.MarkupLine($"Добавьте файл в [green]{currentDirectory}[/] с расширением {needFileExtension}\n");

            _ = AnsiConsole
                .Prompt(new ConfirmationPrompt("Перейти в проводник?"))
                    ? Process.Start("explorer.exe", currentDirectory)
                    : null;

            while (true)
            {
                try
                {
                    var quadraticEquation = filesRepository.GetQuadraticEquations();
                    var rootQuadraticEquations = quadraticEquationsService
                        .SolutionQuadraticEquation(quadraticEquation);

                    for (int i = 0; i < rootQuadraticEquations.Count(); i++)
                    {
                        AnsiConsole
                            .MarkupLine($"\n[lime]{
                                quadraticEquation.ElementAt(i).ToViewQuadraticEquation()}[/]");
                        OutputResult(rootQuadraticEquations.ElementAt(i));
                    }

                    break;
                }
                catch (FileNotFoundException ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.NoStackTrace);
                    continue;
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                    break;
                }

            }
        }

        private static void OutputResult(RootQuadraticEquation? root)
        {
            string result;

            if (root.HasValue)
            {
                result = $"x1 = {root.Value.X1}, " +
                    $"x2 = {root.Value.X2}";
                AnsiConsole.MarkupLine($"\n[green]Ответ: {result}[/]\n");
            }
            else
            {
                result = "Квадратное уравнение не имеет корней";
                AnsiConsole.MarkupLine($"\n[red]Ответ: {result}[/]\n");
            }
        }
    }
}
