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
                    SnowFoxFetchItemMain.SnowFoxFetchItem();
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