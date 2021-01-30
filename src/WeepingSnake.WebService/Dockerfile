#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["WeepingSnake.WebService/WeepingSnake.WebService.csproj", "WeepingSnake.WebService/"]
RUN dotnet restore "WeepingSnake.WebService/WeepingSnake.WebService.csproj"
COPY . .
WORKDIR "/src/WeepingSnake.WebService"
RUN dotnet build "WeepingSnake.WebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeepingSnake.WebService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeepingSnake.WebService.dll"]