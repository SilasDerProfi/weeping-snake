FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app 

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WeepingSnake.Game/*.csproj ./WeepingSnake.Game/
COPY WeepingSnake.ConsoleClient/*.csproj ./WeepingSnake.ConsoleClient/

COPY WeepingSnake.WebService/*.csproj ./WeepingSnake.WebService/

COPY tests/WeepingSnake.ConsoleClient.Tests/*.csproj ./tests/WeepingSnake.ConsoleClient.Tests/
COPY tests/WeepingSnake.Game.Tests/*.csproj ./tests/WeepingSnake.Game.Tests/


RUN dotnet restore 

# copy everything else and build app
COPY WeepingSnake.Game/. ./WeepingSnake.Game/
COPY WeepingSnake.ConsoleClient/. ./WeepingSnake.ConsoleClient/


WORKDIR /app/WeepingSnake.ConsoleClient
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS runtime
WORKDIR /app 

COPY --from=build /app/WeepingSnake.ConsoleClient/out ./
ENTRYPOINT ["dotnet", "WeepingSnake.ConsoleClient.dll"]

# docker build -f WeepingSnake.Game/Dockerfile -t weeping_snake .