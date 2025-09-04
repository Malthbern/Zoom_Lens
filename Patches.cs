using HarmonyLib;
using ABI_RC.Systems.Camera;
using UnityEngine;

namespace Zoom_Lens
{
    public class Patches
    {
        public static GameObject Obj = null;

        private static readonly Vector3 _offset = new Vector3(65f, 0, 0);
        private static readonly Vector3 _nightlyoffset = new Vector3(180f, 0, 0);
        private static readonly Vector3 _nightlyscale = new Vector3(0.65f, 0.65f, 0.65f);
        
        [HarmonyPostfix]
        [HarmonyPriority(Priority.HigherThanNormal)]
        [HarmonyPatch(typeof(PortableCamera), "Start")] 
        public static void AttachLens() // Get camera instance transform to connect our mod to the camera it's self
        {
            Obj = GameObject.Instantiate(Assets.Slider, PortableCamera.Instance.gameObject.transform, false);
            
            if(LensMain.IsStable) // Tempoary fix for 2025r181
            {
                Obj.transform.localPosition = _offset;
            }
            else
            {
                Obj.transform.localPosition = _nightlyoffset;
                Obj.transform.localScale = _nightlyscale;
            }
            
            LensMain.ConnectZoom();
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PortableCamera), "MakePhotoDelayed")]
        public static void TimerLock() // Lock zoom lens when timer is active
        {
            LensMain.LensLock(true);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PortableCamera), "Capture")]
        public static void TimerUnlock() // Unlock zoom lens when timer is complete
        {
            LensMain.LensLock(false);
        }
    }
}