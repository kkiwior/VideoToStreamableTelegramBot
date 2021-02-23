#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine3.13-amd64 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.13-amd64 AS build
WORKDIR /src
COPY ["VideoToStreamableTelegramBot.csproj", ""]
RUN dotnet restore "./VideoToStreamableTelegramBot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "VideoToStreamableTelegramBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VideoToStreamableTelegramBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /usr/local/bin/
COPY youtube-dl .
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideoToStreamableTelegramBot.dll"]

RUN apk add --no-cache python2
RUN apk add --no-cache ffmpeg
