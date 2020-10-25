#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LaunchpadSept2020.Auth/LaunchpadSept2020.Auth.csproj", "LaunchpadSept2020.Auth/"]
RUN dotnet restore "LaunchpadSept2020.Auth/LaunchpadSept2020.Auth.csproj"
COPY . .
WORKDIR "/src/LaunchpadSept2020.Auth"
RUN dotnet build "LaunchpadSept2020.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LaunchpadSept2020.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LaunchpadSept2020.Auth.dll"]