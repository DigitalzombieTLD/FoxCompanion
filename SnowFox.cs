using System;
using MelonLoader;
using Harmony;
using UnityEngine;
using System.Reflection;
using System.Xml.XPath;
using System.Globalization;
using UnhollowerRuntimeLib;
using ModSettings;

namespace FoxCompanion
{
    public class SnowFoxMain : MelonMod
    {        
        public override void OnApplicationStart()
        {
            // load fox asset
            FoxVars.foxload = AssetBundle.LoadFromFile("Mods\\snowfox.unity3d");

            if (FoxVars.foxload == null)
            {
                MelonLogger.Log("Failed to load AssetBundle!");
                return;
            }
           
            MelonLogger.Log("Snow fox asset succesfully loaded");

            // Settings menu
            FoxCompanion.Settings.OnLoad();
        }

        public override void OnLevelWasLoaded(int level)
        {
            SnowFoxInstanceMain.SnowFoxInstanceLoad(level);
        }

        public override void OnUpdate()
        {         
            SnowFoxInstanceMain.SnowFoxInstanceUpdate(); // One time teleport to player and set everything active

            // Do ALL THE STUFF
            if (FoxVars.foxactive == true && FoxVars.foxSpawned == true)
            {
                FoxVars.idleRand = UnityEngine.Random.Range(1, 6); // make some randoms for changing through idle animations

                // Update player, target and fox transforms               
                FoxVars.playerTransform = GameManager.GetPlayerTransform();
                FoxVars.foxTransform = FoxVars.fox.transform;                

                SnowFoxInstanceMain.SnowFoxInstanceUpdate(); 
                SnowFoxInputsMain.SnowFoxInputsUpdate();
                SnowFoxManageMovementMain.SnowFoxManageMovement();
                SnowFoxAuroraMain.SnowFoxAurora();        
                

            }
        } 
    }
}