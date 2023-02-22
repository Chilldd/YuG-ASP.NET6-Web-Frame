#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["YuG.Framework.WebAPI/YuG.Framework.WebAPI.csproj", "YuG.Framework.WebAPI/"]
COPY ["YuG.Framework.Core/YuG.Framework.Core.csproj", "YuG.Framework.Core/"]
COPY ["YuG.Framework.Repository/YuG.Framework.Repository.csproj", "YuG.Framework.Repository/"]
COPY ["YuG.Framework.Authorization/YuG.Framework.Authorization.csproj", "YuG.Framework.Authorization/"]
COPY ["YuG.Framework.Service/YuG.Framework.Service.csproj", "YuG.Framework.Service/"]
RUN dotnet restore "YuG.Framework.WebAPI/YuG.Framework.WebAPI.csproj"
COPY . .
WORKDIR "/src/YuG.Framework.WebAPI"
RUN dotnet build "YuG.Framework.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YuG.Framework.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YuG.Framework.WebAPI.dll", "--urls","http://0.0.0.0"]