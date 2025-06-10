using UnityEngine;
using System.Reflection;
using System.IO;
using MelonLoader;

namespace Zoom_Lens
{
    public class Assets
    {
        private static AssetBundle _assetBundle;
        public static GameObject Slider;
        
        public static void LoadAssets()
        {//https://github.com/ddakebono/BTKSASelfPortrait/blob/master/BTKSASelfPortrait.cs
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Zoom_Lens.slider"))
            {
                if (assetStream != null)
                {
                    MelonLogger.Msg("Zoom Lens assets fetched");
                    
                    using var tempStream = new MemoryStream((int) assetStream.Length);
                    assetStream.CopyTo(tempStream);

                    _assetBundle = AssetBundle.LoadFromMemory(tempStream.ToArray(), 0);
                    _assetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                }
            }

            if (_assetBundle != null)
            {
                Slider = _assetBundle.LoadAsset<GameObject>("ZoomCanvas");
                MelonLogger.Msg("Found prefab");
                Slider.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            }

            if (Slider != null)
            {
                MelonLogger.Msg("Zoom Lens assets have been loaded!");
            }
            else
            {
                MelonLogger.Error("Zoom Lens assets failed to load!");
            }

        }
    }
}