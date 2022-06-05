using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GitUpdater.Helpers
{
    public class RepoFinder : IRepoFinder
    {
        public List<string> FindRepos(string startDir)
        {
            // TODO - this should return both the full path and the relative path to startDir
            // TODO - if no repos found, walk upward, to handle the case where we're in a git repo subdir

            var pending = new Queue<DirectoryInfo>();
            var result = new List<string>();

            if (string.IsNullOrEmpty(startDir))
            {
                pending.Enqueue(new DirectoryInfo(Environment.CurrentDirectory));
            }
            else
            {
                pending.Enqueue(new DirectoryInfo(startDir));
            }

            while (pending.Count > 0)
            {
                var dir = pending.Dequeue();

                // If the directory contains a .git directory, it is a repo
                if (dir.GetDirectories(".git").Any())
                {
                    result.Add(dir.FullName);
                }
                else
                {
                    // Not a git repo, scan subdirectories
                    foreach (var child in dir.GetDirectories())
                    {
                        pending.Enqueue(child);
                    }
                }
            }

            return result;
        }
    }
}
