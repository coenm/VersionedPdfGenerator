del /S /Q .\download
mkdir download

nuget.exe install LibGit2Sharp -Version 0.27.0-preview-0034 -o .\download

del .\src\PdfGenerator\costura64\*.dll
move .\download\LibGit2Sharp.NativeBinaries.2.0.298\runtimes\win-x64\native\*.dll .\src\PdfGenerator\costura64\

del .\src\PdfGenerator\costura32\*.dll
move .\download\LibGit2Sharp.NativeBinaries.2.0.298\runtimes\win-x86\native\*.dll .\src\PdfGenerator\costura32\


nuget.exe install Microsoft.Office.Interop.Word -Version 15.0.4797.1003 -o .\download
move .\download\Microsoft.Office.Interop.Word.15.0.4797.1003\lib\net20\Microsoft.Office.Interop.Word.dll .\src\

del /S /Q .\download