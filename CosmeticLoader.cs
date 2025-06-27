using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppLocomotion;
using Newtonsoft.Json;
using UnityEngine;

namespace CapuchinCosmetics
{
    public class CosmeticLoader : MonoBehaviour
    {
        public static List<CustomCosmetic> Cosmetics = new List<CustomCosmetic>();
        public static void LoadCosmetic(string filePath)
        {
            string extractPath = Path.Combine(Path.GetTempPath(), "Capushirt_TWMP");
            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true);
            
            ZipFile.ExtractToDirectory(filePath, extractPath);
            
            string metadataJson = File.ReadAllText(Path.Combine(extractPath, "metadata.json"));
            CapuCosmeticsMetadata meta = JsonConvert.DeserializeObject<CapuCosmeticsMetadata>(metadataJson);
            Debug.Log($"[CapuCosmetics] Loading Cosmetic: {meta.name} by {meta.author}");
            
            string[] files = Directory.GetFiles(extractPath);
            string bundlePath = System.Array.Find(files, f => !f.EndsWith(".json") && !f.EndsWith(".manifest"));

            if (bundlePath == null)
            {
                Debug.Log("[CapuCosmetics] This cosmetic is corrupted");
                return;
            }
            
            AssetBundle bundle = LoadAssetBundle(bundlePath);
            if (bundle == null)
            {
                Debug.Log("[CapuCosmetics] broken mod or cosmetic or something idk");
                return;
            }
            
            // I call this insane debugging
            string[] assetNames = bundle.GetAllAssetNames();
            Debug.Log($"[CapuCosmetics] Available assets: {string.Join(", ", assetNames)}");
    
            if (assetNames.Length == 0)
            {
                Debug.Log("[CapuCosmetics] No assets found in bundle");
                return;
            }
    
            GameObject prefab = bundle.LoadAsset<GameObject>(assetNames[0]);
            Debug.Log($"[CapuCosmetics] Prefab loaded: {prefab != null}");
            if (prefab == null)
            {
                Debug.Log("[CapuCosmetics] it no worky");
            }
            
            GameObject cosmetic = Instantiate(prefab);
            cosmetic.name = meta.name;
            if (meta.syncToLeftHand)
            {
                cosmetic.transform.SetParent(Player.Instance.LeftHand.transform, false);
                cosmetic.transform.localPosition = Vector3.zero;
                cosmetic.transform.localRotation = Quaternion.identity;
            }
            else if (meta.syncToRightHand)
            {
                cosmetic.transform.SetParent(Player.Instance.RightHand.transform, false);
            }
            else
            {
                cosmetic.transform.SetParent(Player.Instance.playerCam.gameObject.transform, false);
            }

            var cosmeticRenderer = cosmetic.GetComponent<Renderer>();
            if (cosmeticRenderer != null)
            {
                cosmeticRenderer.material.shader = Shader.Find("Shader Graphs/ShadedPiss");
            }
            
            foreach (var renderer in cosmetic.GetComponentsInChildren<Renderer>())
            {
                renderer.material.shader = Shader.Find("Shader Graphs/ShadedPiss");
            }

            foreach (var col in cosmetic.GetComponentsInChildren<Collider>())
            {
                col.enabled = false;
            }

            CustomCosmetic ThisCosmetic = new CustomCosmetic
            {
                CosmeticObj = cosmetic,
                CosmeticMeta = meta,
            };
            Cosmetics.Add(ThisCosmetic);
            cosmetic.SetActive(false);
            bundle.Unload(false);
        }
        
        public static AssetBundle LoadAssetBundle(string filePath)
        {
            byte[] bundleData = File.ReadAllBytes(filePath);
            Il2CppStructArray<byte> bundleBytes = new Il2CppStructArray<byte>(bundleData);
            AssetBundle assetBundle = AssetBundle.LoadFromMemory(bundleBytes);
            return assetBundle;
        }
    }
}

[System.Serializable]
public class CapuCosmeticsMetadata
{
    public string name;
    public string author;
    public int version;
    public string description;
    public bool syncToRightHand;
    public bool syncToLeftHand;
}