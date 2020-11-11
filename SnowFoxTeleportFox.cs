using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxTeleportFoxMain : MelonMod
    {
        public static void TeleportFoxToTarget(Transform target)
        {
            //FoxVars.foxShouldFollowPlayer = false;
            //FoxVars.foxShouldFollowSomething = false;
            
            FoxVars.fox.transform.position = new Vector3(target.position.x, target.position.y + 0.3f, target.position.z);
            MelonLogger.Log("Teleport fox to Player");
        }
    }
}