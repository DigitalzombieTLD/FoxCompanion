using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxInteractionEatMain : MonoBehaviour
    {

        public static void InteractionEat()
        {
            if ((FoxVars.foundFood == true) && FoxVars.targetTransform != null)
            {
                FoxVars.rangeToTarget = Vector3.Distance(FoxVars.foxTransform.position, FoxVars.targetTransform.position);
                GearItem food = FoxVars.targetHitObject.GetComponent<GearItem>();

                if (FoxVars.rangeToTarget >= 0.7f)
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
                        if (FoxVars.foxanimator.speed != 1.8)
                        {
                            FoxVars.foxanimator.speed = 1.8f;
                        }
                        FoxVars.foxanimator.SetFloat("Vertical", FoxVars.rangeToTarget / 3);
                    }
                    else if (FoxVars.rangeToTarget < 16f && FoxVars.rangeToTarget >= 12f)
                    {
                        if (FoxVars.foxanimator.speed != 1.5)
                        {
                            FoxVars.foxanimator.speed = 1.5f;
                        }
                        FoxVars.foxanimator.SetFloat("Vertical", FoxVars.rangeToTarget / 3);
                    }
                    else if (FoxVars.rangeToTarget < 12f && FoxVars.rangeToTarget >= 5f)
                    {
                        if (FoxVars.foxanimator.speed != 1.3)
                        {
                            FoxVars.foxanimator.speed = 1.3f;
                        }
                        FoxVars.foxanimator.SetFloat("Vertical", FoxVars.rangeToTarget / 3);
                    }
                    else if (FoxVars.rangeToTarget < 5f && FoxVars.rangeToTarget >= 0f)
                    {
                        if (FoxVars.foxanimator.speed != 1.3)
                        {
                            FoxVars.foxanimator.speed = 1.3f;
                        }
                        FoxVars.foxanimator.SetFloat("Vertical", 1.2f);
                    }

                    

                    FoxVars._velocity.y += FoxVars.Gravity * Time.deltaTime;
                    FoxVars.foxController.Move(FoxVars._velocity * Time.deltaTime);
                }
                else
                {
                    // MelonLogger.Msg("Random: " + FoxVars.rabbidCatchRand);
                    if(FoxVars.foxEating==false && FoxVars.foundFood == true && FoxVars.targetHitObject.activeSelf && FoxVars.targetHitObject != null)
                    {                        
                        FoxVars.foxEating = true;

                        FoxVars.foxanimator.SetFloat("Vertical", 0f);
                        FoxVars.foxanimator.speed = 1.0f;
                        MelonLogger.Msg("Fox is eating ...");
                        FoxVars.foxanimator.Play("Eat Start", -1, 0f);
                        food.m_NonInteractive = true;

                        FoxVars.eatTimer = 0;
                        //FoxVars.foxanimator.SetBool("Action", true);
                        //FoxVars.foxanimator.SetBool("Swim", false);
                        //FoxVars.foxanimator.SetInteger("IDAction", 2);
                    }
                    else
                    {
                        if(FoxVars.eatTimer<8f)
                        {
                            FoxVars.eatTimer += Time.deltaTime;
                            
                            
                        }          
                        else
                        {
                            FoxVars.foxanimator.Play("Eat End", -1, 0f);
                            
                            Destroy(FoxVars.targetHitObject);
                            FoxVars.eatTimer = 0;
                            FoxVars.foundFood = false;
                            FoxVars.foxEating = false;
                            //FoxVars.foxShouldFollowPlayer = false;
                            FoxVars.foxShouldFollowSomething = false;
                        }
                    }
                }
                    
            }

        }

    }
}
