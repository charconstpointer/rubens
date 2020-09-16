FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:4444
EXPOSE 4444

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY [".", "app/"]
RUN dotnet restore "app/Rubens.Server.csproj"
RUN dotnet build "app/Rubens.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rubens.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "Rubens.Server.dll"]
