#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Game.Forum.Api/Game.Forum.Api.csproj", "Game.Forum.Api/"]
COPY ["Game.Forum.Application/Game.Forum.Application.csproj", "Game.Forum.Application/"]
COPY ["Game.Forum.Domain/Game.Forum.Domain.csproj", "Game.Forum.Domain/"]
COPY ["Game.Forum.Utils/Game.Forum.Utils.csproj", "Game.Forum.Utils/"]
COPY ["Game.Forum.Infrastructure/Game.Forum.Infrastructure.csproj", "Game.Forum.Infrastructure/"]
COPY ["Game.Forum.Persistence/Game.Forum.Persistence.csproj", "Game.Forum.Persistence/"]
RUN dotnet restore "Game.Forum.Api/Game.Forum.Api.csproj"
COPY . .
WORKDIR "/src/Game.Forum.Api"
RUN dotnet build "Game.Forum.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Game.Forum.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Game.Forum.Api.dll"]