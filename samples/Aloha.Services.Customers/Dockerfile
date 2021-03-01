FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim  AS build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY --from=build-env /app/out .

# Expose ports
EXPOSE 5100/tcp
ENV ASPNETCORE_URLS http://*:5100

# Start
ENTRYPOINT ["dotnet", "Aloha.Services.Customers.dll"]
