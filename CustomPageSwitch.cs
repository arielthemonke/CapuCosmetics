using System;
using System.Diagnostics;
using UnityEngine;

namespace CapuchinCosmetics
{
    public class CustomPageSwitch : MonoBehaviour
    {
        public int mode;

        private void OnTriggerEnter(Collider other)
        {
            switch (mode)
            {
                case 0:
                    WardrobeHandler.NextPage();
                    break;
                case 1:
                    WardrobeHandler.PrevPage();
                    break;
            }
        }
    }
}