using UnityEngine;

namespace Utils
{
    public class Resolution : MonoBehaviour
    {
        void Start()
        {
            Screen.SetResolution(600, 1920, true);
        }
    }
}
