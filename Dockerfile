# Build Stage

# Step 1: Use the 'microsoft dotnet sdk 8.0' official image
FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build-env

# Step 2: Change our working directory to the root of the API
WORKDIR /BCV-WSCRAP-API

# Step 3: Copy everything
COPY . ./

# Step 4: Restore packages
RUN dotnet restore

# Step 5: Build and publish a release
RUN dotnet publish -c release --property:PublishDir=../out

# Deployment Stage

# Step 1: Use the 'microsoft asptnet 8.0' official image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff

# Step 2: Add relevant packages to puppeteer to run
RUN apt-get update && \
    apt-get install -y \
        wget \
        gnupg2 \
        apt-transport-https \
        ca-certificates \
        fonts-liberation \
        libappindicator3-1 \
        libasound2 \
        libatk-bridge2.0-0 \
        libatk1.0-0 \
        libcups2 \
        libdbus-1-3 \
        libgdk-pixbuf2.0-0 \
        libnspr4 \
        libnss3 \
        libx11-xcb1 \
        libxcomposite1 \
        libxdamage1 \
        libxrandr2 \
        xdg-utils \
        libgbm1 \
        libxcb-dri3-0 \
        libxss1 && \
    rm -rf /var/lib/apt/lists/*

# Step 3: Change our working directory to the root of the API
WORKDIR /BCV-WSCRAP-API

# Step 4: Copy from build
COPY --from=build-env /BCV-WSCRAP-API/out .

# Step 5: Expose Port 5000
EXPOSE 5000

# Step 6: Add Env Variables
ENV ASPNETCORE_HTTP_PORT=https://+:5001
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Testing

# Step 7: Define Entrypoint
ENTRYPOINT ["dotnet", "BCV-WSCRAP-API.dll"]
