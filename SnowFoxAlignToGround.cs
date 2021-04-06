using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxAlignToGroundMain : MelonMod
    {
        public static void SnowFoxAlignToGround()
        {
            RaycastHit hit;
            int layerMask = 0;
            layerMask |= 1 << 26; // Character collider layer

            if (Physics.Raycast(FoxVars.foxTransform.position, -FoxVars.foxTransform.up, out hit, layerMask))
            {
                var slopeRotation = Quaternion.FromToRotation(FoxVars.foxTransform.up, hit.normal); 
                //slopeRotation = Quaternion.Euler(slopeRotation.eulerAngles.x, slopeRotation.eulerAngles.y, 0f);
                FoxVars.foxTransform.rotation = Quaternion.Slerp(FoxVars.foxTransform.rotation, slopeRotation * FoxVars.foxTransform.rotation, 1 * Time.deltaTime);


                //MelonLogger.Log("Align fox to ground: " + hit.transform.name);
            }
            
        }
    }
}