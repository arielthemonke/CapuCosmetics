using System;
using UnityEngine;

namespace CapuchinCosmetics
{
    public class CustomEnableDisable : MonoBehaviour
    {
        public CustomCosmetic toEnable;
        private bool wasPressedR;
        private bool wasPressedL;

        private void Update()
        {
            if (Vector3.Distance(Locomotion.Player.Instance.LeftHand.transform.position,
                    this.gameObject.transform.position) < 0.2f && !wasPressedL || 
                Vector3.Distance(Locomotion.Player.Instance.RightHand.transform.position,
                    this.gameObject.transform.position) < 0.2f && !wasPressedR)
            {
                toEnable.CosmeticObj.SetActive(!toEnable.CosmeticObj.activeSelf);
            }

            wasPressedR = Vector3.Distance(Locomotion.Player.Instance.RightHand.transform.position,
                this.gameObject.transform.position) < 0.2f;
            wasPressedL = Vector3.Distance(Locomotion.Player.Instance.LeftHand.transform.position,
                this.gameObject.transform.position) < 0.2f;
        }
    }
}