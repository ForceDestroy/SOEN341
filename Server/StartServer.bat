SET ASPNETCORE_ENVIRONMENT=Development
SET LAUNCHER_PATH=bin\Debug\net5.0\Server.exe
del "C:\Users\\My Documents\IISExpress\config"
cd /d "C:\Program Files\IIS Express\"
iisexpress.exe /config:"C:\Users\travis\build\ForceDestroy\SOEN341\Server\applicationhost.config" /site:"Server" /apppool:"Server AppPool" /trace:error