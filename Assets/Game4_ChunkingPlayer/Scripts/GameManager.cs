using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

namespace ChunkingPlayer
{
    public class GameManager : MonoBehaviour
    { 
        public TMPro.TMP_InputField inputField;

    void Start()
    {
        WebGLInput.mobileKeyboardSupport = true;  // เปิดให้รองรับแป้นพิมพ์มือถือ
    }

    public void OpenKeyboard()
    {
        if (inputField != null)
        {
            TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Default);
        }
    }
    }

}
