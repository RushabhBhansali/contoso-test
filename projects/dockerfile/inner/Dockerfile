FROM microsoft/dotnet:2.2-sdk AS installer-env

ARG NBGV_CloudBuildNumber
ARG NBGV_AssemblyVersion
ARG NBGV_AssemblyFileVersion
ARG NBGV_AssemblyInformationalVersion

ENV NBGV_CloudBuildNumber=$NBGV_CloudBuildNumber
ENV NBGV_AssemblyVersion=$NBGV_AssemblyVersion
ENV NBGV_AssemblyFileVersion=$NBGV_AssemblyFileVersion
ENV NBGV_AssemblyInformationalVersion=$NBGV_AssemblyInformationalVersion

COPY src/ComponentDataService.Functions/ComponentDataService.Functions.csproj src/ComponentDataService.Functions/ComponentDataService.Functions.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY src/CratesClient/CratesClient.csproj src/CratesClient/CratesClient.csproj
COPY src/NpmClient/NpmClient.csproj src/NpmClient/NpmClient.csproj
COPY src/NugetClient/NugetClient.csproj src/NugetClient/NugetClient.csproj
COPY src/NugetSearchClient/NugetSearchClient.csproj src/NugetSearchClient/NugetSearchClient.csproj
COPY src/Services/Services.csproj src/Services/Services.csproj
COPY src/InternalStoreClient/InternalStoreClient.csproj src/InternalStoreClient/InternalStoreClient.csproj
COPY src/NpmAuditClient/NpmAuditClient.csproj src/NpmAuditClient/NpmAuditClient.csproj
COPY src/RubyGemsClient/RubyGemsClient.csproj src/RubyGemsClient/RubyGemsClient.csproj
COPY src/PyPiClient/PyPiClient.csproj src/PyPiClient/PyPiClient.csproj
COPY src/GithubClient/GithubClient.csproj src/GithubClient/GithubClient.csproj
COPY src/WhiteSourceClient/WhiteSourceClient.csproj src/WhiteSourceClient/WhiteSourceClient.csproj
COPY src/TwcClient/TwcClient.csproj src/TwcClient/TwcClient.csproj
COPY src/MavenSolrClient/MavenSolrClient.csproj src/MavenSolrClient/MavenSolrClient.csproj
COPY src/MavenClient/MavenClient.csproj src/MavenClient/MavenClient.csproj
COPY src/SecurityPolicy/SecurityPolicy.csproj src/SecurityPolicy/SecurityPolicy.csproj
COPY src/dirs.proj src/dirs.proj
COPY global.json src/global.json
COPY Directory.Build.props /src/Directory.Build.props
COPY NuGet.config NuGet.config
RUN dotnet restore /src/dirs.proj

COPY src/ src/

RUN dotnet build --no-restore /src/dirs.proj

RUN cd /src/ComponentDataService.Functions/ && \
    mkdir -p /home/site/wwwroot && \
    dotnet publish *.csproj --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:2.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true \
    NpmRegistryClientSettings__NpmRegistryBaseUri=https://registry.npmjs.org/ \
    OpenApi__Info__Version=2.0.0 \
    OpenApi__Info__Title="Component Data Service OpenAPI" \
    OpenApi__Info__TermsOfService=https://github.com/aliencube/AzureFunctions.Extensions \
    OpenApi__Info__Contact__Name="Component Governance" \
    OpenApi__Info__Contact__Url=https://github.com/aliencube/AzureFunctions.Extensions/issues

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]