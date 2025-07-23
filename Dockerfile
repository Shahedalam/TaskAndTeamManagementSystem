# Base image with .NET 9 runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image with SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy and restore dependencies
COPY ["TaskAndTeamManagementSystem.csproj", "./"]
RUN dotnet restore "./TaskAndTeamManagementSystem.csproj"

# Copy all files and build
COPY . .
RUN dotnet build "./TaskAndTeamManagementSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish project
FROM build AS publish
RUN dotnet publish "./TaskAndTeamManagementSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TaskAndTeamManagementSystem.dll"]
