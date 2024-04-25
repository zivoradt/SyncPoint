FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SyncPointBack.csproj", "."]
RUN dotnet restore "SyncPointBack.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SyncPointBack.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SyncPointBack.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "SyncPointBack.dll"]
