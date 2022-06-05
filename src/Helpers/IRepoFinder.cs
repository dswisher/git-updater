using System.Collections.Generic;

namespace GitUpdater.Helpers
{
    public interface IRepoFinder
    {
        List<string> FindRepos(string startDir);
    }
}
