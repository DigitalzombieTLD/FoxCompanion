using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxFetchTargetMain : MelonMod
    {
        public static void SnowFoxFetchTarget()
        {
            if ((FoxVars.foundRabbit == true) && FoxVars.targetTransform != null && Settings.options.foxCalories >= 300)
            {
                FoxVars.rangeToTarget = Vector3.Distance(FoxVars.foxTransform.position, FoxVars.targetTransform.position);

                if (FoxVars.rangeToTarget >= 0.3f)
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



                    // Set speed in relation to distance to target
                    if (FoxVars.rangeToTarget >= 16f)
                    {
                        FoxVars.foxanimator.speed = 1.8f;
                    }
                    else if (FoxVars.rangeToTarget < 16f && FoxVars.rangeToTarget >= 12f)
                    {
                        FoxVars.foxanimator.speed = 1.5f;
                    }
                    else if (FoxVars.rangeToTarget < 12f && FoxVars.rangeToTarget >= 7f)
                    {
                        FoxVars.foxanimator.speed = 1.3f;
                    }
                    else if (FoxVars.rangeToTarget < 7f && FoxVars.rangeToTarget >= 0.5f)
                    {
                        if (FoxVars.rabbidEvaded == false)
                        {
                            FoxVars.targetHitObject.GetComponentInChildren<BaseAi>().ForceWanderPause(-1);
                        }

                        if (FoxVars.foxJumping == false)
                        {
                            FoxVars.foxanimator.speed = 1.3f;
                        }
                        else
                        {
                            FoxVars.foxanimator.speed = 2.2f;
                            //FoxVars.foxanimator.SetFloat("Vertical", 3f);
                        } 


                        if (Mathf.Abs(FoxVars.rangeToTarget - 3.3f) < 0.15f && !FoxVars.foxanimator.GetCurrentAnimatorStateInfo(FoxVars.foxanimator.GetLayerIndex("Fox")).IsName("Jump Forward (Single)"))
                        {  
                            FoxVars.foxJumping = true;
                            FoxVars.foxanimator.Play("Jump Forward (Single)", FoxVars.foxanimator.GetLayerIndex("Fox"), 0f);
                            MelonLogger.Log("Jump!");
                            FoxVars.rabbidCatchRand = UnityEngine.Random.Range(0, 150);
                        }
                    }
                    
                    FoxVars.foxanimator.SetFloat("Vertical", FoxVars.rangeToTarget / 3);



                    FoxVars._velocity.y += FoxVars.Gravity * Time.deltaTime;
                    FoxVars.foxController.Move(FoxVars._velocity * Time.deltaTime);
                }
                else
                {
                    // MelonLogger.Log("Random: " + FoxVars.rabbidCatchRand);
                    //FoxVars.rabbidCatchRand <= Settings.options.foxCatchChance 
                    
                    if (FoxVars.rabbidCatchRand <= Settings.options.foxCalories/10)
                    {

                        MelonLogger.Log("Killed the rabbit!");
                        FoxVars.foundRabbit = false;
                        FoxVars.foxJumping = false;
                        //FoxVars.foxState_Movement = "idle";
                        FoxVars.targetHitObject.GetComponentInChildren<BaseAi>().ApplyDamage(200f, 0f, DamageSource.Unspecified, "null");
                        FoxVars.rabbidKilled = true;

                        GameObject foxJaw;
                        foxJaw = FoxVars.foxTransform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject;
                        FoxVars.whiteRabbit.transform.SetParent(foxJaw.transform, true);

                        FoxVars.whiteRabbit.transform.localPosition = new Vector3(FoxVars.bunnyX, FoxVars.bunnyY, FoxVars.bunnyZ); //new Vector3(0.01f, 0.05f, 0.523f);
                        FoxVars.whiteRabbit.transform.localRotation = Quaternion.Euler(0, 0, 0.0f);
                        //child.transform.SetParent(null);
                        FoxVars.foxShouldFollowPlayer = true;
                        FoxVars.foxShouldFollowSomething = false;
                    }
                    else
                    {
                        MelonLogger.Log("Rabbit evaded!");
                        FoxVars.rabbidEvaded = true;
                        FoxVars.foundRabbit = false;
                        FoxVars.foxJumping = false;
                        FoxVars.foxShouldFollowSomething = false;
                        FoxVars.foxShouldFollowPlayer = true;
                        FoxVars.sphereTargetObject = 0;


                        FoxVars.targetHitObject.GetComponentInChildren<BaseAi>().ExitWanderPaused();
                        FoxVars.targetHitObject.GetComponentInChildren<BaseAi>().SetFleeReason(BaseAi.AiFleeReason.ThrownStone);
                        FoxVars.targetHitObject.GetComponentInChildren<BaseAi>().FleeFrom(FoxVars.foxTransform);
                        FoxVars.targetHitObject.GetComponentInChildren<BaseAi>().EnterFlee();
                    }
                }
            }
            else if(Settings.options.foxCalories < 300)
            {
                FoxVars.foundRabbit = false;
                FoxVars.foxShouldFollowSomething = false;

                if (Settings.options.settingDisplayMsg == true)
                {
                    HUDMessage.AddMessage("[The fox is too hungry to obey]", false);
                }
            }

        }
    }
}