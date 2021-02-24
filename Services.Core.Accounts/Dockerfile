FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster AS build
WORKDIR /src

COPY src/Services/Services.Core.Accounts/Services.Core.Accounts.csproj src/Services/Services.Core.Accounts/
RUN dotnet restore "src/Services/Services.Core.Accounts/Services.Core.Accounts.csproj"

COPY . .

WORKDIR /src/src/Services/Services.Core.Accounts
RUN dotnet publish -c Release -o /app

FROM build as publish

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Services.Core.Accounts.dll"]

