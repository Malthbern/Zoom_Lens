using System;
using System.Runtime.CompilerServices;
using ABI_RC.Systems.Camera;
using CurvedUI;
using MelonLoader;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;


namespace Zoom_Lens
{
    public static class BuildInfo
    {
        public const string Name = "Zoom Lens";
        public const string Description = "Adds a slider to the side of the camera to allow zooming without opening the advance settings.";
        public const string Author = "Malthbern";
        public const string Company = null;
        public const string Version = "0.1.3";
        public const string DownloadLink = "https://github.com/Malthbern/Zoom_Lens/releases";
    }
    
    public class LensMain : MelonMod
    {
        
        private static Slider _zoomSlider;
        
        public override void OnInitializeMelon()
        {
            Assets.LoadAssets();
            
            try
            {
                HarmonyInstance.PatchAll(typeof(Patches));
            }
            catch(Exception e)
            {
                MelonLogger.Error("Lens could not be attached:\n" + e);
            }
        }

        public static void ConnectZoom()
        {
            _zoomSlider = Patches.Obj.GetComponentInChildren<Slider>();
            MelonLogger.Msg("Click!");
            _zoomSlider.SetValueWithoutNotify(PortableCamera.Instance.cameraComponent.fieldOfView); // Set our slider to the camera's current FOV without triggering OnValueChanged()
            _zoomSlider.onValueChanged.AddListener(delegate {FOVChange();});
        }
        
        public static bool LensLock(bool newLockState)
        {
            _zoomSlider.interactable = !newLockState; //inverse because the argument name implies true IS NOT interactable
            return _zoomSlider.interactable;
        }
        private static void FOVChange()
        {
            PortableCamera.Instance.ChangeFov(_zoomSlider.value.ToInt());
        }
    }
}