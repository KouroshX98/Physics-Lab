using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemBCControl : MonoBehaviour
{
    public void SelectedSystem(Text name)
    {
        UI.selectedSystem = name.text;
        UI.selectedSystemUI = gameObject;
        UI.WritingSystemText = name;
        GameManager.currentSystem = GameObject.Find(name.text).GetComponent<PhysicSystem>();
    }
}
