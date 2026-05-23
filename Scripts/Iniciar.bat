@echo off
title StudyRecommendation API
color 0A

echo.
echo  =====================================================
echo   StudyRecommendation API
echo  =====================================================
echo.
echo  Verificando requisitos antes de iniciar...
echo.

:: ---- Claude Code CLI ----
where claude 2>nul
if errorlevel 1 (
    color 0E
    echo  [X] Claude Code CLI no encontrado.
    echo      La busqueda de recursos no va a funcionar.
    echo      Para instalarlo, ejecuta Setup-Companeros.ps1
    echo.
    color 0A
) else (
    echo  [OK] Claude Code CLI encontrado.
    echo.
)

:: ---- Detectar IP local ----
set LOCAL_IP=
for /f "tokens=2 delims=:" %%A in ('ipconfig ^| findstr /i "IPv4" ^| findstr /v "127.0.0.1" ^| findstr /v "169.254"') do (
    for /f "tokens=1" %%B in ("%%A") do (
        if not defined LOCAL_IP set LOCAL_IP=%%B
    )
)

echo  =====================================================
echo   La API esta lista en:
echo  =====================================================
echo.
echo   Esta computadora:   http://localhost:5000
if defined LOCAL_IP (
    echo   Red local:          http://%LOCAL_IP%:5000
)
echo.
echo   Probar endpoints:   http://localhost:5000/scalar/v1
echo.
echo  =====================================================
echo   Presiona Ctrl+C para detener la API.
echo  =====================================================
echo.

set ASPNETCORE_ENVIRONMENT=Production
StudyRecommendationAPI.exe

echo.
echo  La API se detuvo.
pause
