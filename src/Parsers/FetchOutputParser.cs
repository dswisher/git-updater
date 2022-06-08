// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GitUpdater.Models;

namespace GitUpdater.Parsers
{
    public class FetchOutputParser : IFetchOutputParser
    {
        private readonly Regex regex = new Regex(@"^(?<flag>.).*$", RegexOptions.Compiled);
        private readonly Dictionary<string, RefStatus> flags = new Dictionary<string, RefStatus>();

        public FetchOutputParser()
        {
            flags.Add(" ", RefStatus.Fetched);
            flags.Add("+", RefStatus.ForcedUpdate);
            flags.Add("-", RefStatus.Pruned);
            flags.Add("t", RefStatus.TagUpdate);
            flags.Add("*", RefStatus.NewRef);
            flags.Add("!", RefStatus.Failed);
            flags.Add("=", RefStatus.UpToDate);
        }

        public FetchResult Parse(string output)
        {
            var result = new FetchResult();

            // Parse line-by-line
            bool first = true;
            var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                // Ignore the first line, which should be the "From repo" text...
                if (first)
                {
                    first = false;
                    continue;
                }

                var match = regex.Match(line);

                if (!match.Success)
                {
                    throw new Exception($"Could not parse fetch output line: {line}");
                }

                var item = new FetchedItem
                {
                    // TODO - pick out other bits
                    Status = flags[match.Groups["flag"].Value]
                };

                result.Items.Add(item);
            }

            return result;
        }
    }
}
