using System.Collections.Generic;
using System.Linq;
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

            // Check for un-pushed commits
            // TODO - check un-pushed commits

            // Check for un-merged commits
            // TODO - check to un-merged commits

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
                problems.Add($"Has {total} uncommitted files.");
            }
        }
    }
}
