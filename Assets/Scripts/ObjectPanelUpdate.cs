using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPanelUpdate : MonoBehaviour
{
    public RectTransform refer;
    public RectTransform panel;
    public ObjectHolder obj;
    private List<ObjectHolder> uuus = new List<ObjectHolder>();

    void Start()
    {
        
    }

    private void Update()
    {

        int l = 0;
        foreach (ObjectHolder a in uuus)
        {
            if (a.name != "Reference" && !GameManager.currentSystem.Objects.Contains(a.obj))
            {
                Destroy(a.gameObject);
                uuus.Remove(a);
            }
        }
        foreach (PhysicObject a in GameManager.currentSystem.Objects)
        {
            bool cond = false;
            foreach (ObjectHolder c in uuus)
            {
                //print(c.obj + "   " + a + "   " + GameManager.currentSystem.Objects[1]);
                if (c.obj == a)
                {
                    //print("here");
                    cond = true;
                    //return;
                }
            }
            if (cond == false)
            {
                ObjectHolder b = Instantiate(obj, GameManager.k.transform);
                uuus.Add(b);
                b.obj = a;
            }
        }
        foreach (ObjectHolder a in uuus)
        {
            RectTransform b = a.GetComponent<RectTransform>();
            b.localPosition = new Vector3(b.localPosition.x, (l * 25f) * -1f, 0);
            l++;
        }
    }


    /*void Update()
    {
        int l = 0;
        RectTransform[] rects = GetComponentsInChildren<RectTransform>();
        Transform[] t = c.GetComponentsInChildren<Transform>();
        foreach (Transform a in t)
        {
            if (a.name != "Reference")
            {
                Destroy(a.gameObject);
            }
        }
        for (int j = 0;j < GameManager.currentSystem.Objects.Count;j++)
        {
            print("oi");
            GameObject y = Instantiate(GameManager.uI, transform);
            y.GetComponent<ObjectHolder>().obj = GameManager.currentSystem.Objects[j];
        }
        for (int j = 1; j < rects.Length; j++)
        {
            if (rects[j].GetComponent<ObjectHolder>() != null)
            {
                rects[j].anchoredPosition = refer.anchoredPosition;
                rects[j].anchorMin = refer.anchorMin;
                rects[j].anchorMax = refer.anchorMax;
                rects[j].localPosition = new Vector3(0, 0, 0);
                rects[j].localPosition -= new Vector3(0, 27 * l, 0);
                l++;
            }
        }
    }*/
}
