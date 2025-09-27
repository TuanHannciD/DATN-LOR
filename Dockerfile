# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj và restore
COPY AuthDemo.csproj ./
RUN dotnet restore AuthDemo.csproj

# Copy toàn bộ source và build
COPY . .
RUN dotnet publish AuthDemo.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AuthDemo.dll"]

