namespace VariableProvider.Git.Tests
{
    using System;

    using Core;
    using FluentAssertions;
    using Xunit;

    public class GitVariableProviderCompositionTests
    {
        // todo. fix
        private const string DIR = "D:\\";

        [Fact(Skip="local test")]
        public void Provide_ShouldReturnGitCommitSha_WhenKeyIsGitSha()
        {
            // arrange
            var sut = new GitVariableProviderComposition();
            var context = new Context(DateTime.Now, DIR);

            // act
            var result = sut.Provide(context, "git.sha", null);

            // assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact(Skip = "local test")]
        public void Provide_ShouldReturnGitRootDir_WhenKeyIsGitRootDir()
        {
            // arrange
            var sut = new GitVariableProviderComposition();
            var context = new Context(DateTime.Now, DIR);

            // act
            var result = sut.Provide(context, "git.rootdir", null);

            // assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}