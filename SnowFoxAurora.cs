using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxAuroraMain : MelonMod
    {
        public static void SnowFoxAurora()
        {
            if (GameManager.GetAuroraManager().AuroraIsActive() && Settings.options.settingAuroraFox == true && GameManager.GetAuroraManager().GetNormalizedAlpha() >= 0.05f) // [Range(0.01f, 1f)] Aurora sensitivity
            {                 
                FoxVars.foxRendererAurora.enabled = true;
            }
            else
            {
                FoxVars.foxRendererAurora.enabled = false;
            }   
        }
    }
}