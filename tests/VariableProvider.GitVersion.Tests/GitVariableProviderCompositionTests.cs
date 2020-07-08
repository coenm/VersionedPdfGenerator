namespace VariableProvider.GitVersion.Tests
{
    using System;

    using Core;
    using Core.Formatters;

    using FluentAssertions;

    using Xunit;

    public class GitVariableProviderCompositionTests
    {
        // todo. fix
        private const string DIR = "C:\\Users\\c.van.den.munckhof\\Documents\\Coen\\Git\\EagleEye\\src\\";
        private const string DIR2 = "\"C:\\Users\\c.van.den.munckhof\\Documents\\Coen\\Git\\EagleEye\\src\\Coen van den Munckhof.docx\"";

        [Fact]
        public void Provide_ShouldReturnGitCommitSha_WhenKeyIsGitSha()
        {
            // arrange
            var dateTimeFormatter = new ConfigurableDateTimeFormatter("", "", "");
            var sut = new GitVersionVariableProviderComposition(dateTimeFormatter);
            var context = new Context(DateTime.Now, DIR2);

            // act
            var result = sut.Provide(context, "gitversion.sha", null);

            // assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}