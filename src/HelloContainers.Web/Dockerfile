#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG APP_VERSION="0.0.1"
WORKDIR /src
COPY ["HelloContainers.Web/HelloContainers.Web.csproj", "HelloContainers.Web/"]
RUN dotnet restore "HelloContainers.Web/HelloContainers.Web.csproj"
COPY . .
WORKDIR "/src/HelloContainers.Web"
RUN dotnet build "HelloContainers.Web.csproj" -c Release -o /app/build /p:Version=$APP_VERSION

FROM build AS publish
ARG APP_VERSION="0.0.1"
RUN dotnet publish "HelloContainers.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:Version=$APP_VERSION

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloContainers.Web.dll"]
ENV HELLO_SECRET_PHRASE=blah
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT="Production"