using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxInteractionBedMain : MelonMod
    {
        public static Vector3 foxVelocity;
        public static float jumpHeight = 0.5f;
        public static float gravityValue = -9.81f;
        public static float jumpTimer = 0f;
        public static float jumpTimerEnd = 0.002f;

        public static void InteractionBed()
        {
            if ((FoxVars.foundBed == true) && FoxVars.targetTransform != null)
            {
                FoxVars.rangeToTarget = Vector3.Distance(FoxVars.foxTransform.position, FoxVars.targetTransform.position);

                if (FoxVars.rangeToTarget >= 2.50f)
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

                    FoxVars.foxanimator.SetFloat("Vertical", 1f);

                    FoxVars._velocity.y += FoxVars.Gravity * Time.deltaTime;
                    FoxVars.foxController.Move(FoxVars._velocity * Time.deltaTime);
                    FoxVars.foxJumping = false;
                }
                else
                {
                    FoxVars.foxanimator.SetFloat("Vertical", 0.2f);
              
                    if (FoxVars.foxJumping == false)
                    {
                        FoxVars.foxanimator.speed = 0.5f;

                            FoxVars.foxJumping = true;
                            FoxVars.foxanimator.Play("Jump Forward (Single)", FoxVars.foxanimator.GetLayerIndex("Fox"), 0f);
                            MelonLogger.Log("Jump!");

                            FoxVars.foundBed = false;
                            MelonLogger.Log("Lying on bed!");
                            FoxVars.foxShouldFollowSomething = false;
                            FoxVars.foxShouldFollowPlayer = false;
                            FoxVars.sphereTargetObject = 0;                     
                    }
                }
            }
        }
    }
}