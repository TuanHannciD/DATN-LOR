FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY AuthDemo.csproj ./
RUN dotnet restore AuthDemo.csproj

COPY . .
RUN dotnet publish AuthDemo.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "AuthDemo.dll"]