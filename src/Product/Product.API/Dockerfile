#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Product/Product.API/Product.API.csproj", "Product/Product.API/"]
COPY ["src/Product/Product.Infrastructure/Product.Infrastructure.csproj", "Product/Product.Infrastructure/"]
COPY ["src/Product/Product.Application/Product.Application.csproj", "Product/Product.Application/"]
COPY ["src/Product/Product.Domain/Product.Domain.csproj", "Product/Product.Domain/"]

RUN dotnet restore "Product/Product.API/Product.API.csproj"
COPY . .
WORKDIR "src/Product/Product.API"
RUN dotnet build "Product.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.API.dll"]