using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxFetchItemMain : MelonMod
    {
        public static void SnowFoxFetchItem()
        {
            if ((FoxVars.foundItem == true) && FoxVars.targetTransform != null)
            {
                FoxVars.rangeToTarget = Vector3.Distance(FoxVars.foxTransform.position, FoxVars.targetTransform.position);

                if (FoxVars.rangeToTarget >= 1.0f)
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
                    MelonLogger.Log("Got the item!");

                    GameObject foxJaw;
                    foxJaw = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject;
                    FoxVars.sphereLastHitObj.transform.SetParent(foxJaw.transform, true);
                    FoxVars.sphereTargetObject = 0;

                    FoxVars.sphereLastHitObj.transform.localPosition = new Vector3(0f, 0f, 0f); //new Vector3(0.01f, 0.05f, 0.523f);
                    FoxVars.sphereLastHitObj.transform.localRotation = Quaternion.Euler(0, 0, 0.0f);
                    //child.transform.SetParent(null);
                    FoxVars.foxShouldFollowPlayer = true;
                    FoxVars.foxShouldFollowSomething = false;
                }
            }

        }
    }
}