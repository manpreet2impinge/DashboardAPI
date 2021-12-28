FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./

# Restore Packages
RUN dotnet restore --no-cache --force --source https://api.nuget.org/v3/index.json\
    --source https://www.myget.org/F/justlogin/auth/34376c58-48b4-4960-ba32-1fcfff4d2b3b/api/v3/index.json

# Switch to API directory
WORKDIR /app/DashboardAPI

# Publish to Release 
RUN dotnet publish -c Release -o dist -v q

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/DashboardAPI/dist .
COPY start-container.sh .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV BucketPath=''
ENV BasePath=dashboard

RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf

RUN apt-get update\
    && apt-get -y install awscli\
    && apt-get -y install libgdiplus\
    && apt-get install -y tzdata\
    && chmod +x start-container.sh

ENV TZ=Asia/Singapore
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

ENTRYPOINT ["./start-container.sh", "DashboardAPI.dll"]
