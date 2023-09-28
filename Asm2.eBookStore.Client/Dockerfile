FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Asm2.eBookStore.Client/Asm2.eBookStore.Client.csproj", "Asm2.eBookStore.Client/"]
RUN dotnet restore "Asm2.eBookStore.Client/Asm2.eBookStore.Client.csproj"
COPY . .
WORKDIR "/src/Asm2.eBookStore.Client"
RUN dotnet build "Asm2.eBookStore.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Asm2.eBookStore.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Asm2.eBookStore.Client.dll"]
