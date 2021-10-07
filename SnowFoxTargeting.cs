using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxTargetingMain : MelonMod
    {
        public static void SnowFoxTargeting()
        {
            if (FoxVars.targetsphereActive == true)
            {
                int layerMask = 0;

                layerMask |= 1 << 16; // NPC layer
                layerMask |= 1 << 17; // gear layer
                layerMask |= 1 << 19; // InteractiveProp layer
				layerMask |= 1 << 14; // player layer

                FoxVars.sphereTargethit = Physics.SphereCastAll(GameManager.GetMainCamera().transform.position, 0.35f, GameManager.GetMainCamera().transform.TransformDirection(Vector3.forward), 200f, layerMask); // spherecast to find a rabbit.

                foreach (RaycastHit foo in FoxVars.sphereTargethit)
                {
                    if ((foo.transform.gameObject.name != null) && (foo.transform.gameObject.name.Contains("GEAR_Raw") == true ||
					foo.transform.gameObject.name.Contains("GEAR_Pumpkin") ||foo.transform.gameObject.name.Contains("GEAR_Rose") ||
					foo.transform.gameObject.name.Contains("GEAR_Reishi") ||foo.transform.gameObject.name.Contains("GEAR_EnergyBar") ||
					foo.transform.gameObject.name.Contains("GEAR_DogFood_Open") ||foo.transform.gameObject.name.Contains("GEAR_Cooked") ||
					foo.transform.gameObject.name.Contains("GEAR_CandyBar") ||foo.transform.gameObject.name.Contains("GEAR_Beefjerky") ||
					foo.transform.gameObject.name.Contains("GEAR_Crackers") ||foo.transform.gameObject.name.Contains("GEAR_Ketchupchips") ||
					foo.transform.gameObject.name.Contains("GEAR_PeanutButter") ||foo.transform.gameObject.name.Contains("Socks") ||
					foo.transform.gameObject.name.Contains("GEAR_Meat") ||
					foo.transform.gameObject.name.Contains("GEAR_HomemadeSoup") == true))
                    {
                        FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        if (Settings.options.settingDisplayMsg == true)
                        {
                            HUDMessage.AddMessage(FoxVars.foxName + " can eat this", 1f, true, false);
                        }
                        
                        FoxVars.sphereTargetObject = 4;
                        //MelonLogger.Msg("Target sphere: " + foo.transform.gameObject.name);

                        FoxVars.foundRabbit = false;
                        FoxVars.foundItem = false;
                        FoxVars.foundBed = false;
                        FoxVars.foundFood = true;
                        //MelonLogger.Msg("Found food: " + foo.transform.gameObject.name);

                    }
                    else if ((foo.transform.gameObject.name != null) && foo.transform.gameObject.name.Contains("WILDLIFE_Rabbit") == true && foo.transform.gameObject.activeSelf)
                    {
						if (Settings.options.settingDisplayMsg == true)
						{
							HUDMessage.AddMessage("Send " + FoxVars.foxName + " to hunt", 1f, true, false);
						}
						FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        FoxVars.sphereTargetObject = 2;
                        //MelonLogger.Msg("Target sphere: " + foo.transform.gameObject.name);
                        FoxVars.whiteRabbit = foo.transform.gameObject;
                        
                        
                        FoxVars.foundRabbit = true;
                        FoxVars.foundBed = false;
                        FoxVars.foundItem = false;
                        FoxVars.foundFood = false;
                    }                   
                    else if ((foo.transform.gameObject.name != null) && foo.transform.gameObject.name.Contains("GEAR_") == true && foo.transform.gameObject.activeSelf)
                    {
						if (Settings.options.settingDisplayMsg == true)
						{
							HUDMessage.AddMessage(FoxVars.foxName + " can fetch this", 1f, true, false);
						}
						FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        FoxVars.carryLantern = foo.transform.gameObject;
                        FoxVars.sphereTargetObject = 3;
                        //MelonLogger.Msg("Target sphere: " + foo.transform.gameObject.name);
                        
                        
                        FoxVars.foundRabbit = false;
                        FoxVars.foundItem = true;
                        FoxVars.foundBed = false;
                        FoxVars.foundFood = false;
                        //MelonLogger.Msg("Found item: " + foo.transform.gameObject.name);

                       

                    }
                    else if (foo.transform.gameObject.name != null && foo.transform.gameObject.name.Contains("Fox") == true)
                    {
						
						HUDMessage.AddMessage(FoxVars.foxName, 1f, true, false);
						

						FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        FoxVars.sphereTargetObject = 1;
                        FoxVars.foundBed = false;
                        FoxVars.foundFood = false;
                        FoxVars.foundItem = false;
                        FoxVars.foundRabbit = false;                 
                    }      
                }

                if (FoxVars.sphereLastHit != null)
                {
                  
                    if (FoxVars.sphereTargetObject == 1) //fox
                    {
                        FoxVars.ringred.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringgreen.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringblue.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringwhite.transform.position = new Vector3(FoxVars.sphereLastHit.position.x, FoxVars.sphereLastHit.position.y + 0.09f, FoxVars.sphereLastHit.position.z - 0.12f);
                    }                   
                    else if (FoxVars.sphereTargetObject == 2) //rabbit
                    {
                        FoxVars.ringgreen.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringwhite.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringblue.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringred.transform.position = new Vector3(FoxVars.sphereLastHit.position.x, FoxVars.sphereLastHit.position.y + 0.055f, FoxVars.sphereLastHit.position.z);
                    }
                    else if (FoxVars.sphereTargetObject == 3) // GEAR
                    {
                        FoxVars.ringred.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringwhite.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringblue.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringgreen.transform.position = new Vector3(FoxVars.sphereLastHit.position.x, FoxVars.sphereLastHit.position.y+0.055f, FoxVars.sphereLastHit.position.z);
                    }
                    else if (FoxVars.sphereTargetObject == 4) // Food
                    {
                        FoxVars.ringred.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringwhite.transform.position = new Vector3(-1000, -1000, -1000);
                        FoxVars.ringblue.transform.position = new Vector3(FoxVars.sphereLastHit.position.x, FoxVars.sphereLastHit.position.y + 0.055f, FoxVars.sphereLastHit.position.z);
                        FoxVars.ringgreen.transform.position = new Vector3(-1000, -1000, -1000);
                    }
                }
            }
        }
    }
}