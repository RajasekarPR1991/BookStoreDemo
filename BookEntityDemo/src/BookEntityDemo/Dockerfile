#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/mssql/server AS build

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/BookEntityDemo/BookEntityDemo.csproj", "src/BookEntityDemo/"]
COPY ["src/BookEntityDemo.Data/BookEntityDemo.Data.csproj", "src/BookEntityDemo.Data/"]
COPY ["src/BookEntityDemo.Models/BookEntityDemo.Models.csproj", "src/BookEntityDemo.Models/"]
COPY ["src/BookEntityDemo.Business/BookEntityDemo.Business.csproj", "src/BookEntityDemo.Business/"]
COPY ["src/BookEntityDemo.Repository/BookEntityDemo.Repository.csproj", "src/BookEntityDemo.Repository/"]
RUN dotnet restore "src/BookEntityDemo/BookEntityDemo.csproj"
COPY . .
WORKDIR "/src/src/BookEntityDemo"
RUN dotnet build "BookEntityDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookEntityDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookEntityDemo.dll"]

RUN chmod +x ./entrypoint.sh

CMD /bin/bash ./entrypoint.sh