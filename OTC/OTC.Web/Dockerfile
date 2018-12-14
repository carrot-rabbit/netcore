FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["OTC/OTC.csproj", "OTC/"]
RUN dotnet restore "OTC/OTC.csproj"
COPY . .
WORKDIR "/src/OTC"
RUN dotnet build "OTC.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OTC.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OTC.dll"]