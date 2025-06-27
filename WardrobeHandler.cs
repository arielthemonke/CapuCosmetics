using System;
using CaputillaMelonLoader.Utils;
using Il2CppSystem.Collections.Generic;
using Il2CppTMPro;
using UnityEngine;

namespace CapuchinCosmetics
{
    public class WardrobeHandler : MonoBehaviour
    {
        private static GameObject Stump;
        private static GameObject Wardrobe;
        private static GameObject WardrobeButtons;
        private static GameObject HeadStands;
        private static GameObject TypesOfCosmetics;
        public static GameObject enabledisable;
        private static GameObject[] enabledisablearray = new GameObject[3];
        public static List<GameObject> newEnableDisable = new List<GameObject>();
        public static GameObject DynamicParent1;
        public static GameObject DynamicParent2;
        public static GameObject DynamicParent3;
        private static GameObject ModdedButton;
        private static GameObject ModdedButtonChild;
        public static bool enabledModMode;
        private static GameObject thingy1;
        private static GameObject thingy2;
        private static GameObject thingy3;
        private static int currentPage;
        private static GameObject nextback;
        public static GameObject nextPage;
        public static GameObject prevPage;
        private static Transform[] displaySlots = new Transform[3];
        private static GameObject[] displayedCosmetics = new GameObject[3];
        private static GameObject motd;
        private static TextMeshPro motdtext;

        public static void Init()
        {
            Stump = GameObject.Find("Global/Levels/ObjectNotInMaps/Stump");
            Wardrobe = Stump.transform.Find("WardorbeStump").gameObject;
            WardrobeButtons = Wardrobe.transform.Find("Buttons").gameObject;
            try
            {
                nextback = WardrobeButtons.transform.GetChild(2).gameObject;
            }
            catch (Exception e)
            {
                Debug.Log($"[CapuCosmetics] nextback: {e.Message}");
            }
            Debug.Log(nextback);
            nextPage = nextback.transform.Find("next").gameObject;
            Debug.Log(nextPage);
            prevPage = nextback.transform.Find("back").gameObject;
            Debug.Log(prevPage);
            nextPage.AddComponent<CustomPageSwitch>().mode = 0;
            prevPage.AddComponent<CustomPageSwitch>().mode = 1;
            enabledisable = WardrobeButtons.transform.GetChild(0).gameObject;
            enabledisablearray[0] = enabledisable.transform.GetChild(0).gameObject;
            enabledisablearray[1] = enabledisable.transform.GetChild(1).gameObject;
            enabledisablearray[2] = enabledisable.transform.GetChild(2).gameObject;
            Debug.Log(enabledisable);
            HeadStands = Wardrobe.transform.Find("Head Stands").gameObject;
            TypesOfCosmetics = WardrobeButtons.transform.Find("types of cosmetics").gameObject;
            DynamicParent1 = HeadStands.transform.Find("Dynamic Parent 1").gameObject;
            DynamicParent2 = HeadStands.transform.Find("Dynamic Parent 2").gameObject;
            DynamicParent3 = HeadStands.transform.Find("Dynamic Parent 3").gameObject;
            ModdedButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ModdedButton.GetComponent<Renderer>().material.color = Color.gray;
            ModdedButton.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
            ModdedButton.AddComponent<ButtonHandler>();
            ModdedButton.AddComponent<BoxCollider>().isTrigger = true;
            ModdedButton.transform.localPosition = new Vector3(-0.1951f, 0.5555f, 0.6786f);
            ModdedButton.transform.localScale = new Vector3(0.0724f, 0.0724f, 0.0724f);
            ModdedButtonChild = new GameObject("TEXTOFBUTTON");
            ModdedButtonChild.transform.localPosition = new Vector3(-0.5011f, -0.7636f, -2.2464f);
            ModdedButtonChild.transform.localScale = new Vector3(0.2727f, 0.3682f, -7.2909f);
            ModdedButtonChild.transform.localRotation = Quaternion.Euler(0, 90, 0);
            var tmpro = ModdedButtonChild.AddComponent<TextMeshPro>();
            tmpro.text = "MODDED";
            tmpro.fontSize = 8f;
            ModdedButtonChild.transform.SetParent(ModdedButton.transform, false);
            ModdedButton.transform.SetParent(TypesOfCosmetics.transform, false);
            displaySlots[0] = new GameObject("DisplaySlot1").transform;
            displaySlots[0].SetParent(HeadStands.transform, false);
            displaySlots[0].localPosition = new Vector3(-0.0282f, 0.358f, 0.2962f);
            displaySlots[0].localRotation = DynamicParent1.transform.localRotation;

            displaySlots[1] = new GameObject("DisplaySlot2").transform;
            displaySlots[1].SetParent(HeadStands.transform, false);
            displaySlots[1].localPosition = new Vector3(-0.0261f, 0.358f, -0.0095f);
            displaySlots[1].localRotation = DynamicParent2.transform.localRotation;

            displaySlots[2] = new GameObject("DisplaySlot3").transform;
            displaySlots[2].SetParent(HeadStands.transform, false);
            displaySlots[2].localPosition = new Vector3(-0.0332f, 0.358f, -0.3561f);
            displaySlots[2].localRotation = DynamicParent3.transform.localRotation;
        }
        
        public static void DisplayCosmetics()
        {
            for (int i = 0; i < displaySlots.Length; i++)
            {
                if (displayedCosmetics[i] != null)
                {
                    Destroy(displayedCosmetics[i]);
                }

                int cosmeticIndex = currentPage * 3 + i;

                if (cosmeticIndex < CosmeticLoader.Cosmetics.Count)
                {
                    CustomCosmetic cosmetic = CosmeticLoader.Cosmetics[cosmeticIndex];

                    GameObject preview = Instantiate(cosmetic.CosmeticObj, displaySlots[i]);
                    preview.transform.localPosition = Vector3.zero;
                    preview.transform.localRotation = Quaternion.identity;
                    preview.transform.localScale = Vector3.one * 0.2f;
                    preview.AddComponent<CustomEnableDisable>().toEnable = cosmetic;
                    preview.SetActive(true);

                    foreach (var collider in preview.GetComponentsInChildren<Collider>())
                        collider.enabled = false;

                    displayedCosmetics[i] = preview;
                }
                else
                {
                    displayedCosmetics[i] = null;
                }
            }
        }

        public static void NextPage()
        {
            if ((currentPage + 1) * 3 < CosmeticLoader.Cosmetics.Count)
            {
                currentPage++;
                DisplayCosmetics();
            }
        }

        public static void PrevPage()
        {
            if (currentPage > 0)
            {
                currentPage--;
                DisplayCosmetics();
            }
        }
    }
}