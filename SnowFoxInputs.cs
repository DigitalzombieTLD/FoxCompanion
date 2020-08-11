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
            if (Input.GetKeyDown(Settings.GetInputKeyFromString(Settings.options.buttonFollowPlayer)))
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
            if (Input.GetKeyDown(Settings.GetInputKeyFromString(Settings.options.buttonTeleport))) // Teleport fox to player
            {
                SnowFoxTeleportFoxMain.TeleportFoxToTarget(FoxVars.playerTransform);               
            }

            if (Input.GetKeyDown(Settings.GetInputKeyFromString(Settings.options.buttonFollowTarget))) // Enable crosshair
            {
                FoxVars.showCrosshair = !FoxVars.showCrosshair;
            }

            if (Input.GetKeyUp(Settings.GetInputKeyFromString(Settings.options.buttonFollowTarget))) // Order fox to point, disable crosshair
            {
                FoxVars.showCrosshair = !FoxVars.showCrosshair;

                RaycastHit hit;

                if (Physics.Raycast(GameManager.GetMainCamera().transform.position, GameManager.GetMainCamera().transform.TransformDirection(Vector3.forward), out hit, 500))
                {
                    MelonLogger.Log("Target: " + hit.transform.gameObject.transform);
                    FoxVars.targetHitObject = hit.transform.gameObject;             
                    FoxVars.foxShouldFollowPlayer = false;
                    FoxVars.foxShouldFollowSomething = true;
                }
            }

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





            if (Input.GetKeyDown(Settings.GetInputKeyFromString(Settings.options.buttonCatchRabbit))) // Enable crosshair
            {
                FoxVars.showCrosshair = !FoxVars.showCrosshair;
            }



            if (Input.GetKeyUp(Settings.GetInputKeyFromString(Settings.options.buttonCatchRabbit))) // Test some stuff
            {
                FoxVars.showCrosshair = !FoxVars.showCrosshair;

                RaycastHit hit;
                

                if (Physics.Raycast(GameManager.GetMainCamera().transform.position, GameManager.GetMainCamera().transform.TransformDirection(Vector3.forward), out hit, 500))
                {
                    if(hit.transform.gameObject != null)
                    {
                        MelonLogger.Log("Target - name: " + hit.transform.gameObject.name);

                        if(hit.transform.gameObject.name== "WILDLIFE_Rabbit")
                        {
                            FoxVars.foundRabbit = true;                         
                            FoxVars.whiteRabbit = hit.transform.gameObject;                            
                        }
                        else if(hit.transform.parent.gameObject != null)
                        {
                            if (hit.transform.parent.gameObject.name == "WILDLIFE_Rabbit")
                            {
                                FoxVars.foundRabbit = true;
                                FoxVars.whiteRabbit = hit.transform.parent.gameObject;
                            }
                            else if (hit.transform.parent.parent.gameObject != null)
                            {
                                if(hit.transform.parent.parent.gameObject.name == "WILDLIFE_Rabbit")
                                {
                                    FoxVars.foundRabbit = true;
                                    FoxVars.whiteRabbit = hit.transform.parent.parent.gameObject;                            
                                }                                
                            }
                        }

                        if(FoxVars.foundRabbit == true)
                        {
                            FoxVars.targetHitObject = FoxVars.whiteRabbit;
                            FoxVars.foxShouldFollowPlayer = false;
                            FoxVars.foxShouldFollowSomething = true;
                            FoxVars.rabbidKilled = false;
                            FoxVars.rabbidEvaded = false;
                            MelonLogger.Log("Found the white Rabbit! " + FoxVars.whiteRabbit.name);                            
                        }                        
                    }                    
                }
            }
        }
    }
}