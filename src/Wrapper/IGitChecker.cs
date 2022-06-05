using System.Collections.Generic;
using LibGit2Sharp;

namespace GitUpdater.Wrapper
{
    public interface IGitChecker
    {
        List<string> CheckRepo(IRepository repo);
    }
}
