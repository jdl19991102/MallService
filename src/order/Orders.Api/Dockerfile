#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Orders.Api/Orders.Api.csproj", "Orders.Api/"]
COPY ["Orders.Application/Orders.Application.csproj", "Orders.Application/"]
COPY ["Orders.Application.Contracts/Orders.Application.Contracts.csproj", "Orders.Application.Contracts/"]
COPY ["Orders.Infra.Utils/Orders.Infra.Utils.csproj", "Orders.Infra.Utils/"]
COPY ["Orders.Domain/Orders.Domain.csproj", "Orders.Domain/"]
COPY ["Orders.Infra.Ioc/Orders.Infra.IoC.csproj", "Orders.Infra.Ioc/"]
RUN dotnet restore "Orders.Api/Orders.Api.csproj"
COPY . .
WORKDIR "/src/Orders.Api"
RUN dotnet build "Orders.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orders.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orders.Api.dll"]