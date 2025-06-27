using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using CaputillaMelonLoader;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

// commit
namespace CapuchinCosmetics
{
    public class Plugin : MonoBehaviour
    {

        private void Start()
        {
            CaputillaMelonLoader.CaputillaHub.OnGameInitialized += Init;
        }

        void Init()
        {
            LoadCosmetics();
            WardrobeHandler.Init();
            ShopWardrobeHandler.Init();
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
    }

    public class CustomCosmetic
    {
        public GameObject CosmeticObj;
        public CapuCosmeticsMetadata CosmeticMeta;
    }
}