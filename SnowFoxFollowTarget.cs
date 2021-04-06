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

                if (FoxVars.rangeToTarget >= 3.0f)
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
                    SnowFoxAlignToGroundMain.SnowFoxAlignToGround();
                    // Forward movement (funzt but jerky)
                    //FoxVars.foxanimator.SetFloat("Vertical", FoxVars.rangeToTarget / 3);
                    //FoxVars.foxanimator.SetBool("Stand", false);                                  

                    // Set animation speed in relation to distance to target                  

                    // Set speed in relation to distance to target
                    if (FoxVars.rangeToTarget >= 16f)
                    {
                        if (FoxVars.foxanimator.speed != 1.8)
                        {
                            FoxVars.foxanimator.speed = 1.8f;
                        }
                    }
                    else if(FoxVars.rangeToTarget < 16f && FoxVars.rangeToTarget >= 12f)
                    {
                        if (FoxVars.foxanimator.speed != 1.5)
                        {
                            FoxVars.foxanimator.speed = 1.5f;
                        }
                    }
                    else if (FoxVars.rangeToTarget < 12f && FoxVars.rangeToTarget >= 5f)
                    {
                        if (FoxVars.foxanimator.speed != 1.3)
                        {
                            FoxVars.foxanimator.speed = 1.3f;
                        }
                    }
                    else if (FoxVars.rangeToTarget < 5f && FoxVars.rangeToTarget >= 0f)
                    {
                        if (FoxVars.foxanimator.speed != 1.0)
                        {
                            FoxVars.foxanimator.speed = 1.0f;
                        }
                    }

                    FoxVars.foxanimator.SetFloat("Vertical", FoxVars.rangeToTarget/3);

                    // Old turn to player function, works but not as nice
                    //FoxVars.foxTransform.rotation = Quaternion.Slerp(FoxVars.foxTransform.rotation, Quaternion.LookRotation(FoxVars.playerTransform.position - FoxVars.foxTransform.position), FoxVars.rotationSpeed * Time.deltaTime);

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