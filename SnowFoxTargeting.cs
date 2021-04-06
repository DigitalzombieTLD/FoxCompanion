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
                layerMask |= 1 << 19; // gear layer
                layerMask |= 1 << 14; // player layer

                FoxVars.sphereTargethit = Physics.SphereCastAll(GameManager.GetMainCamera().transform.position, 0.35f, GameManager.GetMainCamera().transform.TransformDirection(Vector3.forward), 200f, layerMask); // spherecast to find a rabbit.

                foreach (RaycastHit foo in FoxVars.sphereTargethit)
                {
                    if ((foo.transform.gameObject.name != null) && foo.transform.gameObject.name.Contains("GEAR_RawMeatRabbit") == true || foo.transform.gameObject.name.Contains("GEAR_Raw") == true && foo.transform.gameObject.activeSelf)
                    {
                        FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        if (Settings.options.settingDisplayMsg == true)
                        {
                            HUDMessage.AddMessage("[Fox] Eat raw food", false);
                        }
                        
                        FoxVars.sphereTargetObject = 4;
                        //MelonLogger.Log("Target sphere: " + foo.transform.gameObject.name);

                        FoxVars.foundRabbit = false;
                        FoxVars.foundItem = false;
                        FoxVars.foundBed = false;
                        FoxVars.foundFood = true;
                        //MelonLogger.Log("Found food: " + foo.transform.gameObject.name);

                    }
                    else if ((foo.transform.gameObject.name != null) && foo.transform.gameObject.name.Contains("WILDLIFE_Rabbit") == true && foo.transform.gameObject.activeSelf)
                    {
                        FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        FoxVars.sphereTargetObject = 2;
                        //MelonLogger.Log("Target sphere: " + foo.transform.gameObject.name);
                        FoxVars.whiteRabbit = foo.transform.gameObject;
                        if (Settings.options.settingDisplayMsg == true)
                        {
                            HUDMessage.AddMessage("[Fox] Hunt rabbit", false);
                        }
                        
                        FoxVars.foundRabbit = true;
                        FoxVars.foundBed = false;
                        FoxVars.foundItem = false;
                        FoxVars.foundFood = false;
                    }                   
                    else if ((foo.transform.gameObject.name != null) && foo.transform.gameObject.name.Contains("GEAR_") == true && foo.transform.gameObject.activeSelf)
                    {
                        FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        FoxVars.carryLantern = foo.transform.gameObject;
                        FoxVars.sphereTargetObject = 3;
                        //MelonLogger.Log("Target sphere: " + foo.transform.gameObject.name);
                        if (Settings.options.settingDisplayMsg == true)
                        {
                            HUDMessage.AddMessage("[Fox] Fetch item", false);
                        }
                        
                        FoxVars.foundRabbit = false;
                        FoxVars.foundItem = true;
                        FoxVars.foundBed = false;
                        FoxVars.foundFood = false;
                        //MelonLogger.Log("Found item: " + foo.transform.gameObject.name);

                    }
                    else if (foo.transform.gameObject.name != null && foo.transform.gameObject.name.Contains("Fox") == true)
                    {
                        FoxVars.sphereLastHit = foo.transform.gameObject.transform;
                        FoxVars.sphereLastHitObj = foo.transform.gameObject;
                        FoxVars.sphereTargetObject = 1;
                        FoxVars.foundBed = false;
                        FoxVars.foundFood = false;
                        FoxVars.foundItem = false;
                        FoxVars.foundRabbit = false;

                        if (Settings.options.settingDisplayMsg == true)
                        {
                            if (Settings.options.foxCalories > 9000)
                            {
                                HUDMessage.AddMessage("[Please ... no more ... food]", false);
                            }
                            else if (Settings.options.foxCalories > 6000)
                            {
                                HUDMessage.AddMessage("[What are you doing?]", false);
                            }
                            else if (Settings.options.foxCalories > 3000)
                            {
                                HUDMessage.AddMessage("[Please stop feeding the fox]", false);
                            }
                            else if (Settings.options.foxCalories > 2000)
                            {
                                HUDMessage.AddMessage("[The fox is overfed]", false);
                            }
                            else if (Settings.options.foxCalories > 1000)
                            {
                                HUDMessage.AddMessage("[The fox is not hungry]", false);
                            }
                            else if(Settings.options.foxCalories > 600)
                            {
                                HUDMessage.AddMessage("[The fox is a bit hungry]", false);
                            }                            
                            else if (Settings.options.foxCalories > 150)
                            {
                                HUDMessage.AddMessage("[The fox is a very hungry]", false);
                            }
                            else if (Settings.options.foxCalories <= 150)
                            {
                                HUDMessage.AddMessage("[The fox is starving]", false);
                            }
                        }                     

                    }
                    else
                    {
                       
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