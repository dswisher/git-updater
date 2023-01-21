// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading;
using System.Threading.Tasks;
using GitExecWrapper.Commands;
using GitExecWrapper.Helpers;
using GitExecWrapper.Models;

namespace GitUpdater.Wrapper
{
    public class GitFetcher : IGitFetcher
    {
        public async Task<FetchResult> FetchAsync(string repoPath, bool dryRun, CancellationToken cancellationToken)
        {
            var command = new FetchCommand(repoPath)
                .DryRun(dryRun);

            var result = await command.ExecAsync(cancellationToken);

            return result;
        }
    }
}
