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
        [HarmonyPatch(typeof(HUDManager), "UpdateCrosshair")]
        public class WeaponCrosshairUpdate
        {
            public static void Postfix(HUDManager __instance)
            {
                if (FoxVars.showCrosshair == true)
                {
                    //GearItem itemInHands = GameManager.GetPlayerManagerComponent().m_ItemInHands;

                    Utils.SetActive(InterfaceManager.m_Panel_HUD.m_Sprite_Crosshair.gameObject, true);
                    InterfaceManager.m_Panel_HUD.m_Sprite_Crosshair.alpha = 1f;
                }
            }
        }
    }
}