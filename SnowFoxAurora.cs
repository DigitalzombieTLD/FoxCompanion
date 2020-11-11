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
            if (Settings.options.settingAuroraFox == 0)
            {
                if (GameManager.GetAuroraManager().AuroraIsActive() && GameManager.GetAuroraManager().GetNormalizedAlpha() >= 0.05f) // [Range(0.01f, 1f)] Aurora sensitivity
                {
                    FoxVars.foxRendererAurora.enabled = true;
                }
                else
                {
                    FoxVars.foxRendererAurora.enabled = false;
                }
            }
            else if (Settings.options.settingAuroraFox == 1 && FoxVars.foxRendererAurora.enabled != true)
            {
                FoxVars.foxRendererAurora.enabled = true;
            }
            else if (Settings.options.settingAuroraFox == 2 && FoxVars.foxRendererAurora.enabled != false)
            {
                FoxVars.foxRendererAurora.enabled = false;
            }
        }
    }
}