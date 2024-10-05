FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /TestSolution
COPY * ./
RUN dotnet build TestSolution.sln -o Output/
ENTRYPOINT ["./Output/TestRunner"]