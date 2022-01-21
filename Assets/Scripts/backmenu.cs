using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backmenu : MonoBehaviour
{
    public GameObject a, b;
    public void back()
    {
        a.SetActive(false);
        b.SetActive(true);
    }
}
