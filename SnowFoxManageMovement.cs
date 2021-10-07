using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxManageMovementMain : MelonMod
    {
        public static void SnowFoxManageMovement()
        {
            if (FoxVars.foxShouldFollowSomething == true && FoxVars.foxShouldFollowPlayer == false)
            {
                if (FoxVars.foundRabbit == true && FoxVars.sphereTargetObject == 2)
                {                   
                    SnowFoxFetchTargetMain.SnowFoxFetchTarget();
                }

                if (FoxVars.foundItem == true && FoxVars.sphereTargetObject == 3)
                {
                    //SnowFoxFetchItemMain.SnowFoxFetchItem();

                  /*  if (!FoxVars.foundPath && !FoxVars.foxPathfinder.havePath())
                    {
                        FoxVars.foundPath = true;
                       
                        FoxVars.foxPathfinder.findPath(FoxVars.targetTransform.position, FoxVars.fox.transform.position);

						//FoxVars.foxWaypoints = FoxVars.foxPathfinder.getPath();

						//foreach (Vector3 singlePoint in FoxVars.foxWaypoints)
						//{
						//	MelonLogger.Msg("Waypoint: " + singlePoint.x + " / " + singlePoint.y + " / " + singlePoint.z);
						//	GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
						//	sphere.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
						//	sphere.transform.position = singlePoint;
						//}
						
					}*/
					SnowFoxFetchItemMain.SnowFoxFetchItem();
					
					/*
					if (FoxVars.pathTimer >= 6f)
					{
						SnowFoxFetchItemMain.SnowFoxFetchItem();
					}
					else 
					{
						FoxVars.pathTimer += Time.deltaTime;
					}		*/

				}             

                if (FoxVars.foundFood == true && FoxVars.sphereTargetObject == 4)
                {
                    SnowFoxInteractionEatMain.InteractionEat();
                }
            }
            else if (FoxVars.foxShouldFollowSomething == false && FoxVars.foxShouldFollowPlayer == true)
            {
                FoxVars.targetTransform = FoxVars.playerTransform;
                SnowFoxFollowTargetMain.SnowFoxFollowTarget();
            }
            else
            {
                FoxVars.targetTransform = null;
                FoxVars.foxState_Movement = "idle";
                FoxVars.foxanimator.SetBool("Stand", true);
                SnowFoxIdleMain.FoxIdle();

                //FoxVars.idleChangeTimer = 0;
            }
        }
    }
}