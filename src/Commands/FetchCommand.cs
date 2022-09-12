// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GitExecWrapper.Models;
using GitUpdater.Helpers;
using GitUpdater.Settings;
using GitUpdater.Wrapper;
using Humanizer;
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

            foreach (var dirInfo in repoDirList.OrderBy(x => x.RelativePath))
            {
                ansiConsole.MarkupLine($"Fetching {dirInfo.RelativePath}...");

                // TODO - need a CancellationToken
                var result = await fetcher.FetchAsync(dirInfo.FullPath, settings.DryRun.GetValueOrDefault(), CancellationToken.None);

                var updatedBranches = result.Items.Count(x => x.Status == RefStatus.Fetched);
                var newBranches = result.Items.Count(x => x.Status == RefStatus.NewRef);

                if (updatedBranches > 0)
                {
                    if (newBranches > 0)
                    {
                        AnsiConsole.WriteLine($"   -> Received updates for {"existing branch".ToQuantity(updatedBranches)} and {"new branch".ToQuantity(newBranches)}.");
                    }
                    else
                    {
                        AnsiConsole.WriteLine($"   -> Received updates for {"existing branch".ToQuantity(updatedBranches)}.");
                    }
                }
                else if (newBranches > 0)
                {
                    AnsiConsole.WriteLine($"   -> Received {"new branch".ToQuantity(newBranches)}.");
                }
            }

            return 0;
        }
    }
}
