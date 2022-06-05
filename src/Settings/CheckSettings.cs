using System.ComponentModel;
using Spectre.Console.Cli;

namespace GitUpdater.Settings
{
    public class CheckSettings : CommandSettings
    {
        [Description("The directory where the search for git repos begins.")]
        [CommandArgument(0, "[DIRECTORY]")]
        public string Directory { get; set; }
    }
}
