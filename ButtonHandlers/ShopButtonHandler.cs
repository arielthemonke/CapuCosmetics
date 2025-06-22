using UnityEngine;

namespace CapuchinCosmetics
{
    public class ShopButtonHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            WardrobeHandler.enabledModMode = !WardrobeHandler.enabledModMode;

            if (WardrobeHandler.enabledModMode)
            {
                ShopWardrobeHandler.DynamicParent4.SetActive(false);
                ShopWardrobeHandler.DynamicParent5.SetActive(false);
                ShopWardrobeHandler.DynamicParent6.SetActive(false);
                ShopWardrobeHandler.enabledisableShop.SetActive(false);
                ShopWardrobeHandler.nextPage.GetComponent<CustomPageSwitch>().enabled = true;
                ShopWardrobeHandler.prevPage.GetComponent<CustomPageSwitch>().enabled = true;
                foreach (var btnObj in ShopWardrobeHandler.newEnableDisableShop)
                {
                    btnObj.SetActive(true);
                }
                ShopWardrobeHandler.DisplayCosmetics();
            }
            else
            {
                ShopWardrobeHandler.DynamicParent4.SetActive(true);
                ShopWardrobeHandler.DynamicParent5.SetActive(true);
                ShopWardrobeHandler.DynamicParent6.SetActive(true);
                ShopWardrobeHandler.enabledisableShop.SetActive(true);
                ShopWardrobeHandler.nextPage.GetComponent<CustomPageSwitch>().enabled = false;
                ShopWardrobeHandler.prevPage.GetComponent<CustomPageSwitch>().enabled = false;
                foreach (var btnObj in ShopWardrobeHandler.newEnableDisableShop)
                {
                    btnObj.SetActive(true);
                }
            }
        }
    }
}