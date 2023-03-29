using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        void Late()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
