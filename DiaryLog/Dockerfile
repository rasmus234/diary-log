FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY DiaryLogApi/bin/Debug/net6.0/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "DiaryLogApi.dll"]