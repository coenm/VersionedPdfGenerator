# VersionedPdfGenerator

Opens docx files, updates DOCVARIABLES, and saves as pdf file.

## Why

A few years ago, when you were editing word documents, you probably had multiple versions of your document in your folder and soon it becomes a mess. Using Git (or Mercurial, or SVN, or ..) as a version control system solves this issue.
Although it is somewhat difficult (or impossible?) to 'diff' or 'merge' between versions (of docx files) it is useful when you write good and clear commit messages.

The problem of multiple versions still exists when you publish printed versions of your document when gathering feedback for instance. You can update the version manually in the word document before printing it but it somewhat anoying to copy over a git sha when you want that information in the printed document.

## DOCVARIABLE and INCLUDEPICTURE

DocVariables are some sort of placeholders in a Word document that can be replaced/updated with the content/value of the DocVariable at a later point in time. For instance, you can add a DOCVARIABLE with the name 'MyVersion' at a specific location in the document. The same holds for INCLUDEPICTURE. The INCLUDEPICTURE can refer to an URL or a physical location at the PC.

## Our Solution

This project uses Word Interop to open the Word document, updates the pre-configured DOCVARIABLES, and generates a pdf file.
The value of the Docvariables can be specified using simple expressions in a config file or can be set in the commandline. Using these expressions you can assign a the sha of a git commit, the branch name, the name of the committer etc to docvariables.

Our solution can also generate QR codes making it possible to quickly scan such code to go to the correct commit on GitHub for instance. In order to make this possible, our PdfGenerator also acts as a ASP.NET core host generating images of QR codes.

## Variables and methods

You can use static text, variables, and methods to assign values to DOCVARIABLES. Variables and methods are surrounded with curly brackets.

Let's clarify this concept using an example. The config file contains a section `DocVariables` which can be like this.

```yaml
DocVariables:
 Project: 'ProjectX' #1
 MyVersion: 'version: {git.sha}' #2
 Copyright: 'copyright 2010-{now:yyyy}' #3
 MyOperatingSystem: 'My OS is : {env.OS}' #4
 MyOperatingSystemUppercase: 'my os is {Upper({env.OS})}.' #5
 RepoUrlQrCode: '{host}/qr/url/{UrlEncode(https://github.com/coenm/VersionedPdfGenerator/commit/{Git.Sha})}' #6
```

1. Adds docvariable named `Project` with static text `ProjectX` to the document.
2. Assuming the current git commit sha is 'ac2708842a5de915223e0edc899177cad18b252b', the variable '`{git.sha}` will be evaluated and the final content of the document variable `MyVersion` will be `version: ac2708842a5de915223e0edc899177cad18b252b`.
3. Variable `{now}` evaluates to the current date time and is formatted using the default formatting (this can also be configured in the config file). The formatting of the date time in `{now:yyyy}` will be overwritten for this specific variable. Therefore, the docvariable `Copyright` will be added to the document with `copyright 2010-2020` (assuming it is still 2020) as its value.
4. Environment variables are accessable using the prefix `env.`. This config line will add a docvariable named `MyOperatingSystem` with the value `My OS is : Windows10` (assuming the environment variable `OS` is `Windows10`).
5. Example using a method combined with a variable. The `Upper()` method will uppercase the content. I.e. after evaluation the docvariable will be `my os is WINDOWS_NT` when the `env.OS` returned `Windows_NT`.
6. d

The variables supported at the moment can be found [here](documentation/Variables.md).

An example of a config file can be found [in the example directory](example/.VersionedPdfGenerator.yaml).

### Methods

The following methods are implemented:

- Upper;
- Lower;
- Trim;
- TrimLeft;
- TrimRight;
- EncodeUrl;
- DecodeUrl.

### DateTime formatting

Using variables as `{now}` or `{Git.Committer.CommitDate}` (using c# DateTime instances) can be formatted to strings using the format feature of a variable. This can be specified using the semicolumn within the variable.
I.e. `{now:yyyy-MM-dd}` will result in `2020-04-12` and `{now:yyyyMMdd_HHmmss}` results in `20200412_231218`.

All date/time formatting options can be found at [docs.microsoft.com](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings).

## Git and GitVersion

Git is an opensource version control system. Every commit result in a new set of properties (i.e. sha, committer, commit date, branch etc.) which can be used using the GitVariableProvider.

[GitVersion](https://gitversion.net/docs/) is a tool to create/calculate a version from the current git commit (in combination with the branches, tags, and its configuration). The GitVersionDocumentProvider provides all this information as variables.

## Extensibility

At this moment, you can only extend the document variables by creating environment variables before running the application.

You can also create a pull request with a better solution.

## QR code generation

todo

### Security warning

todo

## Examples

todo.

## FAQ

Is Git the best match for word documents?
> No, probably not, as git handles plain text documents ....

Why not use LaTeX, Markdown, LaTeX, HTML or something else for writing documents? It is probably much easier to achieve the same.
> Sometimes I use word documents, sometimes I don't.

I don't like generating pdfs using Word Interop. Is there another way?
> Yes there are multiple libraries capable of opening, updating doc variables and generating pdfs but they require paid licenses. Please open a pull request if you have a solution eliminating Word Interop.
