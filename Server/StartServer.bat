SET ASPNETCORE_ENVIRONMENT=Development
SET LAUNCHER_PATH=bin\Debug\net5.0\Server.exe
iisexpress.exe /config:"..\.vs\Server\config\applicationhost.config" /site:"Server" /apppool:"Server AppPool"