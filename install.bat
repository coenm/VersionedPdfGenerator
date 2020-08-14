del /S /Q .\download
mkdir download

nuget.exe install Microsoft.Office.Interop.Word -Version 15.0.4797.1003 -o .\download
move .\download\Microsoft.Office.Interop.Word.15.0.4797.1003\lib\net20\Microsoft.Office.Interop.Word.dll .\src\PdfGenerator.WordInterop\lib\

del /S /Q .\download