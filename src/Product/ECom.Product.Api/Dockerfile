#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Product/ECom.Product.Api/ECom.Product.Api.csproj", "src/Product/ECom.Product.Api/"]
RUN dotnet restore "src/Product/ECom.Product.Api/ECom.Product.Api.csproj"
COPY . .
WORKDIR "/src/src/Product/ECom.Product.Api"
RUN dotnet build "ECom.Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECom.Product.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECom.Product.Api.dll"]
