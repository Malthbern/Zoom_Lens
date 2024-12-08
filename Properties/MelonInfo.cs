using MelonLoader;
using System.Reflection;
using Zoom_Lens;

[assembly: AssemblyDescription(Zoom_Lens.BuildInfo.Description)]
[assembly: AssemblyCopyright("Created by " + Zoom_Lens.BuildInfo.Author)]
[assembly: AssemblyTrademark(Zoom_Lens.BuildInfo.Company)]
[assembly: MelonInfo(typeof(LensMain), Zoom_Lens.BuildInfo.Name, Zoom_Lens.BuildInfo.Version, Zoom_Lens.BuildInfo.Author, Zoom_Lens.BuildInfo.DownloadLink)]
[assembly: MelonColor(255, 252, 0, 177)]
[assembly: MelonAuthorColor(255, 0, 252, 177)]
[assembly: HarmonyDontPatchAll]