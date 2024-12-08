using System;
using ABI_RC.Systems.Camera;
using CurvedUI;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;


namespace Zoom_Lens
{
    public static class BuildInfo
    {
        public const string Name = "Zoom Lens";
        public const string Description = "Adds a slider to the side of the camera to allow zooming without opening the advance settings.";
        public const string Author = "Malthbern";
        public const string Company = null;
        public const string Version = "0.1.1";
        public const string DownloadLink = "";
    }
    
    public class LensMain : MelonMod
    {
        
        public static Slider ZoomSlider;
        
        public override void OnInitializeMelon()
        {
            Assets.LoadAssets();
            
            try
            {
                HarmonyInstance.PatchAll(typeof(Patches));
            }
            catch(Exception e)
            {
                MelonLogger.Error("Lens could not be attached: " + e);
            }
        }

        public static void ConnectZoom()
        {
            ZoomSlider = Patches.Obj.GetComponentInChildren<Slider>();
            MelonLogger.Msg("Click!");
            ZoomSlider.onValueChanged.AddListener(delegate {FOVChange();});
        }
        
        public static void FOVChange()
        {
            PortableCamera.Instance.ChangeFov(ZoomSlider.value.ToInt());
        }
    }
}