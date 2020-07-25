using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class FoxVars : MelonMod
    {     
        public static Animator foxanimator;
        public static GameObject fox;
        public static GameObject foxasset;
        public static AssetBundle foxload;
        public static Rigidbody foxRigid;
        public static CharacterController foxController;

        public static Material foxMaterial;
        public static Material foxOldMaterial;
        public static SkinnedMeshRenderer foxRendererAurora;

        public static float Gravity = -9.81f;
        public static Vector3 _velocity;

        public static bool showCrosshair = false;

        public static float rangeToTarget;
        public static float rangeToTargetTemp = 0.0f;
        public static float angleToTarget;
        public static float angleToTargetTree;
        public static float angleToTargetTreeTemp = 0.0f;
        public static float angleVelocity = 0.0f;
        public static float vertSpeed = 1.0f;

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

        public static float timeToSpawn = 3f;

        public static float timeToIdle = 12.0f;
        public static float idlechangeTime = 8f;

        public static float timeToSit = timeToIdle + 20f;
        public static float timeToLay = timeToSit + 20f;
        public static float timeToSleep = timeToLay + 20f;        

        public static int idleStateCounter = 0;

        public static bool foxSpawned = false;

        public static float idleTimer = 0.0f;
        public static float idleChangeTimer = 0.0f;
        public static float idleStateChangeTimer = 0.0f;

        public static float foxSpawnTimer = 0.0f;
        public static float walkModeTimer = 0.0f;
        


        public static bool isLevelLoaded = false; // Is the scene loaded?

        public static bool foxShouldFollowPlayer = false;
        public static bool foxShouldFollowSomething = false;
        public static bool foxactive = false; // Is the fox instantiated?
        public static bool foxState_WalkingToTarget = false; // Fox follows player
        public static string foxState_Movement = "wait"; // idle / walk / run

        public Animation walkanim;
    }
}
