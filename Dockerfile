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

# Deployment Stage - Switch to Debian Bookworm Slim
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim@sha256:d4d80bf500f4c8307e5c44bf61eb58aec027da07c4d1c40816846fe5eef3f34d

# 1. Install Essential libraries for Debian
# No need for gcompat or libc6-compat here as glibc is native
RUN apt-get update && apt-get install -y \
    curl \
    tini \
    && rm -rf /var/lib/apt/lists/*

# 2. Install Lightpanda
# We download the nightly x86_64 binary directly
RUN curl -L -o /usr/bin/lightpanda https://github.com/lightpanda-io/browser/releases/download/nightly/lightpanda-x86_64-linux && \
    chmod +x /usr/bin/lightpanda

# 3. Setup Application directory
COPY --from=build-env /BCV-WSCRAP-API/out .

# 4. Networking & Environment Config
# We keep the CDP port (9222) internal to the container for security
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000 \
    PUPPETEER_SKIP_CHROMIUM_DOWNLOAD=true \
    LIGHTPANDA_CDP_URL=ws://127.0.0.1:9222 \
    DOTNET_RUNNING_IN_CONTAINER=true

# 5. Use tini to manage processes
# We bind lightpanda to 127.0.0.1 so it's only accessible from your .NET API
ENTRYPOINT ["tini", "--", "sh", "-c", "lightpanda serve --host 127.0.0.1 --port 9222 & dotnet BCV-WSCRAP-API.dll"]