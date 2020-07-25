using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxInstanceMain : MelonMod
    {
        public static void SnowFoxInstanceLoad(int level)
        {
            // Instantiate fox in scene
            if (level >= 6)
            {
                // load fox asset in game
                FoxVars.foxasset = FoxVars.foxload.LoadAsset<GameObject>("Fox Snow");
                FoxVars.fox = GameObject.Instantiate(FoxVars.foxasset);
                FoxVars.foxanimator = FoxVars.fox.GetComponentInChildren<Animator>();
                FoxVars.foxRigid = FoxVars.fox.GetComponent<Rigidbody>();
                FoxVars.fox.AddComponent<CharacterController>();
                FoxVars.foxController = FoxVars.fox.GetComponent<CharacterController>();

                // Fox controller settings
                FoxVars.foxController.center = new Vector3(0, 0.40f, 0.21f);
                FoxVars.foxController.radius = 0.14f;
                FoxVars.foxController.height = 0.60f;
                FoxVars.foxRigid.useGravity = true;
                FoxVars.foxRigid.isKinematic = true;

                //MelonModLogger.Log("Snow fox is instantiated");
                FoxVars.foxactive = true;
                FoxVars.foxSpawnTimer = 0f;
                FoxVars.isLevelLoaded = true;                

                // Get aurora skinned mesh
                GameObject aurora = FoxVars.fox.transform.Find("Meshes/Magic").gameObject;
                FoxVars.foxRendererAurora = aurora.GetComponent<SkinnedMeshRenderer>();
                FoxVars.foxRendererAurora.enabled = false;

                // Initiate standard 
                FoxVars.foxanimator.SetBool("Stand", true);
                FoxVars.foxanimator.SetInteger("IDInt", 2);
                FoxVars.foxanimator.SetLayerWeight(FoxVars.foxanimator.GetLayerIndex("Fox"), 1);
            }
            else
            {
                // Don't load fox if scene is not loaded
                FoxVars.isLevelLoaded = false;
                FoxVars.foxactive = false;
                FoxVars.foxSpawned = false;
            }
        }

        public static void SnowFoxInstanceUpdate()
        {
            // teleport fox to player some time after scene is loaded 
            if (FoxVars.foxactive == true && FoxVars.foxSpawnTimer > FoxVars.timeToSpawn && FoxVars.foxSpawned == false)
            {
                FoxVars.foxSpawned = true;
                FoxVars.foxSpawnTimer = 0f;
                FoxVars.foxState_WalkingToTarget = false;
                FoxVars.foxState_Movement = "idle";

                SnowFoxTeleportFoxMain.TeleportFoxToTarget(GameManager.GetPlayerTransform());     
            }
            else
            {
                if (FoxVars.isLevelLoaded == true && FoxVars.foxSpawned == false)
                {                   
                    // Advance timer till fox spawn in scene
                    FoxVars.foxSpawnTimer += Time.deltaTime;
                }
            }
        }
    }
}