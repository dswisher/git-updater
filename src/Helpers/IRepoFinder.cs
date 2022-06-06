// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using GitUpdater.Models;

namespace GitUpdater.Helpers
{
    public interface IRepoFinder
    {
        List<RepoDirInfo> FindRepos(string startDir);
    }
}
