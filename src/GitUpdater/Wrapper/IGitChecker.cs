// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GitUpdater.Wrapper
{
    public interface IGitChecker
    {
        Task<List<string>> CheckRepoAsync(string repoPath, CancellationToken cancellationToken);
    }
}
