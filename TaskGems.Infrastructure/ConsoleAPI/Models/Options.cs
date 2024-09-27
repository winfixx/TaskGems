using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskGems.Infrastructure.ConsoleAPI.Models
{
    internal class Options
    {
        public Options() { }
        public Options(IEnumerable<Option> options) => OptionsList = options;

        public IEnumerable<Option> OptionsList { get; private set; } = [];

        public void Add(Option option)
        {
            var newOptions = new Option[OptionsList.Count()];

            newOptions[0] = option;

            for (int i = 1; i < OptionsList.Count(); i++)
            {
                newOptions[i] = OptionsList.ElementAt(i);
            }

            OptionsList = newOptions;
        }

        public Action GetActionByName(string name)
        {
            return OptionsList.First(option => option.Name == name).Action;
        }

        public IEnumerable<string> GetNameOption()
        {
            return OptionsList.Select(option => option.Name);
        }
    }
}
