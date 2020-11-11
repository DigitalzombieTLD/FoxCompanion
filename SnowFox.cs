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

        public override void OnLevelWasInitialized(int level)
        {
            FoxVars.loadedScene = level;
            //MelonLogger.Log("Level initialized: " + level);

            if (level >= 6 && FoxVars.fox == null)
            {               
                SnowFoxInstanceMain.SnowFoxInstanceLoad();
                FoxVars.timeToSpawnStarted = false;
                //MelonLogger.Log("After load and teleport");                
            }

            if(level >= 6 && FoxVars.fox != null)
            {
                FoxVars.timeToSpawnStarted = true;

                //MelonLogger.Log("After load and teleport");
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
                    SnowFoxTeleportFoxMain.TeleportFoxToTarget(GameManager.GetPlayerTransform());
                }
            }


            if (FoxVars.targetsphereActive == true)
            {
                int layerMask = 0;

                layerMask|= 1 << 16; // NPC layer
                layerMask |= 1 << 17; // gear layer

                FoxVars.sphereTargethit = Physics.SphereCastAll(GameManager.GetMainCamera().transform.position, 1.0f, GameManager.GetMainCamera().transform.TransformDirection(Vector3.forward), 200f, layerMask); // spherecast to find a rabbit.

                foreach (RaycastHit foo in FoxVars.sphereTargethit)
                {
                    if (foo.transform.gameObject.name.Contains("Rabbit") == true)
                    {
                        FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        FoxVars.sphereTargetObject = 2;
                        //MelonLogger.Log("Target sphere: " + foo.transform.gameObject.name);
                        FoxVars.whiteRabbit = foo.transform.gameObject;
                        FoxVars.foundRabbit = true;
                        FoxVars.targetsphere.transform.position = new Vector3(FoxVars.sphereLastHit.position.x, FoxVars.sphereLastHit.position.y + 0.2f, FoxVars.sphereLastHit.position.z);
                        FoxVars.sphererend.material.color = new Color(0.8f, 0.1f, 0.1f, 0.4f);
                    }
                    else if (foo.transform.gameObject.name.Contains("GEAR_") == true)
                    {
                        FoxVars.sphereLastHit = foo.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        FoxVars.sphereTargetObject = 3;
                        //MelonLogger.Log("Target sphere: " + foo.transform.gameObject.name);
                        
                        FoxVars.foundRabbit = false;
                        FoxVars.foundItem = true;

                        FoxVars.targetsphere.transform.position = new Vector3(FoxVars.sphereLastHit.position.x, FoxVars.sphereLastHit.position.y + 0.2f, FoxVars.sphereLastHit.position.z);
                        FoxVars.sphererend.material.color = new Color(0.1f, 0.8f, 0.1f, 0.4f);
                    }
                    else if (foo.transform.gameObject.name.Contains("Fox") == true)
                    {
                        FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        FoxVars.sphereTargetObject = 1;
                        //MelonLogger.Log("Target sphere: " + foo.transform.gameObject.name);
                        
                        FoxVars.foundRabbit = false;
                        FoxVars.targetsphere.transform.position = new Vector3(FoxVars.sphereLastHit.position.x, FoxVars.sphereLastHit.position.y + 0.2f, FoxVars.sphereLastHit.position.z);
                        FoxVars.sphererend.material.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
                    }
                    else
                    {
                        //MelonLogger.Log("Target sphere: " + foo.transform.gameObject.name);
                    }                  
                }

                if (FoxVars.sphereLastHit!=null)
                {
                    FoxVars.targetsphere.transform.position = new Vector3(FoxVars.sphereLastHit.position.x, FoxVars.sphereLastHit.position.y + 0.2f, FoxVars.sphereLastHit.position.z);
                }
            }
           

            // Do ALL THE STUFF
            if (FoxVars.foxactive == true && FoxVars.foxSpawned == true && FoxVars.fox != null)
            {
                //MelonLogger.Log("doing something?");
                FoxVars.idleRand = UnityEngine.Random.Range(1, 6); // make some randoms for changing through idle animations

                // Update player, target and fox transforms               
                FoxVars.playerTransform = GameManager.GetPlayerTransform();
                FoxVars.foxTransform = FoxVars.fox.transform;                

                SnowFoxInputsMain.SnowFoxInputsUpdate();
                SnowFoxManageMovementMain.SnowFoxManageMovement();
                SnowFoxAuroraMain.SnowFoxAurora();                    
            }
        } 
    }
}