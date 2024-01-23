#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Tasks.API/Tasks.API.csproj", "Tasks.API/"]
COPY ["Tasks.API.Infra.Data/Tasks.API.InfraData.csproj", "Tasks.API.Infra.Data/"]
COPY ["Tasks.API.Domain/Tasks.API.Domain.csproj", "Tasks.API.Domain/"]
COPY ["Tasks.API.Infra/Tasks.API.Application.csproj", "Tasks.API.Infra/"]
RUN dotnet restore "Tasks.API/Tasks.API.csproj"
COPY . .
WORKDIR "/src/Tasks.API"
RUN dotnet build "Tasks.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasks.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasks.API.dll"]