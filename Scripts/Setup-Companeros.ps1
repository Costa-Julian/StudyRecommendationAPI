# Setup inicial para compañeros - ejecutar UNA SOLA VEZ
# Click derecho -> "Ejecutar con PowerShell"

Write-Host "=== Setup StudyRecommendation API ===" -ForegroundColor Cyan
Write-Host ""

# Verificar Node.js
Write-Host "Verificando Node.js..." -ForegroundColor Yellow
if (-not (Get-Command node -ErrorAction SilentlyContinue)) {
    Write-Host "Node.js no esta instalado." -ForegroundColor Red
    Write-Host "Descargalo de https://nodejs.org (version LTS) e instala, luego volvé a ejecutar este script." -ForegroundColor Red
    Read-Host "Presioná Enter para cerrar"
    exit 1
}
Write-Host "Node.js OK: $(node --version)" -ForegroundColor Green

# Verificar/instalar Claude Code
Write-Host ""
Write-Host "Verificando Claude Code CLI..." -ForegroundColor Yellow
if (-not (Get-Command claude -ErrorAction SilentlyContinue)) {
    Write-Host "Instalando Claude Code CLI..." -ForegroundColor Yellow
    npm install -g @anthropic-ai/claude-code
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Error al instalar Claude Code. Intentá correr este script como Administrador." -ForegroundColor Red
        Read-Host "Presioná Enter para cerrar"
        exit 1
    }
}
Write-Host "Claude Code OK" -ForegroundColor Green

# Login en Claude Code
Write-Host ""
Write-Host "Iniciando sesion en Claude Code..." -ForegroundColor Yellow
Write-Host "Se va a abrir el navegador para que inicies sesion con tu cuenta de Anthropic." -ForegroundColor White
Write-Host ""
claude login

Write-Host ""
Write-Host "=== Setup completo ===" -ForegroundColor Green
Write-Host "Ya podés usar Iniciar.bat para arrancar la API." -ForegroundColor Green
Read-Host "Presioná Enter para cerrar"
