#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CUSTOMER.ANALYSIS.UI.SITE.WEB/CUSTOMER.ANALYSIS.UI.SITE.WEB.csproj", "CUSTOMER.ANALYSIS.UI.SITE.WEB/"]
RUN dotnet restore "CUSTOMER.ANALYSIS.UI.SITE.WEB/CUSTOMER.ANALYSIS.UI.SITE.WEB.csproj"
COPY . .
WORKDIR "/src/CUSTOMER.ANALYSIS.UI.SITE.WEB"
RUN dotnet build "CUSTOMER.ANALYSIS.UI.SITE.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CUSTOMER.ANALYSIS.UI.SITE.WEB.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CUSTOMER.ANALYSIS.UI.SITE.WEB.dll"]