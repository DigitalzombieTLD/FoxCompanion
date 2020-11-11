using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxInstanceMain : MelonMod
    {
        public static void SnowFoxInstanceLoad()
        {
            // Instantiate fox in scene
                MelonLogger.Log("Loading fox ...");

                // load fox asset in game
                FoxVars.foxasset = FoxVars.foxload.LoadAsset<GameObject>("Fox Snow");
                //MelonLogger.Log("Asset: " + FoxVars.foxasset);

                FoxVars.fox = GameObject.Instantiate(FoxVars.foxasset);
                //MelonLogger.Log("Instance: " + FoxVars.fox);
                FoxVars.foxanimator = FoxVars.fox.GetComponentInChildren<Animator>();
                //MelonLogger.Log("Animator: " + FoxVars.foxanimator);
                FoxVars.foxRigid = FoxVars.fox.GetComponent<Rigidbody>();
                //MelonLogger.Log("Rigidbody: " + FoxVars.foxRigid);
                FoxVars.fox.AddComponent<CharacterController>();
                FoxVars.foxController = FoxVars.fox.GetComponent<CharacterController>();
                //MelonLogger.Log("Controller: " + FoxVars.foxController);

                // Fox controller settings
                FoxVars.foxController.center = new Vector3(0, 0.40f, 0.21f);
                FoxVars.foxController.radius = 0.14f;
                FoxVars.foxController.height = 0.60f;
                FoxVars.foxRigid.useGravity = true;
                FoxVars.foxRigid.isKinematic = false;

                FoxVars.foxanimator.updateMode = AnimatorUpdateMode.UnscaledTime;


                FoxVars.foxSpawnTimer = 0f;
                       
                
                // Get aurora skinned mesh
                GameObject aurora = FoxVars.fox.transform.Find("Meshes/Magic").gameObject;
                FoxVars.foxRendererAurora = aurora.GetComponent<SkinnedMeshRenderer>();
                FoxVars.foxRendererAurora.enabled = false;

                // Get normal skinned mesh and material
                GameObject foxmesh = FoxVars.fox.transform.Find("Meshes/Fox").gameObject;
                FoxVars.foxRenderer = foxmesh.GetComponent<SkinnedMeshRenderer>();
                FoxVars.foxMaterial = foxmesh.GetComponent<SkinnedMeshRenderer>().material;
                FoxVars.foxRenderer.receiveShadows = true;
                FoxVars.foxRenderer.castShadows = true;

                FoxVars.foxRigid.interpolation = RigidbodyInterpolation.Interpolate;

                FoxVars.targetsphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                FoxVars.targetsphere.transform.localScale = new Vector3(-0.3f, -0.3f, -0.3f);

                FoxVars.sphererend = FoxVars.targetsphere.GetComponent<Renderer>();
                //sphererend.material = new Material(Shader.Find("Specular"));
                FoxVars.sphererend.material = new Material(Shader.Find("Transparent/Diffuse"));
                FoxVars.sphererend.material.color = new Color(1.0f, 0.1f, 0.1f, 0.4f);

                Collider targetsphereCollider;
                targetsphereCollider = FoxVars.targetsphere.GetComponent<Collider>();
                targetsphereCollider.enabled = false;

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
               
             
                FoxVars.foxRenderer.material.mainTexture = FoxVars.foxTexture;

                FoxVars.foxFurColor = new Color(Settings.options.settingFoxFurColorR, Settings.options.settingFoxFurColorG, Settings.options.settingFoxFurColorB, 1f);
                FoxVars.foxAuroraPatternColor = new Color(Settings.options.settingFoxAuroraColorR, Settings.options.settingFoxAuroraColorG, Settings.options.settingFoxAuroraColorB, 1f);

                // Fur color
                FoxVars.foxRenderer.material.SetColor("_Color", FoxVars.foxFurColor);

                // Aurora pattern color
                FoxVars.foxRendererAurora.material.SetColor("_EmissionColor", FoxVars.foxAuroraPatternColor);
                FoxVars.foxTexture.Apply();

                // Initiate standard 
                FoxVars.foxanimator.SetBool("Stand", true);
                FoxVars.foxanimator.SetInteger("IDInt", 2);
                FoxVars.foxanimator.SetLayerWeight(FoxVars.foxanimator.GetLayerIndex("Fox"), 1);


                if (Settings.options.settingAutoFollow == true)
                {
                    MelonLogger.Log("Autofollow enabled");
                    FoxVars.foxShouldFollowSomething = false;
                    FoxVars.foxShouldFollowPlayer = true;
                }
                else
                {
                    MelonLogger.Log("Autofollow disabled");
                    FoxVars.foxShouldFollowSomething = false;
                    FoxVars.foxShouldFollowPlayer = false;
                }

                MelonLogger.Log("Snow fox is instantiated");
                FoxVars.foxSpawned = true;
                FoxVars.foxactive = true;
                FoxVars.isLevelLoaded = true;
            }      
    }
}