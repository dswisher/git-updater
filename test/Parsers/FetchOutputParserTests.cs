using System.Text;
using FluentAssertions;
using GitUpdater.Models;
using GitUpdater.Parsers;
using Xunit;

namespace GitUpdater.Tests.Parsers
{
    public class FetchOutputParserTests
    {
        private readonly FetchOutputParser parser;

        public FetchOutputParserTests()
        {
            parser = new FetchOutputParser();
        }


        [Fact]
        public void CanParseOne()
        {
            // Arrange
            var builder = OutputBuilder.Create()
                .AddLine("   c6990d4e..7135a954  develop                              -> origin/develop");

            // Act
            var result = parser.Parse(builder.Content);

            // Assert
            result.Items.Should().HaveCount(1);

            result.Items[0].Status.Should().Be(RefStatus.Fetched);
        }


        private class OutputBuilder
        {
            private readonly StringBuilder stringBuilder = new();

            public static OutputBuilder Create(string fromLine = "From repo")
            {
                var outputBuilder = new OutputBuilder();

                outputBuilder.AddLine(fromLine);

                return outputBuilder;
            }


            public string Content => stringBuilder.ToString();


            public OutputBuilder AddLine(string line)
            {
                stringBuilder.AppendLine(line);
                return this;
            }
        }
    }
}
