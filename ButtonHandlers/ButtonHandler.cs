using System;
using UnityEngine;

namespace CapuchinCosmetics
{
    public class ButtonHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            WardrobeHandler.enabledModMode = !WardrobeHandler.enabledModMode;

            if (WardrobeHandler.enabledModMode)
            {
                WardrobeHandler.DynamicParent1.SetActive(false);
                WardrobeHandler.DynamicParent2.SetActive(false);
                WardrobeHandler.DynamicParent3.SetActive(false);
                WardrobeHandler.enabledisable.SetActive(false);
                WardrobeHandler.nextPage.GetComponent<CustomPageSwitch>().enabled = true;
                WardrobeHandler.prevPage.GetComponent<CustomPageSwitch>().enabled = true;
                foreach (var btnObj in WardrobeHandler.newEnableDisable)
                {
                    btnObj.SetActive(true);
                }
                WardrobeHandler.DisplayCosmetics();
            }
            else
            {
                WardrobeHandler.DynamicParent1.SetActive(true);
                WardrobeHandler.DynamicParent2.SetActive(true);
                WardrobeHandler.DynamicParent3.SetActive(true);
                WardrobeHandler.enabledisable.SetActive(true);
                WardrobeHandler.nextPage.GetComponent<CustomPageSwitch>().enabled = false;
                WardrobeHandler.prevPage.GetComponent<CustomPageSwitch>().enabled = false;
                foreach (var btnObj in WardrobeHandler.newEnableDisable)
                {
                    btnObj.SetActive(true);
                }
            }
        }
    }
}