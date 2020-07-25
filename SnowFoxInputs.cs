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
                    //MelonModLogger.Log("Fox is now following player");
                    FoxVars.foxShouldFollowPlayer = true;
                    FoxVars.foxShouldFollowSomething = false;
                }
                else if (FoxVars.foxShouldFollowPlayer == true)
                {
                    //MelonModLogger.Log("Fox is now waiting");
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
                    MelonModLogger.Log("Target: " + hit.transform.gameObject.transform);
                    FoxVars.targetHitObject = hit.transform.gameObject;             
                    FoxVars.foxShouldFollowPlayer = false;
                    FoxVars.foxShouldFollowSomething = true;
                }
            }
        }
    }
}