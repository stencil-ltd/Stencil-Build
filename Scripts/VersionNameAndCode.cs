using System;
using UnityEngine;
using UnityEngine.UI;
using Versions;

namespace Scripts
{
    [RequireComponent(typeof(Text))]
    public class VersionNameAndCode : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Text>().text = $"{VersionCodes.GetVersionName()} ({VersionCodes.GetVersionCode()})";
        }
    }
}