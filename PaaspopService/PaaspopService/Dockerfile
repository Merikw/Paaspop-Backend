FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY PaaspopService/PaaspopService.WebApi.csproj PaaspopService/
COPY PaaspopService.Common/PaaspopService.Common.csproj PaaspopService.Common/
COPY PaaspopService.Domain/PaaspopService.Domain.csproj PaaspopService.Domain/
COPY PaaspopService.Persistence/PaaspopService.Persistence.csproj PaaspopService.Persistence/
COPY PaaspopService.Application/PaaspopService.Application.csproj PaaspopService.Application/
RUN dotnet restore PaaspopService/PaaspopService.WebApi.csproj
COPY . .
WORKDIR /src/PaaspopService
RUN dotnet build PaaspopService.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PaaspopService.WebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PaaspopService.WebApi.dll"]
