using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;
using Harmony;

namespace FoxCompanion
{
    public class SnowFoxHarmonyMain : MelonMod
    {
        [HarmonyLib.HarmonyPatch(typeof(SaveGameSystem), "SaveGame")]
        public class SaveGameSystemSaveAddon
        {
            public static void Postfix()
            {
                Settings.options.Save();
            }
        }        
    }
}