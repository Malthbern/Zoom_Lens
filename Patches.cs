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
        public static void AttachLens()
        {
            Obj = GameObject.Instantiate(Assets.Slider, PortableCamera.Instance.gameObject.transform, false);
            Obj.transform.localPosition = new Vector3(65f, 0, 0);
            LensMain.ConnectZoom();
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(typeof(PortableCamera), "TimerStarted")]
        public static void TimerLock()
        {
            LensMain.LensLock(true);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PortableCamera), "PostImageTaken")]
        public static void TimerUnlock()
        {
            LensMain.LensLock(false);
        }
    }
}