// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace GitUpdater.Models
{
    public class FetchedItem
    {
        public RefStatus Status { get; set; }
        public string Summary { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Reason { get; set; }
    }
}
