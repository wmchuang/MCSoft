#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MCSoft/MCSoft.csproj", "MCSoft/"]
COPY ["MCSoft.Application/MCSoft.Application.csproj", "MCSoft.Application/"]
COPY ["MCSoft.Domain/MCSoft.Domain.csproj", "MCSoft.Domain/"]
COPY ["MCSoft.Utility/MCSoft.Utility.csproj", "MCSoft.Utility/"]
COPY ["MCSoft.Infrastructure/MCSoft.Infrastructure.csproj", "MCSoft.Infrastructure/"]
RUN dotnet restore "MCSoft/MCSoft.csproj"
COPY . .
WORKDIR "/src/MCSoft"
RUN dotnet build "MCSoft.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MCSoft.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MCSoft.dll"]