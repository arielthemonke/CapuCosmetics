using System;
using UnityEngine;
using Il2CppLocomotion;

namespace CapuchinCosmetics
{
    public class CustomEnableDisable : MonoBehaviour
    {
        public CustomCosmetic toEnable;
        private bool wasPressedR;
        private bool wasPressedL;

        private void Update()
        {
            if (Vector3.Distance(Player.Instance.LeftHand.transform.position,
                    this.gameObject.transform.position) < 0.2f && !wasPressedL || 
                Vector3.Distance(Player.Instance.RightHand.transform.position,
                    this.gameObject.transform.position) < 0.2f && !wasPressedR)
            {
                toEnable.CosmeticObj.SetActive(!toEnable.CosmeticObj.activeSelf);
            }

            wasPressedR = Vector3.Distance(Player.Instance.RightHand.transform.position,
                this.gameObject.transform.position) < 0.2f;
            wasPressedL = Vector3.Distance(Player.Instance.LeftHand.transform.position,
                this.gameObject.transform.position) < 0.2f;
        }
    }
}