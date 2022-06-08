// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using GitUpdater.Models;

namespace GitUpdater.Parsers
{
    public interface IFetchOutputParser
    {
        FetchResult Parse(string output);
    }
}
