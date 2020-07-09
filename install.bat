del /S /Q .\download
mkdir download

nuget.exe install LibGit2Sharp -Version 0.27.0-preview-0034 -o .\download

del .\src\PdfGenerator\costura64\*.dll
move .\download\LibGit2Sharp.NativeBinaries.2.0.298\runtimes\win-x64\native\*.dll .\src\PdfGenerator\costura64\

del .\src\PdfGenerator\costura32\*.dll
move .\download\LibGit2Sharp.NativeBinaries.2.0.298\runtimes\win-x86\native\*.dll .\src\PdfGenerator\costura32\

del /S /Q .\download