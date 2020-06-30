namespace Core.Tests.Integration
{
    using System;
    using System.Collections.Generic;

    using Antlr4.Runtime;
    using Core;
    using Core.Formatters;
    using Core.Parser;
    using Core.VariableProviders;
    using Core.VariableProviders.DateTime;
    using Core.VariableProviders.FileInfo;
    using Core.VariableProviders.GitVersion;
    using Xunit;

    public class IntegrationTests
    {
        private readonly List<IVariableProvider> _providers;

        private const string JSON_CONTENT = @"
{
  ""Major"":0,
  ""Minor"":1,
  ""Patch"":0,
  ""PreReleaseTag"":""24830-GetContactsForDocumentByIds.1"",
  ""PreReleaseTagWithDash"":""-24830-GetContactsForDocumentByIds.1"",
  ""PreReleaseLabel"":""24830-GetContactsForDocumentByIds"",
  ""PreReleaseNumber"":1,
  ""WeightedPreReleaseNumber"":30001,
  ""BuildMetaData"":487,
  ""BuildMetaDataPadded"":""0487"",
  ""FullBuildMetaData"":""487.Branch.feature-24830-GetContactsForDocumentByIds.Sha.4d9353f04138567556ed8c547bc10564fa80be67"",
  ""MajorMinorPatch"":""0.1.0"",
  ""SemVer"":""0.1.0-24830-GetContactsForDocumentByIds.1"",
  ""LegacySemVer"":""0.1.0-24830-GetContactsFo1"",
  ""LegacySemVerPadded"":""0.1.0-24830-GetContact0001"",
  ""AssemblySemVer"":""0.1.0.0"",
  ""AssemblySemFileVer"":""0.1.0.0"",
  ""FullSemVer"":""0.1.0-24830-GetContactsForDocumentByIds.1+487"",
  ""InformationalVersion"":""0.1.0-24830-GetContactsForDocumentByIds.1+487.Branch.feature-24830-GetContactsForDocumentByIds.Sha.4d9353f04138567556ed8c547bc10564fa80be67"",
  ""BranchName"":""feature/24830_GetContactsForDocumentByIds"",
  ""Sha"":""4d9353f04138567556ed8c547bc10564fa80be67"",
  ""ShortSha"":""4d9353f"",
  ""NuGetVersionV2"":""0.1.0-24830-getcontact0001"",
  ""NuGetVersion"":""0.1.0-24830-getcontact0001"",
  ""NuGetPreReleaseTagV2"":""24830-getcontact0001"",
  ""NuGetPreReleaseTag"":""24830-getcontact0001"",
  ""VersionSourceSha"":""9a9de099220d3135d645ccbdaa4f76fdbf5b2c29"",
  ""CommitsSinceVersionSource"":487,
  ""CommitsSinceVersionSourcePadded"":""0487"",
  ""CommitDate"":""2020-04-01""
}
";

        public IntegrationTests()
        {
            var dateTimeFormatter = new ConfigurableDateTimeFormatter(
                                                                      "yyyy-M-d HH.mm.ss",
                                                                      "yyyy-M-d",
                                                                      "HH.mm.ss");
            var stringFormatter = new StringFormatter();
            _providers = new List<IVariableProvider>
                            {
                                new DateTimeNowVariableProvider(dateTimeFormatter),
                                new DateTimeTimeVariableProvider(dateTimeFormatter),
                                new DateTimeDateVariableProvider(dateTimeFormatter),
                                new FilenameBaseVariableProvider(),
                                new FilenameVariableProvider(),
                                new FilePathVariableProvider(),
                                new FileExtensionVariableProvider(stringFormatter),
                                new PathSeparatorVariableProvider(),
                                new EmptyVariableProvider(),
                                new EnvironmentVariableVariableProvider(stringFormatter),
                                new GitVersionVariableProvider(new GitVersionJsonReader(JSON_CONTENT)),
                            };
        }

        [Theory]
        [InlineData(
            "{filepath}{PathSeparator}{now}t a{time} aap {date} {env.OS} xx {empty}{fileextension} me  {filenamebase}.pdf ",
            "[D:\\aap\\beer\\cobra][\\][2020-12-1 15.22.23][t a][15.22.23][ aap ][2020-12-1][ ][Windows_NT][ xx ][][.docx][ me  ][File 234 Final][.pdf ]")]

        [InlineData(
            "{filepath}{Pathseparator}{now:yyyy}t a{time} aap {date} {env.OS} xx {empty}{fileextension} me  {filenamebase}.pdf ",
            "[D:\\aap\\beer\\cobra][\\][2020][t a][15.22.23][ aap ][2020-12-1][ ][Windows_NT][ xx ][][.docx][ me  ][File 234 Final][.pdf ]")]

        [InlineData(
            "{filepath}{pathSeparator}{now:yyyy}t a{time} aap {date} {env.OS:lower} xx {empty}{fileextension:upper} me  {filenamebase}.pdf ",
            "[D:\\aap\\beer\\cobra][\\][2020][t a][15.22.23][ aap ][2020-12-1][ ][windows_nt][ xx ][][.DOCX][ me  ][File 234 Final][.pdf ]")]

        [InlineData(
            "rubbish {gitversion.sha}{GitVersion.ShortShA}.pdf ",
            "[rubbish ][4d9353f04138567556ed8c547bc10564fa80be67][4d9353f][.pdf ]")]

        [InlineData(
            "{filenamebase}_v{GitVersion.MajorMinorPatch}.pdf ",
            "[File 234 Final][_v][0.1.0][.pdf ]")]

        public void Parse(string input, string expectedOutput)
        {
            // arrange
            var ctx = new Context(
                                  new DateTime(2020, 12, 1, 15, 22, 23),
                                  @"D:\aap\beer\cobra\File 234 Final.docx");
            var visitor = new LanguageVisitor(_providers, ctx);

            // act
            var context = GetExpressionContext(input);
            var result = visitor.Visit(context);

            // assert
            Assert.Equal(expectedOutput, result);
        }

        private static LanguageParser.ExpressionContext GetExpressionContext(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new LanguageLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new LanguageParser(commonTokenStream);
            return parser.expression();
        }
    }
}
