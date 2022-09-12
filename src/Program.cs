// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using GitUpdater.Commands;
using GitUpdater.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GitUpdater
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            // TODO - need a CancellationToken

            try
            {
                var services = new ServiceCollection();
                Startup.ConfigureServices(services);

                var registrar = new TypeRegistrar(services);

                var app = new CommandApp(registrar);
                app.Configure(config =>
                {
                    config.UseStrictParsing();

                    config.AddCommand<FetchCommand>("fetch")
                        .WithDescription("Fetch commits from origin.");

                    config.AddCommand<RebaseCommand>("rebase")
                        .WithDescription("Bring each repo up to date by merging upstream changes via a rebase, if it would not conflict.");

                    config.AddCommand<StatusCommand>("status")
                        .WithDescription("Check the status of each git repository.");
                });

                return await app.RunAsync(args);
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
                return 1;
            }
        }
    }
}
