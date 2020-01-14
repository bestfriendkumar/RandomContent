#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
#WORKDIR /src
#COPY ["RandomContent/RandomContent.csproj", "RandomContent/"]
#RUN dotnet restore "RandomContent/RandomContent.csproj"
#COPY . .
#WORKDIR "/src/RandomContent"
#RUN dotnet build "RandomContent.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "RandomContent.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
##ENTRYPOINT ["dotnet", "RandomContent.dll"]
#CMD dotnet RandomContent.dll #needed for Heroku support

#FROM microsoft/dotnet:2.1-aspnetcore-runtime
#WORKDIR /app
#COPY . .
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet RandomContent.dll

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS builder
WORKDIR /sources

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish --output /app/ --configuration Release

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
COPY --from=builder /app .
CMD ["dotnet", "RandomContent.dll"]