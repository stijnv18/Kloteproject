# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8888

# Use the official ASP.NET Core SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["/NoteSaverAPI.csproj", "./"]
RUN dotnet restore "./NoteSaverAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NoteSaverAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NoteSaverAPI.csproj" -c Release -o /app/publish

# Copy the build app to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy the certificate
COPY NoteSaverAPI.pfx /root/.aspnet/https/NoteSaverAPI.pfx
ENTRYPOINT ["dotnet", "NoteSaverAPI.dll"]