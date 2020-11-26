FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env

RUN mkdir -p /usr/share/man/man1 /usr/share/man/man2

RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    openjdk-11-jre

RUN dotnet tool install --global dotnet-sonarscanner
ENV PATH="${PATH}:/root/.dotnet/tools"
ENV JAVA_TOOL_OPTIONS -Dfile.encoding=UTF8

COPY . ./WebAPIClient
WORKDIR /WebAPIClient

RUN dotnet restore

RUN dotnet test \
    /p:CollectCoverage=true \
    /p:CoverletOutputFormat=opencover

RUN dotnet sonarscanner begin /k:"WebAPIClient" \
    /d:sonar.host.url="http://192.168.15.106:9000" \
    /d:sonar.verbose=true \
    /v:1.0.0 \
    /d:sonar.login="b9dbcbf66a141c0bf4a7d63af1a8b419a98997db" \
    /d:sonar.cs.opencover.reportsPaths="./WebAPI.Tests/coverage.opencover.xml"

RUN dotnet build
RUN dotnet sonarscanner end /d:sonar.login="b9dbcbf66a141c0bf4a7d63af1a8b419a98997db"