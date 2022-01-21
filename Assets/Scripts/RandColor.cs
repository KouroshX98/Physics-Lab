using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandColor : MonoBehaviour
{
    MeshRenderer b;
    Material a;
    public float k;
    // Start is called before the first frame update
    void Start()
    {
        b = gameObject.GetComponent<MeshRenderer>();
        a = b.material;
        StartCoroutine(colorr());
    }
    
    IEnumerator colorr()
    {
        Color c = new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
        Color d = a.color;
        for (float j = 0; j <= 1; j+= 0.05f)
        {
            a.color = Color.Lerp(d, c, j);
            yield return new WaitForSeconds(k);
        }
        print(Time.time);
        StartCoroutine(colorr());
    }

    // Update is called once per frame
    void Update()
    {
        
        //print(Random.Range(0f, 1f));
    }
}
