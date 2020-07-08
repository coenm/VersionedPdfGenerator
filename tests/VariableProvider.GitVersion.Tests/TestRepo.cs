// namespace VariableProvider.GitVersion.Tests
// {
//     using System;
//     using System.Collections.Generic;
//     using System.Diagnostics;
//     using System.IO;
//
//     using GitTools.Testing;
//
//     using LibGit2Sharp;
//
//     using Xunit;
//
//     public class Coen
//     {
//         [Fact]
//         public void Abc()
//         {
//             using (var repo = CreateRepo("coen", "a.b.c@github.com"))
//             {
//                 repo.Commit()
//             }
//
//             using (var fixture = new EmptyRepositoryFixture())
//             {
//                 // fixture.Repository.Com;
//                 fixture.MakeACommit();
//                 fixture.MakeACommit();
//                 fixture.MakeATaggedCommit("1.0.0");
//                 fixture.BranchTo("develop");
//                 fixture.MakeACommit();
//                 fixture.Checkout("master");
//                 fixture.MergeNoFF("develop");
//             }
//         }
//
//
//         /*
//         public static Commit CreateFileAndCommit(
//             this IRepository repository,
//             string relativeFileName,
//             string commitMessage = null)
//         {
//             string path = Path.Combine(repository.Info.WorkingDirectory, relativeFileName);
//             if (File.Exists(path))
//                 File.Delete(path);
//             string contents = Guid.NewGuid().ToString();
//             File.WriteAllText(path, contents);
//             Commands.Stage(repository, path);
//
//             return repository.Commit($"Test Commit for file '{(object)relativeFileName}' - {(object)commitMessage}", Generate.SignatureNow(), Generate.SignatureNow());
//         }
//         */
//
//
//         private IRepository CreateRepo(string username, string email)
//         {
//             var path = DirectoryHelper.GetTempPath();
//             var repo = CreateNewRepository(path);
//             repo.Config.Set<string>("user.name", username);
//             repo.Config.Set<string>("user.email", email);
//             return repo;
//         }
//
//
//
//         private static IRepository CreateNewRepository(string path)
//         {
//             Repository.Init(path);
//             // Console.WriteLine("Created git repository at '{0}'", (object)path);
//             return (IRepository)new Repository(path);
//         }
//     }
//
//     public class Coen : IDisposable
//     {
//         public Coen(IRepository repository)
//         {
//             Repository = repository;
//             Repository.Config.Set<string>("user.name", "Test");
//             Repository.Config.Set<string>("user.email", "test@email.com");
//         }
//
//         public IRepository Repository { get; private set; }
//
//         public string RepositoryPath => this.Repository.Info.WorkingDirectory.TrimEnd(new char[1] { '\\' });
//
//         public virtual void Dispose()
//         {
//             Repository.Dispose();
//
//             try
//             {
//                 DirectoryHelper.DeleteDirectory(this.RepositoryPath);
//             }
//             catch (Exception ex)
//             {
//                 // RepositoryFixtureBase.Logger.InfoFormat("Failed to clean up repository path at {0}. Received exception: {1}", (object)this.RepositoryPath, (object)ex.Message);
//             }
//         }
//
//         public void Checkout(string branch)
//         {
//             Commands.Checkout(Repository, branch);
//         }
//
//         public void MakeATaggedCommit(string tag)
//         {
//             this.MakeACommit();
//             this.ApplyTag(tag);
//         }
//
//         public void ApplyTag(string tag)
//         {
//             this.Repository.ApplyTag(tag);
//         }
//
//         public void BranchTo(string branchName, string @as = null)
//         {
//             Commands.Checkout(Repository, Repository.CreateBranch(branchName));
//         }
//
//         public void BranchToFromTag(string branchName, string fromTag, string onBranch, string @as = null)
//         {
//             Commands.Checkout(Repository, Repository.CreateBranch(branchName));
//         }
//
//         public void MakeACommit()
//         {
//             this.Repository.MakeACommit((string)null);
//         }
//
//         public void MergeNoFF(string mergeSource)
//         {
//             this.Repository.MergeNoFF(mergeSource, Generate.SignatureNow());
//         }
//
//         public LocalRepositoryFixture CloneRepository()
//         {
//             string tempPath = DirectoryHelper.GetTempPath();
//             LibGit2Sharp.Repository.Clone(this.RepositoryPath, tempPath);
//             return new LocalRepositoryFixture((IRepository)new LibGit2Sharp.Repository(tempPath));
//         }
//     }
//
//
//     public static class Generate
//     {
//         public static LibGit2Sharp.Signature SignatureNow()
//         {
//             return Generate.Signature(VirtualTime.Now);
//         }
//
//         public static LibGit2Sharp.Signature Signature(DateTimeOffset dateTimeOffset)
//         {
//             return new LibGit2Sharp.Signature("A. U. Thor", "thor@valhalla.asgard.com", dateTimeOffset);
//         }
//     }
//
//     internal static class DirectoryHelper
//     {
//         public static string GetTempPath()
//         {
//             return Path.Combine(Path.GetTempPath(), "TestRepositories", Guid.NewGuid().ToString());
//         }
//
//         public static void DeleteDirectory(string directoryPath)
//         {
//             if (!Directory.Exists(directoryPath))
//             {
//                 Trace.WriteLine($"Directory '{(object)directoryPath}' is missing and can't be removed.");
//             }
//             else
//             {
//                 var files = Directory.GetFiles(directoryPath);
//                 var directories = Directory.GetDirectories(directoryPath);
//                 foreach (string path in files)
//                 {
//                     File.SetAttributes(path, FileAttributes.Normal);
//                     File.Delete(path);
//                 }
//                 foreach (string directoryPath1 in directories)
//                     DeleteDirectory(directoryPath1);
//
//                 File.SetAttributes(directoryPath, FileAttributes.Normal);
//                 try
//                 {
//                     Directory.Delete(directoryPath, false);
//                 }
//                 catch (IOException ex)
//                 {
//                     Trace.WriteLine(string.Format("{0}The directory '{1}' could not be deleted!{0}Most of the time, this is due to an external process accessing the files in the temporary repositories created during the test runs, and keeping a handle on the directory, thus preventing the deletion of those files.{0}Known and common causes include:{0}- Windows Search Indexer (go to the Indexing Options, in the Windows Control Panel, and exclude the bin folder of LibGit2Sharp.Tests){0}- Antivirus (exclude the bin folder of LibGit2Sharp.Tests from the paths scanned by your real-time antivirus){0}", (object)Environment.NewLine, (object)Path.GetFullPath(directoryPath)));
//                 }
//             }
//         }
//     }
//
// }