using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxInputsMain : MelonMod
    {  

        public static void SnowFoxInputsUpdate()
        {
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.GetInputKeyFromString(Settings.options.buttonFollowPlayer)) && FoxVars.foxactive == true && FoxVars.foxSpawned == true)
            {
                // Toggle following player
                if (FoxVars.foxShouldFollowPlayer == false)
                {
                    //MelonLogger.Log("Fox is now following player");
                    FoxVars.foxShouldFollowPlayer = true;
                    FoxVars.foxShouldFollowSomething = false;
                    if (Settings.options.settingDisplayMsg == true)
                    {
                        HUDMessage.AddMessage("[Fox] Follow mode enabled", false);
                    }
                    
                }
                else if (FoxVars.foxShouldFollowPlayer == true)
                {
                    //MelonLogger.Log("Fox is now waiting");
                    FoxVars.foxShouldFollowPlayer = false;
                    FoxVars.foxState_Movement = "idle";
                    if (Settings.options.settingDisplayMsg == true)
                    {
                        HUDMessage.AddMessage("[Fox] Follow mode disabled", false);
                    }                    
                    FoxVars.idleStateChangeTimer = 0;
                }
            }

            // Teleport fox to player
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.GetInputKeyFromString(Settings.options.buttonTeleport)) && FoxVars.foxactive == true && FoxVars.foxSpawned == true) // Teleport fox to player
            {
                SnowFoxTeleportFoxMain.TeleportFoxToTarget(FoxVars.playerTransform);

                if (Settings.options.settingDisplayMsg == true)
                {
                    HUDMessage.AddMessage("[Fox] Teleport to player", false);
                }
            }


            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.GetInputKeyFromString(Settings.options.buttonCommandMode)) && FoxVars.foxactive == true && FoxVars.foxSpawned == true && FoxVars.targetsphereActive == false) // Enable targeting mode
            {
                //FoxVars.showCrosshair = true;
                //Settings.options.foxCalories = Settings.options.foxCalories - 100;
                
                FoxVars.targetsphereActive = true;
                
                GameManager.GetPlayerManagerComponent().SetControlMode(PlayerControlMode.BearSpear);

                if(GameManager.GetPlayerAnimationComponent().m_State != PlayerAnimation.State.Aiming)
                {
                    FoxVars.tempPlayerState = GameManager.GetPlayerAnimationComponent().m_State;
                }
                //GameManager.GetPlayerAnimationComponent().m_State = PlayerAnimation.State.Aiming;
                //GameManager.GetPlayerAnimationComponent().MaybeSetState(PlayerAnimation.State.Aiming);               
            }

            if (InputManager.GetMouseButtonDown(InputManager.m_CurrentContext, 0) && FoxVars.foxactive == true && FoxVars.foxSpawned == true && FoxVars.targetsphereActive == true)
            {                
                if(FoxVars.targetsphereActive == true)
                {
                    //GameManager.GetPlayerManagerComponent().SetControlMode(PlayerControlMode.Normal);
                    //GameManager.GetPlayerAnimationComponent().m_State = FoxVars.tempPlayerState;
                    //GameManager.GetPlayerAnimationComponent().m_State = PlayerAnimation.State.Hidden;
                    GameManager.GetPlayerManagerComponent().SetControlMode(PlayerControlMode.Normal);
                    if (FoxVars.sphereTargetObject == 1)
                    {
                        
                        FoxVars.targetHitObject = FoxVars.sphereLastHitObj;

                        FoxVars.targetTransform = FoxVars.sphereLastHitObj.transform;

                        FoxVars.foxShouldFollowPlayer = false;
                        FoxVars.foxShouldFollowSomething = false;
                        FoxVars.rabbidKilled = false;
                        FoxVars.rabbidEvaded = false;

                        FoxVars.ringred.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringgreen.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringblue.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringwhite.transform.position = new Vector3(-1000, -1000, -1000);
                    }

                    if (FoxVars.sphereTargetObject == 2 && FoxVars.foundRabbit == true)
                    {
                        FoxVars.targetHitObject = FoxVars.sphereLastHitObj;

                        FoxVars.targetTransform = FoxVars.sphereLastHitObj.transform;

                        FoxVars.foxShouldFollowPlayer = false;
                        FoxVars.foxShouldFollowSomething = true;
                        FoxVars.rabbidKilled = false;
                        FoxVars.rabbidEvaded = false;
                            
                        //MelonLogger.Log("Found the rabbit! " + FoxVars.whiteRabbit.name);

                        FoxVars.ringred.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringgreen.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringblue.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringwhite.transform.position = new Vector3(-1000, -1000, -1000);
                    }

                    if (FoxVars.sphereTargetObject == 3)
                    {
                        
                        FoxVars.targetHitObject = FoxVars.sphereLastHitObj;
                        
                        FoxVars.targetTransform = FoxVars.sphereLastHitObj.transform;
                        
                        FoxVars.foxShouldFollowPlayer = false;
                        FoxVars.foxShouldFollowSomething = true;
                        FoxVars.rabbidKilled = false;
                        FoxVars.rabbidEvaded = false;

                        FoxVars.ringred.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringgreen.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringblue.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringwhite.transform.position = new Vector3(-1000, -1000, -1000);
                    }

                    if (FoxVars.sphereTargetObject == 4)
                    {
                        
                        FoxVars.targetHitObject = FoxVars.sphereLastHitObj;
                       
                        FoxVars.targetTransform = FoxVars.sphereLastHitObj.transform;

                        FoxVars.foxShouldFollowPlayer = false;
                        FoxVars.foxShouldFollowSomething = true;

                        FoxVars.ringred.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringgreen.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringblue.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringwhite.transform.position = new Vector3(-1000, -1000, -1000);
                    }
                   
                    FoxVars.targetsphereActive = false;
                }
            }

            if (InputManager.GetMouseButtonDown(InputManager.m_CurrentContext, 1) && FoxVars.foxactive == true && FoxVars.foxSpawned == true)
            {     
                    if(FoxVars.targetsphereActive == true)
                    {
                    //GameManager.GetPlayerAnimationComponent().m_State = FoxVars.tempPlayerState;
                    //GameManager.GetPlayerAnimationComponent().m_State = PlayerAnimation.State.Hidden;
                    //GameManager.GetPlayerManagerComponent().SetControlMode(PlayerControlMode.Normal);
                    GameManager.GetPlayerManagerComponent().SetControlMode(PlayerControlMode.Normal);
                    }
                    //GameManager.GetPlayerAnimationComponent().MaybeSetState(PlayerAnimation.State.Aiming);

                FoxVars.targetsphereActive = false;
                    FoxVars.foundRabbit = false;
                    FoxVars.foundBed = false;
                    FoxVars.foundItem = false;
                   
                    FoxVars.ringred.transform.position = new Vector3(-1000, -1000, -1000);
                    FoxVars.ringgreen.transform.position = new Vector3(-1000, -1000, -1000);
                    FoxVars.ringblue.transform.position = new Vector3(-1000, -1000, -1000);
                    FoxVars.ringwhite.transform.position = new Vector3(-1000, -1000, -1000);
            }
        }
    }
}