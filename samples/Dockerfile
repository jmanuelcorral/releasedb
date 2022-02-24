FROM mcr.microsoft.com/dotnet/sdk:6.0
RUN apt-get update
WORKDIR /
RUN dotnet tool install releasedb --version 1.0.1 --tool-path /bin
RUN releasedb -h
ENTRYPOINT ["releasedb", "-h"]