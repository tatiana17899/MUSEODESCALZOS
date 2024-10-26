FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore
    
# Copiar todo lo demás y construir
COPY . ./
RUN dotnet publish -c Release -o out

# Construir imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Instalar wkhtmltopdf y sus dependencias
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    wkhtmltopdf \
    libgdiplus \
    libc6-dev \
    libx11-dev \
    fontconfig \
    libxext6 \
    libxrender1 \
    xfonts-75dpi \
    xfonts-base \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Crear directorio para wkhtmltopdf
RUN mkdir -p /app/Rotativa/Linux

# Crear enlaces simbólicos a los binarios de wkhtmltopdf
RUN ln -s /usr/bin/wkhtmltopdf /app/Rotativa/Linux/wkhtmltopdf \
    && ln -s /usr/bin/wkhtmltoimage /app/Rotativa/Linux/wkhtmltoimage

# Copiar archivos de la aplicación
COPY --from=build-env /app/out .

# Configurar variables de entorno
ENV APP_NET_CORE="MUSEODESCALZOS.dll"
ENV PORT=80

# Usar combinación de ENTRYPOINT y CMD para mayor claridad
ENTRYPOINT ["dotnet"]
CMD ["MUSEODESCALZOS.dll"]