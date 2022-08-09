using System;
using FluentAssertions;
using GitUpdater.Commands;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Spectre.Console;
using Xunit;

namespace GitUpdater.Tests
{
    public class StartupTests
    {
        private readonly ServiceProvider container;

        public StartupTests()
        {
            // Set up the services, as normal
            var services = new ServiceCollection();

            Startup.ConfigureServices(services);

            // Add shims for things that Spectre automagically includes
            services.AddSingleton(new Mock<IAnsiConsole>().Object);

            // Build the container that will be used for tests
            container = services.BuildServiceProvider();
        }


        [Theory]
        [InlineData(typeof(FetchCommand))]
        [InlineData(typeof(StatusCommand))]
        public void CanResolveCommands(Type type)
        {
            // Act
            var command = container.GetRequiredService(type);

            // Assert
            command.Should().NotBeNull();
        }
    }
}
