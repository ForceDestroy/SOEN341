SET ASPNETCORE_ENVIRONMENT=Development
SET LAUNCHER_PATH=bin\Debug\net5.0\Server.exe
dir
del "C:\Users\\My Documents\IISExpress\config"
cd /d "C:\Program Files\IIS Express\"
iisexpress.exe /site:"Server" /apppool:"Server AppPool" /trace:error