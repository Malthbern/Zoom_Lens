using ABI_RC.Core.Savior;
using HarmonyLib;
using ABI_RC.Systems.Camera;
using UnityEngine;

namespace Zoom_Lens
{
    public class Patches
    {
        public static GameObject Obj = null;

        private static readonly Vector3 _vroffset = new Vector3(70f, 0, 0);
        private static readonly Vector3 _vrscale = new Vector3(.25f, .25f, .25f);
        
        private static readonly Vector3 _doffset = new Vector3(255f, 0, 0);
        private static readonly Vector3 _dscale = new Vector3(.75f, .75f, .75f);
        
        private static readonly Vector3 _dwoffset = new Vector3(70f, 0, 0);
        private static readonly Vector3 _dwscale = new Vector3(.2f, .2f, .2f);
        
        [HarmonyPostfix]
        [HarmonyPriority(Priority.HigherThanNormal)]
        [HarmonyPatch(typeof(PortableCamera), "Start")] 
        public static void AttachLens() // Get camera instance transform to connect our mod to the camera it's self
        {
            Obj = GameObject.Instantiate(Assets.Slider, PortableCamera.Instance.gameObject.transform, false);
            LensMain.ConnectZoom();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PortableCamera), "SetViewportPinMode")]
        public static bool SetZoomOffset(ref bool worldPin)
        {
            
             /*
             * I blame NAK for everything wrong in the world.
             * the camera has 3 potential scales all because he couldn't deal
             * with how the camera looked on desktop. and instead of just waiting for me
             * to rewrite the piece of shit he made this awful hack to make the camera
             * screenspace in desktop view.
             */

            switch (worldPin)
            {
                case true:
                    Obj.transform.localPosition = _dwoffset;
                    Obj.transform.localScale = _dwscale;
                    break;
                
                case false:
                    Obj.transform.localPosition = _doffset;
                    Obj.transform.localScale = _dscale;
                    break;
            }
            
            // Always prioritize VR offsets
            if (MetaPort.Instance.isUsingVr)
            {
                Obj.transform.localPosition = _vroffset;
                Obj.transform.localScale = _vrscale;
            }
            
            return true; //continue native code
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PortableCamera), "MakePhotoDelayed")]
        public static void MakePhotoDelayed() // Lock zoom lens when timer is active
        {
            LensMain.LensLock(true);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PortableCamera), "Capture")]
        public static void Capture() // Unlock zoom lens when timer is complete
        {
            LensMain.LensLock(false);
        }
    }
}