using HarmonyLib;
using ABI_RC.Systems.Camera;
using UnityEngine;
using UnityEngine.UI;

namespace Zoom_Lens
{
    public class Patches
    {
        public static GameObject Obj = null;
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PortableCamera), "Start")] 
        public static void AttachLens() // Get camera instance transform to connect our mod to the camera it's self
        {
            Obj = GameObject.Instantiate(Assets.Slider, PortableCamera.Instance.gameObject.transform, false);
            Obj.transform.localPosition = new Vector3(65f, 0, 0);
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