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
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine@sha256:354c2fcfb3c23abc60d98f0380cdb403fba844a62a123b6343a8c9611209995c

# Step 2: Add relevant packages to puppeteer to run
RUN apk add --no-cache \
    udev \
    ttf-freefont \
    chromium \
    nss \
    freetype \
    harfbuzz \
    ca-certificates \
    nodejs \
    npm

# Tell Puppeteer to use the system Chromium
ENV PUPPETEER_EXECUTABLE_PATH=/usr/bin/chromium-browser \
    PUPPETEER_SKIP_CHROMIUM_DOWNLOAD=true

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
