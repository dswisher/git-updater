// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using GitUpdater.Models;
using GitUpdater.Parsers;

namespace GitUpdater.Wrapper
{
    public class GitFetcher : IGitFetcher
    {
        private readonly IFetchOutputParser parser;

        public GitFetcher(IFetchOutputParser parser)
        {
            this.parser = parser;
        }


        public async Task<FetchResult> FetchAsync(string repoPath, bool dryRun)
        {
            // Build the command line
            var args = "fetch --verbose";

            if (dryRun)
            {
                args += " --dry-run";
            }

            // Execute the fetch command
            var (stdout, stderr) = await SimpleExec.Command.ReadAsync("git", args, workingDirectory: repoPath);

            // Parse the result
            try
            {
                return parser.Parse(string.IsNullOrEmpty(stderr) ? stdout : stderr);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error parsing fetch output: {stderr}", ex);
            }
        }
    }
}
