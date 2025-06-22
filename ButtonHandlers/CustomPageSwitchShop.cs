using UnityEngine;

namespace CapuchinCosmetics
{
    public class CustomPageSwitchShop : MonoBehaviour
    {
        public int mode;

        private void OnTriggerEnter(Collider other)
        {
            switch (mode)
            {
                case 0:
                    ShopWardrobeHandler.NextPage();
                    break;
                case 1:
                    ShopWardrobeHandler.PrevPage();
                    break;
            }
        }
    }
}