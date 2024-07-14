
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TicketsHandlingTask/TicketsHandling.csproj", "TicketsHandlingTask/"]
COPY ["TaskHandling.Infrastructure/TicketsHandling.Persistence.csproj", "TaskHandling.Infrastructure/"]
COPY ["TaskHandlingTask.Application/TicketsHandling.Application.csproj", "TaskHandlingTask.Application/"]
COPY ["TaskHandlingTask.Data/TicketsHandling.Domain.csproj", "TaskHandlingTask.Data/"]
RUN dotnet restore "./TicketsHandlingTask/TicketsHandling.csproj"
COPY . .
WORKDIR "/src/TicketsHandlingTask"
RUN dotnet build "./TicketsHandling.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TicketsHandling.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketsHandling.dll"]