#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebNotesApi/WebNotesApi.csproj", "WebNotesApi/"]
RUN dotnet restore "WebNotesApi/WebNotesApi.csproj"
COPY . .
WORKDIR "/src/WebNotesApi"
RUN dotnet build "WebNotesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebNotesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebNotesApi.dll"]

# # syntax=docker/dockerfile:1
# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
# WORKDIR /app
    
# # Copy csproj and restore as distinct layers
# COPY *.csproj ./
# RUN dotnet restore
    
# # Copy everything else and build
# COPY ../engine/examples ./
# RUN dotnet publish -c Release -o out
    
# # Build runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app
# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "WebNotesApi.dll"]