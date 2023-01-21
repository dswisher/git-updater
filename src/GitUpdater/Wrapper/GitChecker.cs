// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GitExecWrapper.Commands;
using GitExecWrapper.Helpers;
using GitExecWrapper.Models;
using Humanizer;

namespace GitUpdater.Wrapper
{
    public class GitChecker : IGitChecker
    {
        public async Task<List<string>> CheckRepoAsync(string repoPath, CancellationToken cancellationToken)
        {
            // Keep a list of problems that have been found
            var problems = new List<string>();

            // Get the status for this repo
            var status = await new StatusCommand(repoPath).ExecAsync(cancellationToken);

            // Look for files that have not been committed
            CheckFiles(problems, status);

            // Check the branch
            // TODO - Get the default upstream branch for the current remote, and verify we are on it.
            // See https://github.com/dswisher/git-updater/issues/7
            var remotes = await new RemoteCommand(repoPath).ExecAsync(cancellationToken);
            var branches = await new BranchCommand(repoPath).ExecAsync(cancellationToken);

            // Check for un-pushed commits and un-merged commits
            var ahead = status.CommitsAhead.GetValueOrDefault();
            var behind = status.CommitsBehind.GetValueOrDefault();

            if (ahead > 0)
            {
                if (behind > 0)
                {
                    problems.Add($"is ahead by {ahead} and behind by {behind} commits.");
                }
                else
                {
                    problems.Add($"is ahead by {"commit".ToQuantity(ahead)}.");
                }
            }
            else if (behind > 0)
            {
                problems.Add($"is behind by {"commit".ToQuantity(behind)}.");
            }

            // Return what has been found (if anything)
            return problems;
        }


        private void CheckFiles(List<string> problems, StatusResult status)
        {
            // Go through all the items, looking for things that need to be committed
            var needsCommit = 0;

            foreach (var item in status.Items)
            {
                // TODO - move this logic into git-exec-wrapper and expose in a "NeedsCommit" count property, or perhaps an "IsDirty" boolean
                if (item.IndexStatus != FileStatus.Unchanged || item.WorkDirStatus != FileStatus.Unchanged)
                {
                    needsCommit += 1;
                }
            }

            if (needsCommit > 0)
            {
                problems.Add($"has {"uncommitted file".ToQuantity(needsCommit)}.");
            }
        }
    }
}
