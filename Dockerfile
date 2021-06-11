FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
EXPOSE 80
EXPOSE 443
COPY EF-DBFirst-Approach.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EF-DBFirst-Approach.dll"]