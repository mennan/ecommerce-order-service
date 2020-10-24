FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS builder
WORKDIR /source

COPY . .

# Change the Directory
WORKDIR /source/

# aspnet-core
RUN dotnet restore src/ECommerce.Api/ECommerce.Api.csproj
RUN dotnet publish src/ECommerce.Api/ECommerce.Api.csproj --output /ecommerce/ --configuration Release

## Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic

# Change the Directory
WORKDIR /ecommerve

COPY --from=builder /ecommerce .
ENTRYPOINT ["dotnet", "ECommerce.Api.dll"]