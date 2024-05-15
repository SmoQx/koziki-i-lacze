@echo off

git add Assets GameProject.sln ignore.conf Logs Packages ProjectSettings UserSettings
git status

set /p choice="Wyslac zmiany githuba (git push origin)? (t/n): "

if /i "%choice%"=="t" (
    git commit
    git push origin
) else (
    git commit
)