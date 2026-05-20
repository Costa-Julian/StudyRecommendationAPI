@echo off
title StudyRecommendation API
color 0A

echo =====================================
echo   StudyRecommendation API
echo =====================================
echo.
echo Iniciando servidor en http://localhost:5000
echo Para ver la documentacion abre: http://localhost:5000/scalar/v1
echo.
echo Presiona Ctrl+C para detener el servidor.
echo.

set ASPNETCORE_ENVIRONMENT=Production
set ASPNETCORE_URLS=http://localhost:5000

StudyRecommendationAPI.exe

pause
