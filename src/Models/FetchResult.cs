// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace GitUpdater.Models
{
    public class FetchResult
    {
        public FetchResult()
        {
            Items = new List<FetchedItem>();
        }


        public List<FetchedItem> Items { get; }
    }
}
