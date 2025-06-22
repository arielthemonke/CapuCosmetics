using System;
using UnityEngine;

namespace CapuchinCosmetics
{
    public class CustomEnableDisable : MonoBehaviour
    {
        public CustomCosmetic toEnable;

        private void Update()
        {
            if (Vector3.Distance(Locomotion.Player.Instance.LeftHand.transform.position,
                    this.gameObject.transform.position) < 0.2f)
            {
                toEnable.CosmeticObj.SetActive(!toEnable.CosmeticObj.activeSelf);
            }
        }
    }
}