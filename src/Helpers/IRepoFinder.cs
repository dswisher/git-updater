// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace GitUpdater.Helpers
{
    public interface IRepoFinder
    {
        List<string> FindRepos(string startDir);
    }
}
