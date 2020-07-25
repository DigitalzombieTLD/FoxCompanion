using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxIdleMain : MelonMod
    {
        public static void FoxIdle()
        {
            FoxVars.foxanimator.speed = 1.0f;
            // Advance timer till sit / lay / sleep
            FoxVars.idleStateChangeTimer += Time.deltaTime;

            if (FoxVars.idleStateChangeTimer >= FoxVars.timeToSleep && FoxVars.idleStateCounter != 3)
            {
                FoxVars.idleStateCounter = 3;
                FoxVars.foxanimator.Play("Laying to Sleep", 0, 0f);
                MelonModLogger.Log("Fox is sleeping");
            }
            else if (FoxVars.idleStateChangeTimer >= FoxVars.timeToLay && FoxVars.idleStateChangeTimer < FoxVars.timeToSleep && FoxVars.idleStateCounter != 2)
            {
                FoxVars.idleStateCounter = 2;
                FoxVars.foxanimator.Play("Seat to Laying", 0, 0f);
                MelonModLogger.Log("Fox is laying");
            }
            else if (FoxVars.idleStateChangeTimer >= FoxVars.timeToSit && FoxVars.idleStateChangeTimer < FoxVars.timeToLay && FoxVars.idleStateCounter != 1)
            {
                if (FoxVars.foxanimator.GetCurrentAnimatorStateInfo(FoxVars.foxanimator.GetLayerIndex("Fox")).IsName("Idle 02") || FoxVars.foxanimator.GetCurrentAnimatorStateInfo(FoxVars.foxanimator.GetLayerIndex("Fox")).IsName("Idle 03") || FoxVars.foxanimator.GetCurrentAnimatorStateInfo(FoxVars.foxanimator.GetLayerIndex("Fox")).IsName("Idle04") || FoxVars.foxanimator.GetCurrentAnimatorStateInfo(FoxVars.foxanimator.GetLayerIndex("Fox")).IsName("Dig"))
                {
                    // Animation is overlapping with one already playing! Not changing
                }
                else
                {
                    FoxVars.idleStateCounter = 1;
                    FoxVars.foxanimator.Play("Stand to Seat", 0, 0f);
                    MelonModLogger.Log("Fox is sitting");
                }             
            }
            else if (FoxVars.idleStateChangeTimer > FoxVars.timeToIdle && FoxVars.idleStateChangeTimer < FoxVars.timeToSit && FoxVars.idleStateCounter == 0)
            {
                FoxVars.idleChangeTimer += Time.deltaTime;
                //MelonModLogger.Log("Fox is idle");

                if (FoxVars.idleChangeTimer > FoxVars.idlechangeTime)
                {
                    if (FoxVars.foxanimator.GetCurrentAnimatorStateInfo(FoxVars.foxanimator.GetLayerIndex("Fox")).IsName("Idle 02") || FoxVars.foxanimator.GetCurrentAnimatorStateInfo(FoxVars.foxanimator.GetLayerIndex("Fox")).IsName("Idle 03") || FoxVars.foxanimator.GetCurrentAnimatorStateInfo(FoxVars.foxanimator.GetLayerIndex("Fox")).IsName("Idle04") || FoxVars.foxanimator.GetCurrentAnimatorStateInfo(FoxVars.foxanimator.GetLayerIndex("Fox")).IsName("Dig"))
                    {
                        // Animation is overlapping with one already playing! Not changing
                    }
                    else
                    {
                        //MelonModLogger.Log("Fox is changing idle anim");
                        switch (FoxVars.idleRand)
                        {
                            case 1:
                                //foxanimator.Play("Idle 01", 0, 0f);   // Don't play standard idle again                       
                                break;
                            case 2:
                                FoxVars.foxanimator.Play("Idle 02", 0, 0f);
                                break;
                            case 3:
                                FoxVars.foxanimator.Play("Idle 03", 0, 0f);
                                break;
                            case 4:
                                FoxVars.foxanimator.Play("Idle04", 0, 0f);
                                break;
                            case 5:
                                FoxVars.foxanimator.Play("Dig", 0, 0f);
                                break;
                            default:
                                // Do nothing :)
                                break;
                        }
                        FoxVars.idleChangeTimer = 0;
                    }
                }
                FoxVars.idleStateCounter = 0;
            }
        }
    }
}