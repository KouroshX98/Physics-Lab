using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflect : MonoBehaviour
{
    // Start is called before the first frame updat
    public GameObject a;
    RaycastHit f;
    void Start()
    {
        
    }

    // Update is c
    void Update()
    {
        Ray ld = Camera.main.ScreenPointToRay(new Vector3(0.1f * Camera.main.pixelWidth, 0.1f * Camera.main.pixelHeight, 0));
        Ray cd = Camera.main.ScreenPointToRay(new Vector3(0.5f * Camera.main.pixelWidth, 0.1f * Camera.main.pixelHeight, 0));
        Ray rd = Camera.main.ScreenPointToRay(new Vector3(0.9f * Camera.main.pixelWidth, 0.1f * Camera.main.pixelHeight, 0));
        Ray lc = Camera.main.ScreenPointToRay(new Vector3(0.1f * Camera.main.pixelWidth, 0.5f * Camera.main.pixelHeight, 0));
        Ray cc = Camera.main.ScreenPointToRay(new Vector3(0.5f * Camera.main.pixelWidth, 0.5f * Camera.main.pixelHeight, 0));
        Ray rc = Camera.main.ScreenPointToRay(new Vector3(0.9f * Camera.main.pixelWidth, 0.5f * Camera.main.pixelHeight, 0));
        Ray lu = Camera.main.ScreenPointToRay(new Vector3(0.1f * Camera.main.pixelWidth, 0.9f * Camera.main.pixelHeight, 0));
        Ray cu = Camera.main.ScreenPointToRay(new Vector3(0.5f * Camera.main.pixelWidth, 0.9f * Camera.main.pixelHeight, 0));
        Ray ru = Camera.main.ScreenPointToRay(new Vector3(0.9f * Camera.main.pixelWidth, 0.9f * Camera.main.pixelHeight, 0));
        RaycastHit[] y = new RaycastHit[9];
        Physics.Raycast(ld,out y[0], 250f);
        Physics.Raycast(cd,out y[1], 250f);
        Physics.Raycast(rd,out y[2], 250f);
        Physics.Raycast(lc,out y[3], 250f);
        Physics.Raycast(cc,out y[4], 250f);
        Physics.Raycast(rc,out y[5], 250f);
        Physics.Raycast(lu,out y[6], 250f);
        Physics.Raycast(cu,out y[7], 250f);
        Physics.Raycast(ru,out y[8], 250f);
        Debug.DrawLine(ld.origin, y[0].point);
        Debug.DrawLine(cd.origin, y[1].point);
        Debug.DrawLine(rd.origin, y[2].point);
        Debug.DrawLine(lc.origin, y[3].point);
        Debug.DrawLine(cc.origin, y[4].point);
        Debug.DrawLine(rc.origin, y[5].point);
        Debug.DrawLine(lu.origin, y[6].point);
        Debug.DrawLine(cu.origin, y[7].point);
        Debug.DrawLine(ru.origin, y[8].point);
        RaycastHit desti = y[0];
        foreach(RaycastHit l in y)
        {
            print(l.distance);
            if(l.distance < desti.distance)
            {
                print("desti   " + desti.distance);
                desti = l;
            }
        }
        a.transform.position = desti.point;
    }
}
