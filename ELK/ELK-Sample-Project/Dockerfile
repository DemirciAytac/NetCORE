#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ELK/ELK-Sample-Project/ELK-Sample-Project.csproj", "ELK/ELK-Sample-Project/"]
RUN dotnet restore "ELK/ELK-Sample-Project/ELK-Sample-Project.csproj"
COPY . .
WORKDIR "/src/ELK/ELK-Sample-Project"
RUN dotnet build "ELK-Sample-Project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ELK-Sample-Project.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ELK-Sample-Project.dll"]