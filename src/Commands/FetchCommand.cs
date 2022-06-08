// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using GitUpdater.Helpers;
using GitUpdater.Settings;
using GitUpdater.Wrapper;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GitUpdater.Commands
{
    public class FetchCommand : AsyncCommand<FetchSettings>
    {
        private readonly IRepoFinder repoFinder;
        private readonly IGitFetcher fetcher;
        private readonly IAnsiConsole ansiConsole;

        public FetchCommand(IRepoFinder repoFinder, IGitFetcher fetcher, IAnsiConsole ansiConsole)
        {
            this.repoFinder = repoFinder;
            this.fetcher = fetcher;
            this.ansiConsole = ansiConsole;
        }


        public override async Task<int> ExecuteAsync(CommandContext context, FetchSettings settings)
        {
            var repoDirList = repoFinder.FindRepos(settings.Directory);

            if (!repoDirList.Any())
            {
                ansiConsole.MarkupLine("[red]No repositories found.[/]");
                return 1;
            }

            // TODO - use progress

            foreach (var dirInfo in repoDirList)
            {
                ansiConsole.MarkupLine($"Fetching {dirInfo.RelativePath}...");
                await fetcher.FetchAsync(dirInfo.FullPath, settings.DryRun.GetValueOrDefault());
            }

            return 0;
        }
    }
}
