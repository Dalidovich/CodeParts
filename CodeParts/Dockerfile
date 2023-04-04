#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["CodeParts/CodeParts.csproj", "CodeParts/"]
COPY ["CodeParts.Data/CodeParts.Data.csproj", "CodeParts.Data/"]
COPY ["CodeParts.Service/CodeParts.Service.csproj", "CodeParts.Service/"]
RUN dotnet restore "CodeParts/CodeParts.csproj"
RUN dotnet dev-certs https --trust

COPY . .
WORKDIR "/src/CodeParts"
RUN dotnet build "CodeParts.csproj" -c Release -o /app/build
RUN dotnet dev-certs https --trust

FROM build AS publish
RUN dotnet publish "CodeParts.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet dev-certs https --trust

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
ENTRYPOINT ["dotnet", "CodeParts.dll"]