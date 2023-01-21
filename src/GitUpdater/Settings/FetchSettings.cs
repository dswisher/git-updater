// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel;
using Spectre.Console.Cli;

namespace GitUpdater.Settings
{
    public class FetchSettings : AbstractGitCommandSettings
    {
        [Description("Show what would be done, without making any changes.")]
        [CommandOption("--dry-run")]
        public bool? DryRun { get; set; }
    }
}
