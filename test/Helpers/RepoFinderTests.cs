using System.IO;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using GitUpdater.Helpers;
using Xunit;

namespace GitUpdater.Tests.Helpers
{
    public class RepoFinderTests
    {
        private const string Proj1 = "my-proj";
        private const string Child1 = "sub1";
        private const string Child2 = "sub2";

        private readonly string currentDir;

        private readonly MockFileSystem fs;
        private readonly RepoFinder finder;


        public RepoFinderTests()
        {
            currentDir = Path.Join(Path.DirectorySeparatorChar + "users", "fred", Proj1);

            fs = new MockFileSystem(null, currentDir);

            finder = new RepoFinder(fs);
        }


        [Fact]
        public void CanFindInCurrentDir()
        {
            // Arrange
            fs.AddDirectory(Path.Join(currentDir, ".git"));

            // Act
            var repos = finder.FindRepos(currentDir);

            // Assert
            repos.Should().HaveCount(1);

            repos[0].FullPath.Should().Be(currentDir);
            repos[0].RelativePath.Should().Be(Proj1);
        }


        [Fact]
        public void CanFindChildren()
        {
            // Arrange
            fs.AddDirectory(Path.Join(currentDir, Child1, ".git"));
            fs.AddDirectory(Path.Join(currentDir, Child2, ".git"));

            // Act
            var repos = finder.FindRepos(currentDir);

            // Assert
            repos.Should().HaveCount(2);

            repos[0].FullPath.Should().Be(Path.Join(currentDir, Child1));
            repos[1].FullPath.Should().Be(Path.Join(currentDir, Child2));

            // TODO - xyzzy - check relative paths
        }
    }
}
