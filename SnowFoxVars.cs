using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class FoxVars : MelonMod
    {     
        // Assetbundles

        public static Animator foxanimator;
        public static GameObject fox = null;
        public static GameObject foxasset;
        public static AssetBundle foxload;
        public static Rigidbody foxRigid;
        public static CharacterController foxController;
        public static CorvoPathFinder foxPathfinder;
		public static Vector3[] foxWaypoints;
		public static Vector3 PFTarget;
		public static float rangeToPFTarget;
		public static float pathTimer = 0;
		public static string foxName = "the fox";
		public static bool foundPath = false;

        public static GameObject ringasset_red;
        public static GameObject ringred;
        public static GameObject ringasset_green;
        public static GameObject ringgreen;
        public static GameObject ringasset_blue;
        public static GameObject ringblue;
        public static GameObject ringasset_white;
        public static GameObject ringwhite;

        public static GameObject twinkleasset_red;
        public static GameObject twinklered;
        public static GameObject twinkleasset_blue;
        public static GameObject twinkleblue;
        public static GameObject twinkleasset_green;
        public static GameObject twinklegreen;
        public static GameObject twinkleasset_white;
        public static GameObject twinklewhite;

        public static int loadedLevel = 0;

        public static GameObject furShader = null;
        public static GameObject furShaderAsset;
        public static GameObject furShaderObj;
        public static AssetBundle furShaderLoad;
        public static Material furShaderMat;

        public static Material foxMaterial;
        public static Texture2D foxTexture;
        public static Material foxOldMaterial;
        public static SkinnedMeshRenderer foxRenderer;
        public static SkinnedMeshRenderer foxRendererAurora;
        public static Material foxRendererAuroraMaterial;

        public static Color foxFurColor;
        public static Color foxAuroraPatternColor;
        public static Vector4 foxAuroraEmissionColor;
        public static float foxAuroraLightIntensity;
        public static float foxAuroraLightRange;
        public static GameObject foxLight;
        public static Light foxLightComp;

        public static float Gravity = -9.81f;
        public static Vector3 _velocity;

        public static bool showCrosshair = false;
        public static GameObject SettingsBackgroundSprite;
        public static GameObject SettingsBackgroundMark;
        public static GameObject SettingsBackgroundVignette;

        //(0.01f, 0.07f, 0.523f)
        public static float bunnyX=0.01f;
        public static float bunnyY=-0.05f;
        public static float bunnyZ=0.523f;

        public static Rigidbody lanternRigid;
        public static float lanternX = 0.00f;
        public static float lanternY = 0.00f;
        public static float lanternZ = 0.00f;

        public static float rangeToTarget;
        public static float rangeToTargetTemp = 0.0f;
        public static float angleToTarget;
        public static float angleToTargetTree;
        public static float angleToTargetTreeTemp = 0.0f;
        public static float angleVelocity = 0.0f;
        public static float speedVelocity = 1.0f;
        public static float vertSpeed = 1.0f;
        public static float vertSpeedTemp = 1.0f;

        public static bool foundRabbit = false;
        public static bool foundItem = false;
        public static bool foundBed = false;
        public static bool foundFood = false;
        public static bool rabbidStopped = false;
        public static bool rabbidKilled = false;
        public static bool rabbidEvaded = false;
        public static int rabbidCatchRand;
        public static bool carryRabbit = false;
        public static bool foxJumping = false;
        public static bool foxEating = false;
        public static GameObject whiteRabbit;
        public static GameObject carryLantern;

        // Standard control settings
        public static string ButtonOrderFoxToFollowPlayer = "M";
        public static string ButtonOrderFoxToTarget = "n";
        public static string ButtonTeleportFoxToTarget = "P";

        public static float rotationSpeed = 2; //speed of turning

        public static Transform rotTransform;
        public static Transform playerTransform; //current transform data of the player
        public static Transform targetTransform; //current target transform
        public static Transform targetHitTransform; //current target transform
        public static GameObject targetHitObject;
        public static Transform foxTransform; //current fox transform  

        public static Vector3 playerPosition; //current position of the player
        public static Vector3 _direction;
        public static Vector3 targetPlayerDir; // Player direction
        public static Vector3 foxForward; // Fox forward facing direction

        public static int idleRand;

        public static float timeToSpawn = 0.5f;
        public static bool timeToSpawnStarted = false;

        public static float timeToIdle = 12.0f;
        public static float idlechangeTime = 8f;

        public static float timeToSit = timeToIdle + 20f;
        public static float timeToLay = timeToSit + 20f;
        public static float timeToSleep = timeToLay + 20f;        

        public static int idleStateCounter = 0;

        public static bool foxSpawned = false;

        public static float eatTimer = 0.0f;
        public static PlayerAnimation.State tempPlayerState;
    

        public static float idleTimer = 0.0f;
        public static float idleChangeTimer = 0.0f;
        public static float idleStateChangeTimer = 0.0f;

        public static float foxSpawnTimer = 3.2f;
        public static float walkModeTimer = 0.0f;

        public static int loadedScene = 0;

        public static bool isLevelLoaded = false; // Is the scene loaded?
        public static bool isFoxLoaded = false; // Is the fox  loaded?

        public static bool foxShouldFollowPlayer = false;
        public static bool foxShouldFollowSomething = false;
        public static bool foxactive = false; // Is the fox instantiated?
        public static bool foxState_WalkingToTarget = false; // Fox follows player
        public static string foxState_Movement = "wait"; // idle / walk / run

        public Animation walkanim;

        public static float animTimer = 0f;

        public static GameObject targetsphere;
        public static Transform targetsphereTransform;
        public static Vector3 targetsphereLastPos;

        public static bool targetsphereActive = false;

        public static RaycastHit[] sphereTargethit;
        public static Transform sphereLastHit;
        public static GameObject sphereLastHitObj;
        public static Renderer sphererend;

        public static int sphereTargetObject;

        public static GameObject testSphere;
        public static Renderer testSphereRend;


        // IK stuff
        public static GameObject foxL_ElbowTarget;
        public static GameObject foxR_ElbowTarget;

        //IK settings front-left
        public static Vector3 foxFLUpperArm_OffsetRotation;
        public static Vector3 foxFLForeArm_OffsetRotation;
        public static Vector3 foxFLHand_OffsetRotation;
        
        public static bool foxFL_handMatchesTargetRotation;

        public static float foxFLAngle;
        public static float foxFLUpperArm_Length;
        public static float foxFLForearm_Length;
        public static float foxFLArm_Length;
        public static float foxFLTargetDistance;
        public static float foxFLAdjacent;

        //IK settings front-right
        public static Vector3 foxFRUpperArm_OffsetRotation;
        public static Vector3 foxFRForeArm_OffsetRotation;
        public static Vector3 foxFRHand_OffsetRotation;

        public static bool foxFR_handMatchesTargetRotation;

        public static float foxFRAngle;
        public static float foxFRUpperArm_Length;
        public static float foxFRForearm_Length;
        public static float foxFRArm_Length;
        public static float foxFRTargetDistance;
        public static float foxFRAdjacent;

        //IK settings back-left
        public static Vector3 foxRLUpperArm_OffsetRotation;
        public static Vector3 foxRLForeArm_OffsetRotation;
        public static Vector3 foxRLHand_OffsetRotation;

        public static bool foxRL_handMatchesTargetRotation;

        public static float foxRLAngle;
        public static float foxRLUpperArm_Length;
        public static float foxRLForearm_Length;
        public static float foxRLArm_Length;
        public static float foxRLTargetDistance;
        public static float foxRLAdjacent;

        //IK settings back-right
        public static Vector3 foxRRUpperArm_OffsetRotation;
        public static Vector3 foxRRForeArm_OffsetRotation;
        public static Vector3 foxRRHand_OffsetRotation;

        public static bool foxRR_handMatchesTargetRotation;

        public static float foxRRAngle;
        public static float foxRRUpperArm_Length;
        public static float foxRRForearm_Length;
        public static float foxRRArm_Length;
        public static float foxRRTargetDistance;
        public static float foxRRAdjacent;


        // IK Limbs
        public static Transform foxJaw;
        public static Transform foxHead;
        public static Transform foxNeck;

        public static Transform foxFLClavice;
        public static Transform foxFLUpperArm;
        public static Transform foxFLForeArm;
        public static Transform foxFLHand;
        public static Transform foxFLHandTrack;

        public static Transform foxFRClavice;
        public static Transform foxFRUpperArm;
        public static Transform foxFRForeArm;
        public static Transform foxFRHand;
        public static Transform foxFRHandTrack;

        public static Transform foxRLClavice;
        public static Transform foxRLUpperArm;
        public static Transform foxRLForeArm;
        public static Transform foxRLHand;
        public static Transform foxRLHandTrack;

        public static Transform foxRRClavice;
        public static Transform foxRRUpperArm;
        public static Transform foxRRForeArm;
        public static Transform foxRRHand;
        public static Transform foxRRHandTrack;


        // Custom animations
        public static bool petStand = false;
    }
}
