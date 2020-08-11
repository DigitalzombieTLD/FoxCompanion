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

                //MelonLogger.Log("Snow fox is instantiated");
                FoxVars.foxactive = true;
                FoxVars.foxSpawnTimer = 0f;
                FoxVars.isLevelLoaded = true;                

                // Get aurora skinned mesh
                GameObject aurora = FoxVars.fox.transform.Find("Meshes/Magic").gameObject;
                FoxVars.foxRendererAurora = aurora.GetComponent<SkinnedMeshRenderer>();
                FoxVars.foxRendererAurora.enabled = false;

                // Get normal skinned mesh and material
                GameObject foxmesh = FoxVars.fox.transform.Find("Meshes/Fox").gameObject;
                FoxVars.foxRenderer = foxmesh.GetComponent<SkinnedMeshRenderer>();
                FoxVars.foxMaterial = foxmesh.GetComponent<SkinnedMeshRenderer>().material;

                //FoxVars.foxMaterial.color = Color.red;

                /*[Choice("Snow", "Black", "Orange", "Mane", "Zerda", "Custom 1", "Custom 2", "Custom 3")]
                public int settingTexture = 0;*/
                byte[] img;

                switch (Settings.options.settingTexture)
                {
                    case 0: 
                        img = System.IO.File.ReadAllBytes("Mods\\foxtures\\snow.png");
                        break;
                    case 1: 
                        img = System.IO.File.ReadAllBytes("Mods\\foxtures\\black.png");
                        break;
                    case 2: 
                        img = System.IO.File.ReadAllBytes("Mods\\foxtures\\orange.png");
                        break;
                    case 3: 
                        img = System.IO.File.ReadAllBytes("Mods\\foxtures\\mane.png");
                        break;
                    case 4: 
                        img = System.IO.File.ReadAllBytes("Mods\\foxtures\\zerda.png");
                        break;
                    case 5: 
                        img = System.IO.File.ReadAllBytes("Mods\\foxtures\\custom1.png");
                        break;
                    case 6: 
                        img = System.IO.File.ReadAllBytes("Mods\\foxtures\\custom2.png");
                        break;
                    case 7: 
                        img = System.IO.File.ReadAllBytes("Mods\\foxtures\\custom3.png");
                        break;
                    default: 
                        img = System.IO.File.ReadAllBytes("Mods\\foxtures\\snow.png");
                        break;
                }

                FoxVars.foxTexture = new Texture2D(128, 64);
                //FoxVars.foxTexture.LoadImage(FoxVars.foxTexture, img);
                ImageConversion.LoadImage(FoxVars.foxTexture, img);
                FoxVars.foxTexture.Apply();
             
                FoxVars.foxRenderer.material.mainTexture = FoxVars.foxTexture;
                

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

                SnowFoxTeleportFoxMain.TeleportFoxToTarget(GameManager.GetPlayerTransform());

                if (Settings.options.settingAutoFollow == true)
                {
                    FoxVars.foxShouldFollowSomething = false;
                    FoxVars.foxShouldFollowPlayer = true;
                }
                else
                {
                    // FoxVars.foxState_Movement = "idle";
                    FoxVars.foxShouldFollowSomething = false;
                    FoxVars.foxShouldFollowPlayer = false;
                }
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