#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestProject.ZipPay.Api/TestProject.ZipPay.Api.csproj", "TestProject.ZipPay.Api/"]
COPY ["TestProject.Zip.Command/TestProject.ZipPay.Command.csproj", "TestProject.Zip.Command/"]
COPY ["TestProject.Zip.Common/TestProject.ZipPay.Common.csproj", "TestProject.Zip.Common/"]
COPY ["TestProject.Zip.Contract/TestProject.ZipPay.Contract.csproj", "TestProject.Zip.Contract/"]
COPY ["TestProject.Zip.Domain/TestProject.ZipPay.Domain.csproj", "TestProject.Zip.Domain/"]
COPY ["TestProject.Zip.Infrastructure/TestProject.ZipPay.Infrastructure.csproj", "TestProject.Zip.Infrastructure/"]
COPY ["TestProject.Zip.Query/TestProject.ZipPay.Query.csproj", "TestProject.Zip.Query/"]
RUN dotnet restore "TestProject.ZipPay.Api/TestProject.ZipPay.Api.csproj"
COPY . .
WORKDIR "/src/TestProject.ZipPay.Api"
RUN dotnet build "TestProject.ZipPay.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestProject.ZipPay.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestProject.ZipPay.Api.dll"]