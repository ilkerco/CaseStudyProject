FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Campaign/Campaign.API/Campaign.API.csproj", "Campaign/Campaign.API/"]
COPY ["src/Campaign/Campaign.Infrastructure/Campaign.Infrastructure.csproj", "Campaign/Campaign.Infrastructure/"]
COPY ["src/Campaign/Campaign.Application/Campaign.Application.csproj", "Campaign/Campaign.Application/"]
COPY ["src/Campaign/Campaign.Domain/Campaign.Domain.csproj", "Campaign/Campaign.Domain/"]

RUN dotnet restore "Campaign/Campaign.API/Campaign.API.csproj"
COPY . .
WORKDIR "src/Campaign/Campaign.API"
RUN dotnet build "Campaign.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Campaign.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Campaign.API.dll"]