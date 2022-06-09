// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Humanizer;
using LibGit2Sharp;

namespace GitUpdater.Wrapper
{
    public class GitChecker : IGitChecker
    {
        public List<string> CheckRepo(IRepository repo)
        {
            // Keep a list of problems that have been found
            var problems = new List<string>();

            // Look for files that have not been committed
            CheckFiles(problems, repo);

            // Check the branch
            // TODO - branch check

            // Check for un-pushed commits and un-merged commits
            var ahead = repo.Head.TrackingDetails.AheadBy.GetValueOrDefault();
            var behind = repo.Head.TrackingDetails.BehindBy.GetValueOrDefault();

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


        private void CheckFiles(List<string> problems, IRepository repo)
        {
            // Keep track of counts by file state
            var stateCounts = new Dictionary<FileStatus, int>();

            // Go through all the files (git status) and tally up counts by state
            var options = new StatusOptions
            {
                IncludeIgnored = false,
                RecurseIgnoredDirs = false
            };

            foreach (var item in repo.RetrieveStatus(options))
            {
                if (stateCounts.ContainsKey(item.State))
                {
                    stateCounts[item.State] += 1;
                }
                else
                {
                    stateCounts.Add(item.State, 1);
                }
            }

            // Add problem reports, based on the state
            var total = stateCounts.Values.Sum();

            if (total > 0)
            {
                problems.Add($"has {"uncommitted file".ToQuantity(total)}.");
            }
        }
    }
}
