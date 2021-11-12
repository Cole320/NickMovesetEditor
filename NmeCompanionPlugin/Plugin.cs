using BepInEx;
using HarmonyLib;
using Nick;
using UnityEngine;

namespace NmeCompanionPlugin
{
    [BepInPlugin("com.coal.plugins.nme.companion", "NME Companion Plugin", "1.0.0")]
    [BepInProcess("Nickelodeon All-Star Brawl.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private static string _bepInExPath;
        private static string _textAssetPath;
        private void Awake()
        {
            new Harmony("com.coal.plugins.nme.companion").PatchAll();
            Logger.LogInfo("NMECompanionPlugin Initialized!");
            Logger.LogFatal("THIS GAME IS BETTER THAN SMASH! <3");

            InitAssetFolder();
        }
    
        private static void InitAssetFolder()
        {
            _bepInExPath = Paths.BepInExRootPath;
            _textAssetPath = System.IO.Path.Combine(_bepInExPath, "Movesets");
            if (!System.IO.Directory.Exists(_textAssetPath)) System.IO.Directory.CreateDirectory(_textAssetPath);
        }
        private static void WriteAsset(TextAsset textAsset)
        {
            var textAssetPath = System.IO.Path.Combine(_textAssetPath, textAsset.name + ".bsa");
            
            if (!System.IO.File.Exists(textAssetPath))
            {
                System.IO.File.WriteAllText(textAssetPath, textAsset.ToString());
            }
        }

        private static TextAsset ReadAsset(TextAsset textAsset)
        {
            var textAssetPath = System.IO.Path.Combine(_textAssetPath, textAsset.name + ".bsa");
            
            return System.IO.File.Exists(textAssetPath) ? new TextAsset(System.IO.File.ReadAllText(textAssetPath)) : textAsset;
        }

        // TODO: Auto-dump from character list instead of having to load each character manually
        [HarmonyPatch(typeof(GameParse), "ReadSerialMovesetPreloaded")]
        private class TextAssetPatch2
        {
            private static void Prefix(ref TextAsset[] movesetLayers)
            {
                for (var i = 0; i < movesetLayers.Length; i++)
                {
                    Debug.Log("Found TextAsset: " + movesetLayers[i].name);
                    Debug.Log(Paths.BepInExRootPath);
                    WriteAsset(movesetLayers[i]);
                    movesetLayers[i] = ReadAsset(movesetLayers[i]);
                }
            }
        }
    }
}
