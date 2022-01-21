using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class few : MonoBehaviour
{
    public Text a;
    float deltaTime = 0.0f;
    void Start()
    {
        StartCoroutine("frame");
    }
    IEnumerator frame()
    {
        while (true)
        {
            deltaTime += 1/((Time.unscaledDeltaTime - deltaTime));
            a.text = deltaTime.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
    void Update()
    {

    }
}
