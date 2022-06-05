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
    public class CheckCommand : Command<CheckSettings>
    {
        private readonly IRepoFinder repoFinder;
        private readonly IGitChecker gitChecker;
        private readonly IAnsiConsole ansiConsole;

        public CheckCommand(
            IRepoFinder repoFinder,
            IGitChecker gitChecker,
            IAnsiConsole ansiConsole)
        {
            this.repoFinder = repoFinder;
            this.gitChecker = gitChecker;
            this.ansiConsole = ansiConsole;
        }


        public override int Execute(CommandContext context, CheckSettings settings)
        {
            var repoDirList = repoFinder.FindRepos(settings.Directory);

            if (!repoDirList.Any())
            {
                ansiConsole.MarkupLine("[red]No repositories found.[/]");
                return 1;
            }

            // TODO - if more than two repos were found, set up status bars

            foreach (var repoDir in repoDirList)
            {
                using (var repo = new Repository(repoDir))
                {
                    var problems = gitChecker.CheckRepo(repo);

                    if (problems.Any())
                    {
                        ansiConsole.MarkupLine($":cross_mark: {repoDir}:");

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
