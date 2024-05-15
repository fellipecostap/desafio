#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Estagio 1
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Estagio 2
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
#COPY ["Desafio.WebApi/Desafio.WebApi.csproj", "Desafio.WebApi/"]
#COPY ["Desafio.Application/Desafio.Application.csproj", "Desafio.Application/"]
#COPY ["Desafio.Domain/Desafio.Domain.csproj", "Desafio.Domain/"]
#COPY ["Desafio.Infrastructure/Desafio.Infrastructure.csproj", "Desafio.Infrastructure"]
COPY . .
WORKDIR "/src/Desafio.WebApi"
RUN dotnet build "Desafio.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Desafio.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Desafio.WebApi.dll"]