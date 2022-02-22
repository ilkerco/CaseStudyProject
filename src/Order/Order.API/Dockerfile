#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Order/Order.API/Order.API.csproj", "Order/Order.API/"]
COPY ["src/Order/Order.Infrastructure/Order.Infrastructure.csproj", "Order/Order.Infrastructure/"]
COPY ["src/Order/Order.Application/Order.Application.csproj", "Order/Order.Application/"]
COPY ["src/Order/Order.Domain/Order.Domain.csproj", "Order/Order.Domain/"]

RUN dotnet restore "Order/Order.API/Order.API.csproj"
COPY . .
WORKDIR "src/Order/Order.API"
RUN dotnet build "Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]