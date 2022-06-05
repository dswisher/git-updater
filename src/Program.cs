// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using GitUpdater.Commands;
using GitUpdater.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GitUpdater
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection();
                Startup.ConfigureServices(services);

                var registrar = new TypeRegistrar(services);

                var app = new CommandApp(registrar);
                app.Configure(config =>
                {
                    config.AddCommand<CheckCommand>("check")
                        .WithDescription("Check each git repository for issues and report them.");
                });

                return app.Run(args);
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
                return 1;
            }
        }
    }
}
