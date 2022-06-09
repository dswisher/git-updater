// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using GitUpdater.Helpers;
using GitUpdater.Settings;
using GitUpdater.Wrapper;
using LibGit2Sharp;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GitUpdater.Commands
{
    public class StatusCommand : Command<StatusSettings>
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


        public override int Execute(CommandContext context, StatusSettings settings)
        {
            var repoDirList = repoFinder.FindRepos(settings.Directory);

            if (!repoDirList.Any())
            {
                ansiConsole.MarkupLine("[red]No repositories found.[/]");
                return 1;
            }

            // TODO - if more than two repos were found, set up status bars

            foreach (var dirInfo in repoDirList)
            {
                using (var repo = new Repository(dirInfo.FullPath))
                {
                    var problems = gitChecker.CheckRepo(repo);

                    if (problems.Any())
                    {
                        ansiConsole.MarkupLine($":cross_mark: {dirInfo.RelativePath}:");

                        foreach (var issue in problems)
                        {
                            ansiConsole.MarkupLine($"   - {issue}");
                        }
                    }
                }
            }

            return 0;
        }
    }
}