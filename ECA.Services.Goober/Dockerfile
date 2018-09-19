FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ECA.Services.Goober/ECA.Services.Goober.csproj", "ECA.Services.Goober/"]
COPY ["ECA.Services.Goober.DAL/ECA.Services.Goober.DAL.csproj", "ECA.Services.Goober.DAL/"]
RUN dotnet restore "ECA.Services.Goober/ECA.Services.Goober.csproj"
COPY . .
WORKDIR "/src/ECA.Services.Goober"
RUN dotnet build "ECA.Services.Goober.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ECA.Services.Goober.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eca.services.goober.dll"]