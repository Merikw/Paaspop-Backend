FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["./PaaspopService/PaaspopService/PaaspopService.WebApi.csproj", "PaaspopService/"]
COPY ["./PaaspopService/PaaspopService.Common/PaaspopService.Common.csproj", "PaaspopService.Common/"]
COPY ["./PaaspopService/PaaspopService.Domain/PaaspopService.Domain.csproj", "PaaspopService.Domain/"]
COPY ["./PaaspopService/PaaspopService.Persistence/PaaspopService.Persistence.csproj", "PaaspopService.Persistence/"]
COPY ["./PaaspopService/PaaspopService.Application/PaaspopService.Application.csproj", "PaaspopService.Application/"]
RUN dotnet restore "./PaaspopService/PaaspopService.WebApi.csproj"
COPY . .
WORKDIR "/src/PaaspopService/PaaspopService"
RUN dotnet build "PaaspopService.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PaaspopService.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PaaspopService.WebApi.dll", "--urls", "http://*:8080;http://*:8081" ]
