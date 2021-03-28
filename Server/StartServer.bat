SET ASPNETCORE_ENVIRONMENT=Development
SET LAUNCHER_PATH=bin\Debug\net5.0\Server.exe
cd /d "C:\Program Files\IIS Express\"
iisexpress.exe /config:"C:\Users\travis\build\ForceDestroy\SOEN341\Server\.vs\Server\config\applicationhost.config" /site:"Server" /apppool:"Server AppPool" /trace:error