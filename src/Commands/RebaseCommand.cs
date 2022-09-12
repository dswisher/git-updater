// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using GitUpdater.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GitUpdater.Commands
{
    public class RebaseCommand : AsyncCommand<RebaseSettings>
    {
        private readonly IAnsiConsole ansiConsole;

        public RebaseCommand(IAnsiConsole ansiConsole)
        {
            this.ansiConsole = ansiConsole;
        }



        public override async Task<int> ExecuteAsync(CommandContext context, RebaseSettings settings)
        {
            ansiConsole.MarkupLine("[red]Rebase is not yet implemented.[/]");
            await Task.CompletedTask;
            return 1;
        }
    }
}
