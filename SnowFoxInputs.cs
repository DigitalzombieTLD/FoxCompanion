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
                }
                else if (FoxVars.foxShouldFollowPlayer == true)
                {
                    //MelonLogger.Log("Fox is now waiting");
                    FoxVars.foxShouldFollowPlayer = false;
                    FoxVars.foxState_Movement = "idle";
                    FoxVars.idleStateChangeTimer = 0;
                }
            }

            // Teleport fox to player
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.GetInputKeyFromString(Settings.options.buttonTeleport)) && FoxVars.foxactive == true && FoxVars.foxSpawned == true) // Teleport fox to player
            {
                SnowFoxTeleportFoxMain.TeleportFoxToTarget(FoxVars.playerTransform);               
            }

            /*
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.GetInputKeyFromString(Settings.options.buttonFollowTarget))) // Enable crosshair
            {
                FoxVars.showCrosshair = true;
            }

            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.GetInputKeyFromString(Settings.options.buttonFollowTarget))) // Order fox to point, disable crosshair
            {
                FoxVars.showCrosshair = false;

                RaycastHit hit;

                if (Physics.Raycast(GameManager.GetMainCamera().transform.position, GameManager.GetMainCamera().transform.TransformDirection(Vector3.forward), out hit, 500))
                {
                    MelonLogger.Log("Target: " + hit.transform.gameObject.transform);
                    //FoxVars.targetHitObject = hit.transform;
                    //FoxVars.targetTransform = hit.transform;
                    FoxVars.targetTransform = hit.transform.gameObject.transform;
                    FoxVars.foxShouldFollowPlayer = false;
                    FoxVars.foxShouldFollowSomething = true;
                }
            }*/

            // Rotate bunny, for positioning
            /*
            if (Input.GetKeyDown(KeyCode.Keypad1)) 
            {
                if (FoxVars.targetTransform != null)
                {
                    FoxVars.bunnyX = FoxVars.bunnyX + 0.01f;
                    FoxVars.whiteRabbit.transform.localPosition = new Vector3(FoxVars.bunnyX, FoxVars.bunnyY, FoxVars.bunnyZ);
                    MelonLogger.Log("Position X" + FoxVars.whiteRabbit.transform.localPosition.x);
                }
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                if (FoxVars.targetTransform != null)
                {
                    FoxVars.bunnyY = FoxVars.bunnyY + 0.01f;
                    FoxVars.whiteRabbit.transform.localPosition = new Vector3(FoxVars.bunnyX, FoxVars.bunnyY, FoxVars.bunnyZ);
                    MelonLogger.Log("Position Y" + FoxVars.whiteRabbit.transform.localPosition.y);
                }
            }
            if (Input.GetKeyDown(KeyCode.Keypad3)) 
            {
                    if (FoxVars.targetTransform != null)
                    {
                        FoxVars.bunnyZ = FoxVars.bunnyZ + 0.01f;
                    FoxVars.whiteRabbit.transform.localPosition = new Vector3(FoxVars.bunnyX, FoxVars.bunnyY, FoxVars.bunnyZ);
                    MelonLogger.Log("Position Z" + FoxVars.whiteRabbit.transform.localPosition.z);
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                if (FoxVars.targetTransform != null)
                {
                    FoxVars.bunnyX = FoxVars.bunnyX - 0.01f;
                    FoxVars.whiteRabbit.transform.localPosition = new Vector3(FoxVars.bunnyX, FoxVars.bunnyY, FoxVars.bunnyZ);
                    MelonLogger.Log("Position X" + FoxVars.whiteRabbit.transform.localPosition.x);
                }
            }
            if (Input.GetKeyDown(KeyCode.Keypad5)) 
            {
                if (FoxVars.targetTransform != null)
                {
                    FoxVars.bunnyY = FoxVars.bunnyY - 0.01f;
                    FoxVars.whiteRabbit.transform.localPosition = new Vector3(FoxVars.bunnyX, FoxVars.bunnyY, FoxVars.bunnyZ);
                    MelonLogger.Log("Position Y" + FoxVars.whiteRabbit.transform.localPosition.y);
                }
            }
            if (Input.GetKeyDown(KeyCode.Keypad6)) 
            {
                if (FoxVars.targetTransform != null)
                {
                    FoxVars.bunnyZ = FoxVars.bunnyZ - 0.01f;
                    FoxVars.whiteRabbit.transform.localPosition = new Vector3(FoxVars.bunnyX, FoxVars.bunnyY, FoxVars.bunnyZ);
                    MelonLogger.Log("Position Z" + FoxVars.whiteRabbit.transform.localPosition.z);
                }
            }
            */


            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.GetInputKeyFromString(Settings.options.buttonCommandMode)) && FoxVars.foxactive == true && FoxVars.foxSpawned == true && FoxVars.targetsphereActive == false) // Enable targeting mode
            {
                //FoxVars.showCrosshair = true;
                FoxVars.targetsphereActive = true;
            }

            if (InputManager.GetMouseButtonDown(InputManager.m_CurrentContext, 0) && FoxVars.foxactive == true && FoxVars.foxSpawned == true && FoxVars.targetsphereActive == true)
            {                
                if(FoxVars.targetsphereActive == true)
                {                    
                    FoxVars.sphererend.material.color = new Color(0.1f, 0.1f, 1.0f, 0.0f);

                    if (FoxVars.sphereTargetObject == 2 && FoxVars.foundRabbit == true)
                    {
                        FoxVars.targetHitObject = FoxVars.sphereLastHitObj;

                        FoxVars.targetTransform = FoxVars.sphereLastHitObj.transform;

                        FoxVars.foxShouldFollowPlayer = false;
                        FoxVars.foxShouldFollowSomething = true;
                        FoxVars.rabbidKilled = false;
                        FoxVars.rabbidEvaded = false;
                            
                        MelonLogger.Log("Found the white Rabbit! " + FoxVars.whiteRabbit.name);                        
                    }

                    if (FoxVars.sphereTargetObject == 3)
                    {
                        FoxVars.targetHitObject = FoxVars.sphereLastHitObj;

                        FoxVars.targetTransform = FoxVars.sphereLastHitObj.transform;

                        FoxVars.foxShouldFollowPlayer = false;
                        FoxVars.foxShouldFollowSomething = true;
                        FoxVars.rabbidKilled = false;
                        FoxVars.rabbidEvaded = false;
                    }

                    FoxVars.targetsphereActive = false;
                }
            }

            if (InputManager.GetMouseButtonDown(InputManager.m_CurrentContext, 1) && FoxVars.foxactive == true && FoxVars.foxSpawned == true && FoxVars.targetsphereActive == true)
            {
                if (FoxVars.targetsphereActive == true)
                {
                    FoxVars.targetsphereActive = false;
                    FoxVars.foundRabbit = false;
                    FoxVars.sphererend.material.color = new Color(0.1f, 0.1f, 1.0f, 0.0f);
                }
            }
        }
    }
}