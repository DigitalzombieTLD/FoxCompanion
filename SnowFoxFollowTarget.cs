using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxFollowTargetMain : MelonMod
    {
        public static void SnowFoxFollowTarget()
        {
            if ((FoxVars.foxShouldFollowPlayer == true || FoxVars.foxShouldFollowSomething == true) && FoxVars.targetTransform != null)
            {
                FoxVars.rangeToTarget = Vector3.Distance(FoxVars.foxTransform.position, FoxVars.targetTransform.position);

                if (FoxVars.rangeToTarget >= Settings.options.foxStopDistanceSlider)
                {
                    FoxVars.foxState_Movement = "move";
                    FoxVars.foxanimator.SetBool("Stand", false);
                    FoxVars.idleStateChangeTimer = 0;
                    FoxVars.idleStateCounter = 0;

                    //rotate to look at the target
                    FoxVars.targetPlayerDir = FoxVars.targetTransform.position - FoxVars.foxTransform.position;

                    FoxVars.angleToTarget = Vector3.SignedAngle(FoxVars.targetPlayerDir, FoxVars.foxTransform.forward, Vector3.up);
                    FoxVars.angleToTargetTree = (FoxVars.angleToTarget / 100) * -1; // Range [-1.8] - [1.8]

                    // Smooth turn animation
                    FoxVars.angleToTargetTreeTemp = Mathf.SmoothDamp(FoxVars.angleToTargetTreeTemp, FoxVars.angleToTargetTree, ref FoxVars.angleVelocity, 0.2f);
                    FoxVars.foxanimator.SetFloat("Horizontal", FoxVars.angleToTargetTreeTemp);

                    // Forward movement (funzt but jerky)
                    //FoxVars.foxanimator.SetFloat("Vertical", FoxVars.rangeToTarget / 3);
                    //FoxVars.foxanimator.SetBool("Stand", false);                                  

                    // Set animation speed in relation to distance to target                  

                    // Set speed in relation to distance to target
                    if (FoxVars.rangeToTarget >= 16f)
                    {
                        FoxVars.foxanimator.speed = Settings.options.foxRunningSpeedSlider;
                    }
                    else if(FoxVars.rangeToTarget < 16f && FoxVars.rangeToTarget >= 12f)
                    {
                        FoxVars.foxanimator.speed = Settings.options.foxTrottingSpeedSlider;
                    }
                    else if (FoxVars.rangeToTarget < 12f && FoxVars.rangeToTarget >= 5f)
                    {
                        FoxVars.foxanimator.speed = Settings.options.foxWalkingSpeedSlider;
                    }
                    else if (FoxVars.rangeToTarget < 5f && FoxVars.rangeToTarget >= 0f)
                    {
                        FoxVars.foxanimator.speed = 1.0f;
                    }

                    FoxVars.foxanimator.SetFloat("Vertical", FoxVars.rangeToTarget/3);

                    // Old turn to player function, works but not as nice
                    //foxTransform.rotation = Quaternion.Slerp(foxTransform.rotation, Quaternion.LookRotation(playerTransform.position - foxTransform.position), rotationSpeed * Time.deltaTime);

                    FoxVars._velocity.y += FoxVars.Gravity * Time.deltaTime;
                    FoxVars.foxController.Move(FoxVars._velocity * Time.deltaTime);
                }
                else
                {                    
                    FoxVars.foxState_Movement = "idle";
                    FoxVars.foxanimator.SetBool("Stand", true);
                    SnowFoxIdleMain.FoxIdle();
                }
            }
        }
    }
}