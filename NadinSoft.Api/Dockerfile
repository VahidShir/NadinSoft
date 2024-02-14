#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NadinSoft.Api/NadinSoft.Api.csproj", "NadinSoft.Api/"]
COPY ["Infrastructures/NadinSoft.EntityFrameworkCore/NadinSoft.EntityFrameworkCore.csproj", "Infrastructures/NadinSoft.EntityFrameworkCore/"]
COPY ["NadinSoft.Domain/NadinSoft.Domain.csproj", "NadinSoft.Domain/"]
COPY ["NadinSoft.Domain.Shared/NadinSoft.Domain.Shared.csproj", "NadinSoft.Domain.Shared/"]
COPY ["NadinSoft.Application/NadinSoft.Application.csproj", "NadinSoft.Application/"]
COPY ["NadinSoft.Application.Contracts/NadinSoft.Application.Contracts.csproj", "NadinSoft.Application.Contracts/"]
RUN dotnet restore "./NadinSoft.Api/./NadinSoft.Api.csproj"
COPY . .
WORKDIR "/src/NadinSoft.Api"
RUN dotnet build "./NadinSoft.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NadinSoft.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NadinSoft.Api.dll"]