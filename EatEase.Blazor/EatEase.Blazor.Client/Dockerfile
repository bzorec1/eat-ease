FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EatEase.Blazor/EatEase.Blazor.Client/EatEase.Blazor.Client.csproj", "EatEase.Blazor/EatEase.Blazor.Client/"]
RUN dotnet restore "EatEase.Blazor/EatEase.Blazor.Client/EatEase.Blazor.Client.csproj"
COPY . .
WORKDIR "/src/EatEase.Blazor/EatEase.Blazor.Client"
RUN dotnet build "EatEase.Blazor.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EatEase.Blazor.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EatEase.Blazor.Client.dll"]
