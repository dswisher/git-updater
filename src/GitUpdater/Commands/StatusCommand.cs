// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GitUpdater.Helpers;
using GitUpdater.Settings;
using GitUpdater.Wrapper;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GitUpdater.Commands
{
    public class StatusCommand : AsyncCommand<StatusSettings>
    {
        private readonly IRepoFinder repoFinder;
        private readonly IGitChecker gitChecker;
        private readonly IAnsiConsole ansiConsole;

        public StatusCommand(
            IRepoFinder repoFinder,
            IGitChecker gitChecker,
            IAnsiConsole ansiConsole)
        {
            this.repoFinder = repoFinder;
            this.gitChecker = gitChecker;
            this.ansiConsole = ansiConsole;
        }


        public override async Task<int> ExecuteAsync(CommandContext context, StatusSettings settings)
        {
            var repoDirList = repoFinder.FindRepos(settings.Directory);

            if (!repoDirList.Any())
            {
                ansiConsole.MarkupLine("[red]No repositories found.[/]");
                return 1;
            }

            foreach (var dirInfo in repoDirList)
            {
                try
                {
                    // TODO - need a CancellationToken
                    var problems = await gitChecker.CheckRepoAsync(dirInfo.FullPath, CancellationToken.None);

                    if (problems.Any())
                    {
                        ansiConsole.MarkupLine($":cross_mark: {dirInfo.RelativePath}:");

                        foreach (var issue in problems)
                        {
                            ansiConsole.MarkupLine($"   - {issue}");
                        }
                    }
                }
                catch
                {
                    AnsiConsole.WriteLine("Error checking repo {0}", dirInfo.RelativePath);
                    throw;
                }
            }

            return 0;
        }
    }
}
