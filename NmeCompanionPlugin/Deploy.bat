@REM LIGMA
@ECHO OFF
TASKKILL /f /im "Nickelodeon All-Star Brawl.exe"
SET MOD_DLL_NAME=NMECompanionPlugin.dll
SET CURRENT_PATH=C:\Users\Coal\Desktop\Coding\C#\NickMovesetEditor\NMECompanionPlugin\bin\Debug\netstandard2.0\
SET MOD_DLL_PATH=%CURRENT_PATH%%MOD_DLL_NAME%
DEL "D:\Games\Steam\steamapps\common\Nickelodeon All-Star Brawl\BepInEx\plugins\Coal-NMECompanion\NMECompanionPlugin.dll" >NUL
echo %MOD_DLL_PATH%
COPY %MOD_DLL_PATH% "D:\Games\Steam\steamapps\common\Nickelodeon All-Star Brawl\BepInEx\plugins\Coal-NMECompanion\NMECompanionPlugin.dll" >NUL
ECHO Successfully Deployed Mod DLL - Launching Nickelodeon All-Star Brawl
"D:\Games\Steam\steamapps\common\Nickelodeon All-Star Brawl\Nickelodeon All-Star Brawl.exe"