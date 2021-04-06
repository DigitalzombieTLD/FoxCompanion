using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;

namespace FoxCompanion
{
    public class SnowFoxIKMain : MelonMod
    {
		public static void SnowFoxIKSetUp()
		{
            // IK stuff
            FoxVars.foxJaw = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject.transform;
            FoxVars.foxHead = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;
            FoxVars.foxNeck = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).gameObject.transform;

            FoxVars.foxFLClavice = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).gameObject.transform;
            FoxVars.foxFLUpperArm = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject.transform;
            FoxVars.foxFLForeArm = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.transform;
            FoxVars.foxFLHand = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;
            FoxVars.foxFLHandTrack = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;
 
            FoxVars.foxFRClavice = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject.transform;
            FoxVars.foxFRUpperArm = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).gameObject.transform;
            FoxVars.foxFRForeArm = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).gameObject.transform;
            FoxVars.foxFRHand = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;
            FoxVars.foxFRHandTrack = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;

            FoxVars.foxRLClavice = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform; // L Thigh
            FoxVars.foxRLUpperArm = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform; // L Calf
            FoxVars.foxRLForeArm = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform; // L HorseLink
            FoxVars.foxRLHand = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform; // L Foot
            FoxVars.foxRLHandTrack = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform; // L Track

            FoxVars.foxRRClavice = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.transform; // R Thigh
            FoxVars.foxRRUpperArm = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject.transform; // R Calf
            FoxVars.foxRRForeArm = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.transform; // R HorseLink
            FoxVars.foxRRHand = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.transform; // R Foot
            FoxVars.foxRRHandTrack = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform; // R Track

            FoxVars.foxL_ElbowTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            FoxVars.foxL_ElbowTarget.transform.localScale = new Vector3(-0.3f, -0.3f, -0.3f);
            FoxVars.foxL_ElbowTarget.GetComponent<Collider>().enabled = false;
            FoxVars.foxL_ElbowTarget.transform.SetParent(FoxVars.fox.transform);
            FoxVars.foxL_ElbowTarget.transform.localPosition = new Vector3(-0.216f, 0.154f, -1.89f);

            FoxVars.foxR_ElbowTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            FoxVars.foxR_ElbowTarget.transform.localScale = new Vector3(-0.3f, -0.3f, -0.3f);
            FoxVars.foxR_ElbowTarget.GetComponent<Collider>().enabled = false;
            FoxVars.foxR_ElbowTarget.transform.SetParent(FoxVars.fox.transform);
            FoxVars.foxR_ElbowTarget.transform.localPosition = new Vector3(0.216f, 0.154f, -1.89f);

            FoxVars.foxFLUpperArm_OffsetRotation = new Vector3(0f, 90f, 0f);
            FoxVars.foxFLForeArm_OffsetRotation = new Vector3(-90f, 0f, 90f);
            FoxVars.foxFLHand_OffsetRotation = new Vector3(0f, 0f, 0f);

            FoxVars.foxFRUpperArm_OffsetRotation = new Vector3(0f, 90f, 0f);
            FoxVars.foxFRForeArm_OffsetRotation = new Vector3(-90f, 0f, 90f);
            FoxVars.foxFRHand_OffsetRotation = new Vector3(0f, 0f, 0f);

            FoxVars.foxRLUpperArm_OffsetRotation = new Vector3(180f, 90f, 0f);
            FoxVars.foxRLForeArm_OffsetRotation = new Vector3(90f, 0f, -90f);
            FoxVars.foxRLHand_OffsetRotation = new Vector3(0f, 0f, 0f);

            FoxVars.foxRRUpperArm_OffsetRotation = new Vector3(180f, 90f, 0f);
            FoxVars.foxRRForeArm_OffsetRotation = new Vector3(90f, 0f, -90f);
            FoxVars.foxRRHand_OffsetRotation = new Vector3(0f, 0f, 0f);
        }

		public static void SnowFoxIK()
        {
            
        }
    }
}