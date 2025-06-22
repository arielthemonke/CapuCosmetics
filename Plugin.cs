using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using CapuchinCosmetics.Patches;
using UnityEngine;
using Caputilla;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Locomotion;

// commit
namespace CapuchinCosmetics
{
    [BepInPlugin(ModInfo.GUID, ModInfo.Name, ModInfo.Version)]
    public class Init : BasePlugin
    {
        // I wouldn't recommend modifying this Init class as it can be hard to understand for new modders.
        // If you're experienced then probably ignore these comments as they're mostly here to guide new modders.
        public static Init instance;
        public Harmony harmonyInstance;

        public override void Load()
        {
            harmonyInstance = HarmonyPatcher.Patch(ModInfo.GUID);
            instance = this;

            // If and only IF you're making custom MonoBehaviour's do this:
            // ClassInjector.RegisterTypeInIl2Cpp<CustomMonoBehaviour>();
            // If you don't do this, your MonoBehaviour will not be recognized by the game.

            ClassInjector.RegisterTypeInIl2Cpp<CosmeticLoader>();
            ClassInjector.RegisterTypeInIl2Cpp<WardrobeHandler>();
            ClassInjector.RegisterTypeInIl2Cpp<ButtonHandler>();
            ClassInjector.RegisterTypeInIl2Cpp<CustomEnableDisable>();
            ClassInjector.RegisterTypeInIl2Cpp<CustomPageSwitch>();
            AddComponent<Plugin>();
        }

        public override bool Unload()
        {
            if (harmonyInstance != null)
                HarmonyPatcher.Unpatch(harmonyInstance);

            return true;
        }
    }

    public class Plugin : MonoBehaviour
    {

        private void Start()
        {
            CaputillaManager.Instance.OnGameInitialized += Init;
        }

        void Init()
        {
            LoadCosmetics();
            WardrobeHandler.Init();
        }

        void LoadCosmetics()
        {
            var assembly = Assembly.GetExecutingAssembly().Location;
            var assemblyPath = Path.GetDirectoryName(assembly);
            var cosmeticDir = Path.Combine(assemblyPath, "Cosmetics");
            Directory.CreateDirectory(cosmeticDir);
            string[] capucosmetics = Directory.GetFiles(cosmeticDir, "*.capucosmetic");
            foreach (string capucosmetic in capucosmetics)
            {
                CosmeticLoader.LoadCosmetic(capucosmetic);
            }
        }

        private void OnGUI()
        {
            foreach (var cosmeticThing in CosmeticLoader.Cosmetics)
            {
                if (GUILayout.Button(cosmeticThing.CosmeticMeta.name))
                {
                    if (cosmeticThing.CosmeticObj.active)
                    {
                        cosmeticThing.CosmeticObj.SetActive(false);
                    }
                    else
                    {
                        cosmeticThing.CosmeticObj.SetActive(true);
                    }
                }
            }
        }
    }

    public class CustomCosmetic
    {
        public GameObject CosmeticObj;
        public CapuCosmeticsMetadata CosmeticMeta;
    }
}