# Genera el ejecutable autocontenido para distribuir a compañeros
# Ejecutar desde la raiz del repositorio: .\Scripts\Publicar.ps1

$projectPath = ".\StudyRecommendationAPI\StudyRecommendationAPI.csproj"
$outputPath  = ".\Distribuible"

Write-Host "Limpiando carpeta anterior..." -ForegroundColor Cyan
if (Test-Path $outputPath) { Remove-Item $outputPath -Recurse -Force }

Write-Host "Compilando y publicando..." -ForegroundColor Cyan
dotnet publish $projectPath `
    --runtime win-x64 `
    --self-contained `
    --configuration Release `
    --output $outputPath `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error al compilar. Revisa los errores arriba." -ForegroundColor Red
    exit 1
}

# Copiar scripts de arranque
Copy-Item ".\Scripts\Iniciar.bat"        "$outputPath\Iniciar.bat"
Copy-Item ".\Scripts\Setup-Companeros.ps1" "$outputPath\Setup-Companeros.ps1"

# Crear el zip para compartir
$zipPath = ".\StudyRecommendationAPI-distribuible.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }
Compress-Archive -Path "$outputPath\*" -DestinationPath $zipPath

Write-Host ""
Write-Host "Listo! Compartí el archivo: $zipPath" -ForegroundColor Green
Write-Host "Tus compañeros solo necesitan descomprimir y seguir las instrucciones del README." -ForegroundColor Green
