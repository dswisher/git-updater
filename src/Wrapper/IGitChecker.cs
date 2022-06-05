// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using LibGit2Sharp;

namespace GitUpdater.Wrapper
{
    public interface IGitChecker
    {
        List<string> CheckRepo(IRepository repo);
    }
}
