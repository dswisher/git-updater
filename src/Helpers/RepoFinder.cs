// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using GitUpdater.Models;

namespace GitUpdater.Helpers
{
    public class RepoFinder : IRepoFinder
    {
        private readonly IFileSystem fileSystem;

        public RepoFinder(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }


        public List<RepoDirInfo> FindRepos(string startDir)
        {
            // TODO - this should return both the full path and the relative path to startDir
            // TODO - if no repos found, walk upward, to handle the case where we're in a git repo sub-dir

            var pending = new Queue<IDirectoryInfo>();
            var result = new List<RepoDirInfo>();

            if (string.IsNullOrEmpty(startDir))
            {
                pending.Enqueue(fileSystem.DirectoryInfo.FromDirectoryName(Environment.CurrentDirectory));
            }
            else
            {
                pending.Enqueue(fileSystem.DirectoryInfo.FromDirectoryName(startDir));
            }

            while (pending.Count > 0)
            {
                var dir = pending.Dequeue();

                // If the directory contains a .git directory, it is a repo
                if (dir.GetDirectories(".git").Any())
                {
                    var info = new RepoDirInfo
                    {
                        // TODO - xyzzy - compute the relative path
                        FullPath = dir.FullName
                    };

                    result.Add(info);
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
