FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["BDSAProject.Core/BDSAProject.Core.csproj", "BDSAProject.Core/"]
RUN dotnet restore "BDSAProject.Core/BDSAProject.Core.csproj"
COPY . .
WORKDIR "/src/BDSAProject.Core"
RUN dotnet build "BDSAProject.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BDSAProject.Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BDSAProject.Core.dll"]
