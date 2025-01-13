using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDostoryOnLoad : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<DonDostoryOnLoad>().Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Debug.Log("DontDestroyOnLoad");
    }
}
