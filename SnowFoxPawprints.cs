using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;
using MelonLoader;
using Harmony;

namespace FoxCompanion
{
    public class SnowFoxPawPrintsMain : MelonMod
    {
        public static FootstepTrail foxTrail;
        public static int m_LastFootstepTick;
        public static bool m_LastFootstepFront;
        public static bool m_LastFootstepLeft;
        public static int m_NextRecentFootPrintsIndex = 0;
        public static Vector3[] m_RecentFootPrints = new Vector3[8];
       

        public static void initPaws()
        {
            //FoxVars.foxFLHandTrack = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;
            //FoxVars.foxFRHandTrack = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;
            //FoxVars.foxRLHandTrack = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform; // L Track
            //FoxVars.foxRRHandTrack = FoxVars.foxTransform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.transform; // R Track

            //print("GetChild(0): " + GameObject.Find("L Foot").name);
            FoxVars.foxFLHandTrack = GameObject.Find("L Hand").transform;
            FoxVars.foxFRHandTrack = GameObject.Find("R Hand").transform;
            FoxVars.foxRLHandTrack = GameObject.Find("L Foot").transform;
            FoxVars.foxRRHandTrack = GameObject.Find("R Foot").transform;

            //FootstepTrailType trailType = FootstepTrailType.AiTransient;
            //SnowImprintType imprintType = SnowImprintType.WolfFootprint;  //WolfFootprint RabbitFootprint

            //foxTrail = new FootstepTrail(trailType, imprintType);
            //GameManager.GetFootstepTrailManager().AddFootstepTrail(foxTrail, true);
        }

        public static void leavePaw(Vector3 position, bool isFront, bool isLeft)
        {
            //Vector3 position = FoxVars.foxFLHandTrack.position;
            

			/*
            float num = 0.5f;
            position.y += num;
            RaycastHit raycastHit;
            bool flag = Physics.Raycast(position, Vector3.down, out raycastHit, float.PositiveInfinity, Utils.m_PhysicalCollisionLayerMask);
            if (!flag || raycastHit.collider == null)
            {
                return;
            }
            if (PositionOverlapsRecentFootprints(raycastHit.point))
            {
                return;
            }
            if (GameManager.GetDynamicDecalsManager().CanCreateFootPrintsOnMaterial(raycastHit.collider.gameObject.tag) && SnowPatchManager.m_Active && (m_LastFootstepTick != Time.frameCount || m_LastFootstepFront != isFront || m_LastFootstepLeft != isLeft))
            {
                m_LastFootstepTick = Time.frameCount;
                m_LastFootstepFront = isFront;
                m_LastFootstepLeft = isLeft;
                m_RecentFootPrints[m_NextRecentFootPrintsIndex % 8] = raycastHit.point;
                m_NextRecentFootPrintsIndex++;
                foxTrail.AddFootstep(position, raycastHit.point, raycastHit.normal, FoxVars.foxTransform.rotation.eulerAngles.y, isLeft, (!isFront) ? 1 : 0, 0f);
            }
			*/
        }

        public static bool PositionOverlapsRecentFootprints(Vector3 pos)
        {
            float num = 0.22f;
           
            int num2 = Mathf.Min(m_NextRecentFootPrintsIndex, 8);
            for (int i = 0; i < num2; i++)
            {
                float num3 = Vector3.Distance(pos, m_RecentFootPrints[i]);
                if (num3 < num)
                {
                    return true;
                }
            }
            return false;
        }

        /*[HarmonyPatch(typeof(PlayerManager), "StickPlayerToGround")]
        public class StickPlayerToGroundPatcher
        {
            public static void Postfix()
            {
                if (FoxVars.loadedScene >= 6 && FoxVars.fox == null)
                {
                    SnowFoxInstanceMain.SnowFoxInstanceLoad();
                    FoxVars.timeToSpawnStarted = false;
                    //print("After load and teleport");       
                    SnowFoxTeleportFoxMain.TeleportFoxToTarget(GameManager.GetPlayerTransform());
                }

                if (FoxVars.loadedScene >= 6 && FoxVars.fox != null)
                {
                    //FoxVars.timeToSpawnStarted = true;
                    SnowFoxTeleportFoxMain.TeleportFoxToTarget(GameManager.GetPlayerTransform());
                    //print("After load and teleport");
                }
            }
        }*/

        /*[HarmonyPatch(typeof(PlayerManager), "TeleportPlayer")]
        public class TeleportPlayerPatcher
        {
            public static void Postfix()
            {
                if (FoxVars.loadedScene >= 6 && FoxVars.fox == null)
                {
                    SnowFoxInstanceMain.SnowFoxInstanceLoad();
                    FoxVars.timeToSpawnStarted = false;
                    //print("After load and teleport");       
                    SnowFoxTeleportFoxMain.TeleportFoxToTarget(GameManager.GetPlayerTransform());
                }

                if (FoxVars.loadedScene >= 6 && FoxVars.fox != null)
                {
                    //FoxVars.timeToSpawnStarted = true;
                    SnowFoxTeleportFoxMain.TeleportFoxToTarget(GameManager.GetPlayerTransform());
                    //print("After load and teleport");
                }
            }
        }*/
    }
}