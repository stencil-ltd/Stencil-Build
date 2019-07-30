using UnityEngine;
using UnityEngine.UI;
using Versions;

namespace Scripts
{
    [RequireComponent(typeof(Text))]
    public class VersionCode : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Text>().text = ""+VersionCodes.GetVersionCode();
        }
    }
}