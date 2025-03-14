﻿using System;
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
        public const string Version = "0.1.5";
        public const string DownloadLink = "https://github.com/Malthbern/Zoom_Lens/releases";
    }
    
    public class LensMain : MelonMod
    {
        
        private static Slider _zoomSlider;
        private static Text _fovText;
        private static Collider _zoomCollider;
        
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
            _zoomCollider = Patches.Obj.GetComponentInChildren<Collider>();
            _fovText = Patches.Obj.GetComponentInChildren<Text>();

            if (_zoomSlider == null)
            {
                MelonLogger.Error("Zoom slider not connected");
                return;
            }
            else if (_zoomCollider == null)
            {
                MelonLogger.Error("Zoom collider not connected");
                return;
            }
            else if (_fovText == null)
            {
                MelonLogger.Warning("FOV number not connected");
            }
            else
            {
                MelonLogger.Msg("Click!");
            }
            
            _zoomSlider.SetValueWithoutNotify(PortableCamera.Instance.cameraComponent.fieldOfView); // Set our slider to the camera's current FOV without triggering OnValueChanged()
            _fovText.text = PortableCamera.Instance.cameraComponent.fieldOfView.ToString();
            
            _zoomSlider.onValueChanged.AddListener(delegate {FOVChange();});
        }
        
        public static bool LensLock(bool newLockState)
        {
            _zoomSlider.interactable = !newLockState; // Inverse because the argument name implies true IS NOT interactable
            _zoomCollider.enabled = !newLockState; // Disable collider because CVR doesn't use Unity's eventsystem stack for UI input
            return _zoomSlider.interactable;
        }
        private static void FOVChange()
        {
            _fovText.text = _zoomSlider.value.ToString();
            PortableCamera.Instance.ChangeFov(_zoomSlider.value.ToInt());
        }
    }
}