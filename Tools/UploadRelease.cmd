CodeplexReleaseUploader.exe /UserName=%CODEPLEX_USERNAME% /Password=%CODEPLEX_PASSWORD% /Project=oxyplot "/Release=OxyPlot %1" "/Description=Release assemblies and examples." "/Upload=..\Output\OxyPlot-Release-%1.zip,OxyPlot-Release-%1.zip" "/Upload=..\Output\OxyPlot-Examples-%1.zip,OxyPlot-Examples-%1.zip" > UploadRelease.log