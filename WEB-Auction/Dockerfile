#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WEB-Auction/WEB-Auction.csproj", "WEB-Auction/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["BLL/BLL.csproj", "BLL/"]
RUN dotnet restore "WEB-Auction/WEB-Auction.csproj"
COPY . .
WORKDIR "/src/WEB-Auction"
RUN dotnet build "WEB-Auction.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WEB-Auction.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WEB-Auction.dll"]