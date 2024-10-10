FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN apt-get install curl
RUN curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh -o ./brew_install.sh
RUN chmod -x ./brew_install.sh
RUN ./brew_install.sh
RUN brew install gauge