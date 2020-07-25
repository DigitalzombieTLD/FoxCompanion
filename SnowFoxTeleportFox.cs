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
            FoxVars.foxShouldFollowPlayer = false;
            FoxVars.foxShouldFollowSomething = false;

            FoxVars.fox.transform.position = target.position;
            MelonModLogger.Log("Teleport to Playertarget");
        }
    }
}