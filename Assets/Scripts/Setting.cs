using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Setting : MonoBehaviour
{
    public InputField IBT;
    public InputField IST;
    public InputField ICO;
    public InputField ISI;
    public InputField ISVI;
    public Toggle TAF;
    public Dropdown DBT;
    public Dropdown DFT;
    public Toggle TED;
    public PostProcessProfile PPP;

    public void Update()
    {
        print(Time.frameCount / Time.time);

    }
    public void SettingUpdate()
    {
        Physics.bounceThreshold = float.Parse(IBT.text);
        Physics.sleepThreshold = float.Parse(IST.text);
        Physics.defaultContactOffset = float.Parse(ICO.text);
        Physics.defaultSolverIterations = int.Parse(ISI.text);
        Physics.defaultSolverVelocityIterations = int.Parse(ISVI.text);
    }

}
