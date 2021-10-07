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
			// Pathfinding components
			ClassInjector.RegisterTypeInIl2Cpp<CorvoPathFinder>();
			ClassInjector.RegisterTypeInIl2Cpp<UnitPathfinder>();
			

			// load fox asset
			FoxVars.foxload = AssetBundle.LoadFromFile("Mods\\snowfox.unity3d");
    

            if (FoxVars.foxload == null)
            {
                MelonLogger.Msg("Failed to load AssetBundle (Fox)!");
                return;
            }


            MelonLogger.Msg("Snow fox asset succesfully loaded");

            

            // Settings menu
            FoxCompanion.Settings.OnLoad();
        }

        public override void OnSceneWasInitialized(int level, string sceneName)
        {
            FoxVars.loadedScene = level;
            //MelonLogger.Msg("Level initialized: " + level);
       
            if (level >= 4 && FoxVars.fox == null)
            {               
                SnowFoxInstanceMain.SnowFoxInstanceLoad();
                
                FoxVars.timeToSpawnStarted = false;
                //MelonLogger.Msg("After load and teleport");       
                SnowFoxTeleportFoxMain.TeleportFoxToTarget(GameManager.GetPlayerTransform());

			}

            
            if(level >= 4 && FoxVars.fox != null)
            {
                FoxVars.timeToSpawnStarted = true;
                
                //MelonLogger.Msg("After load and teleport");
            }
            
        }

        public override void OnUpdate()
        {
            // One time teleport to player
            if(FoxVars.timeToSpawnStarted==true)
            {
                if(FoxVars.foxSpawnTimer > 0)
                {
                    FoxVars.foxSpawnTimer -= Time.deltaTime;
                }
                else
                {
                    FoxVars.foxSpawnTimer = FoxVars.timeToSpawn;
                    FoxVars.timeToSpawnStarted = false;
                    
                    //if(PlayerManager.m_TeleportPending() == false)
                    SnowFoxTeleportFoxMain.TeleportFoxToTarget(GameManager.GetPlayerTransform());
                    SnowFoxPawPrintsMain.initPaws();
                }
            }

            // Do ALL THE STUFF
            if (FoxVars.foxactive == true && FoxVars.foxSpawned == true && FoxVars.fox != null)
            {
                //MelonLogger.Msg("doing something?");
                FoxVars.idleRand = UnityEngine.Random.Range(1, 5); // make some randoms for changing through idle animations
               

                //FoxVars.randomTarget.position = new Vector3(FoxVars.foxTransform.position.x + FoxVars.randomTarget.position.x + UnityEngine.Random.insideUnitCircle.x * 3, FoxVars.foxTransform.position.y + FoxVars.randomTarget.position.y + UnityEngine.Random.insideUnitCircle.y * 3, 0);

                // Update player, target and fox transforms               
                FoxVars.playerTransform = GameManager.GetPlayerTransform();
                FoxVars.foxTransform = FoxVars.fox.transform;
                
                SnowFoxInputsMain.SnowFoxInputsUpdate();
                SnowFoxManageMovementMain.SnowFoxManageMovement();
                SnowFoxAuroraMain.SnowFoxAurora();
                SnowFoxTargetingMain.SnowFoxTargeting();

                SnowFoxPawPrintsMain.leavePaw(FoxVars.foxFLHandTrack.position, true, true);
                SnowFoxPawPrintsMain.leavePaw(FoxVars.foxFRHandTrack.position, true, false);
                SnowFoxPawPrintsMain.leavePaw(FoxVars.foxRLHandTrack.position, false, true);
                SnowFoxPawPrintsMain.leavePaw(FoxVars.foxRRHandTrack.position, false, false);
            }          
        }

        public override void OnFixedUpdate()
        {
            if (FoxVars.foxactive == true && FoxVars.foxSpawned == true && FoxVars.fox != null)
            {
                
            }
        }
    }
}