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
        [HarmonyPatch(typeof(SaveGameSystem), "SaveGame")]
        public class SaveGameSystemSaveAddon
        {
            public static void Postfix()
            {
                Settings.options.Save();
            }
        }

        [HarmonyPatch(typeof(Hunger), "RemoveReserveCalories")]
        public class RemoveReserveCaloriesAddon
        {
            public static void Prefix(ref float calories)
            {
                if(Settings.options.foxCalories>=0)
                {
                    Settings.options.foxCalories = Settings.options.foxCalories - calories;
                }
                else
                {
                    Settings.options.foxCalories = 0;
                }
            }
        }
    }
}