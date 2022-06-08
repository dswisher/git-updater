// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using GitUpdater.Models;

namespace GitUpdater.Wrapper
{
    public interface IGitFetcher
    {
        Task<FetchResult> FetchAsync(string repoPath, bool dryRun);
    }
}
