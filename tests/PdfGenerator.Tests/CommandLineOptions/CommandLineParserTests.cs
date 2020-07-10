namespace PdfGenerator.Tests.CommandLineOptions
{
    using FluentAssertions;
    using PdfGenerator.CommandLineOptions.Verbs;
    using Xunit;

    using Sut = PdfGenerator.CommandLineOptions.CommandLineParser;

    public class CommandLineParserTests
    {
        [Fact]
        public void Parse_ShouldReturnCreateOptions_WhenInputIsCorrect()
        {
            // arrange
            var args = new[]
                           {
                               "create",
                               "C:\\Users\\coenm\\Documents\\MyDocument.docx",
                               "-c",
                               "D:\\VersionedPdfGenerator.yaml",
                               "-o",
                               "{filepath}{PathSeparator}{now:yyyyMMddHHmmss}_{env.username:lower}_{filenamebase}.pdf",
                               "--dry-run",
                               "--vars",
                               "Author=Coen",
                               "Age=1",
                               "SHA={gitversion.sha}",
                               "Desc=this is some text",
                               "GitVersion=v.{GitVersion.FullSemVer} -- Last commit {gitVersion.CommitDate}"
                           };

            // act
            var result = Sut.Parse(args);

            // assert
            result.Should()
                  .BeEquivalentTo(new CreateOptions
                                      {
                                          ConfigFile = @"D:\VersionedPdfGenerator.yaml",
                                          AdditionalVariables = new[]
                                                                    {
                                                                        "Author=Coen",
                                                                        "Age=1",
                                                                        "SHA={gitversion.sha}",
                                                                        "Desc=this is some text",
                                                                        "GitVersion=v.{GitVersion.FullSemVer} -- Last commit {gitVersion.CommitDate}",
                                                                    },
                                          Force = false,
                                          OutputFilename = "{filepath}{PathSeparator}{now:yyyyMMddHHmmss}_{env.username:lower}_{filenamebase}.pdf",
                                          Filename = "C:\\Users\\coenm\\Documents\\MyDocument.docx",
                                      });
        }



        [Fact]
        public void Parse_ShouldReturnGenerateConfigOptions_WhenInputIsCorrect()
        {
            // arrange
            var args = new[]
                           {
                               "generate-config",
                               "D:\\VersionedPdfGenerator.yaml",
                           };

            // act
            var result = Sut.Parse(args);

            // assert
            result.Should()
                  .BeEquivalentTo(new GenerateConfigOptions
                                      {
                                          OutputFilename = "D:\\VersionedPdfGenerator.yaml",
                                      });
        }
    }
}