using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UPR : MonoBehaviour
{
    public InputField PX, PY, PZ;
    public InputField RX, RY, RZ;
    public GameObject self;
    void Start()
    {
        
    }

    void Update()
    {
        if(!PX.isFocused)
        {
            PX.text = self.transform.position.x.ToString();
        }
        if (!PY.isFocused)
        {
            PY.text = self.transform.position.y.ToString();
        }
        if (!PZ.isFocused)
        {
            PZ.text = self.transform.position.z.ToString();
        }
        if (!RX.isFocused)
        {
            RX.text = self.transform.rotation.eulerAngles.x.ToString();
        }
        if (!RY.isFocused)
        {
            RY.text = self.transform.rotation.eulerAngles.y.ToString();
        }
        if (!RZ.isFocused)
        {
            RZ.text = self.transform.rotation.eulerAngles.z.ToString();
        }
    }
    public void change()
    {
        self.transform.position = new Vector3(float.Parse(PX.text), float.Parse(PY.text), float.Parse(PZ.text));
        self.transform.localEulerAngles = new Vector3(float.Parse(RX.text), float.Parse(RY.text), float.Parse(RZ.text));
    }
}
