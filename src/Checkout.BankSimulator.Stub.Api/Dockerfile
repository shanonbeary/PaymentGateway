FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App
COPY . .
# Restore as distinct layers
RUN dotnet restore "src/Checkout.BankSimulator.Stub.Api/Checkout.BankSimulator.Stub.Api.csproj"
# Build and publish a release
RUN dotnet publish "src/Checkout.BankSimulator.Stub.Api/Checkout.BankSimulator.Stub.Api.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Checkout.BankSimulator.Stub.Api.dll"]