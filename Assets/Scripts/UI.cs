using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityTemplateProjects;

public class UI : MonoBehaviour
{
    public RectTransform SystemPanel;
    public GameObject SystemUI;
    public Image setting, pause;
    public Color En, Ex, White, Gray;
    private bool isGamePause = false;
    public RectTransform ReferencePosSys;
    public RectTransform SysSize;
    private int SystemNumeric = 1;
    public Text SysText;
    public GameObject SysPf;
    public static string selectedSystem;
    public static GameObject selectedSystemUI;
    public static List<RectTransform> SystemUIList = new List<RectTransform>();
    //private string WritingSystemName = "New System 2";
    public static Text WritingSystemText;
    public GameObject Sett, SettEdit;

    public InputField SystemInTex;
    public InputField GX, GY, GZ, SPX, SPY, SPZ, SLX, SLY, SLZ;
    public void SettingEnter()
    {
        setting.color = En;
    }
    public void SettingExit()
    {
        setting.color = Ex;
    }
    public void BoldIn(Image e)
    {
        e.color = White;
    }
    public void BoldOut(Image e)
    {
        //if (isGamePause == false || e.name != "Pause")
        e.color = new Color(0, 0, 0, 0);
        if (isGamePause == true && e.name == "PauseBC")
        {
            e.color = White;
        }
    }
    public void PlayGame(Slider e)
    {
        pause.color = new Color(0, 0, 0, 0);
        Time.timeScale = e.value;
        isGamePause = false;
    }
    public void PauseGame(Image e)
    {
        isGamePause = true;
        e.color = White;
        Time.timeScale = 0.01f;
    }
    public void TimeScaleUpdate(Slider e)
    {
        Time.timeScale = e.value;
    }
    public void AMEEnter(Image e)
    {
        e.color = White;
    }
    public void AMEExit(Image e)
    {
        e.color = Gray;
    }
    public void AddSystem()
    {
        GameObject c = Instantiate(SystemUI, SystemPanel);
        c.GetComponentInChildren<Text>().text = "New System " + SystemNumeric;
        c.transform.SetParent(SystemPanel.GetComponent<RectTransform>());
        GameManager.InstantiateSystem(c.GetComponentInChildren<Text>().text, SysPf);
        SystemUIList.Add(c.GetComponent<RectTransform>());
        SystemNumeric++;
        /*RectTransform f = c.GetComponent<RectTransform>();
        f.anchoredPosition = ReferencePosSys.anchoredPosition;
        f.anchorMin = ReferencePosSys.anchorMin;
        f.anchorMax = ReferencePosSys.anchorMax;
        f.localPosition -= new Vector3(0, 35 * GameManager.SystemCount - 35, 0);
        SysSize.sizeDelta = new Vector2(SysSize.sizeDelta.x, GameManager.SystemCount * 35 + 30);*/
        SysReAlign();
    }
    public void RemoveSystem()
    {
        GameManager.DestroySystem(selectedSystem, selectedSystemUI);
        SysReAlign();
    }
    public void SysReAlign()
    {
        for (int j = 0; j < SystemUIList.Count; j++)
        {
            //print(j + )
            SystemUIList[j].anchoredPosition = ReferencePosSys.anchoredPosition;
            SystemUIList[j].anchorMin = ReferencePosSys.anchorMin;
            SystemUIList[j].anchorMax = ReferencePosSys.anchorMax;
            SystemUIList[j].localPosition -= new Vector3(0, 35 * j, 0);
        }
        SysSize.sizeDelta = new Vector2(SysSize.sizeDelta.x, GameManager.SystemCount * 35 + 30);
    }
    public void ChangeSystemName(InputField input)
    {
        GameObject a = GameObject.Find(selectedSystem);
        a.name = input.text;
        a.GetComponent<PhysicSystem>().SystemName = input.text;
        selectedSystem = input.text;
        WritingSystemText.text = input.text;
    }
    public void EditSystem()
    {
        PhysicSystem b = GameObject.Find(selectedSystem).GetComponent<PhysicSystem>();
        SystemInTex.text = b.name;
        GX.text = b.Gravity.x.ToString();
        GY.text = b.Gravity.y.ToString();
        GZ.text = b.Gravity.z.ToString();
        SPX.text = b.gameObject.transform.position.x.ToString();
        SPY.text = b.gameObject.transform.position.y.ToString();
        SPZ.text = b.gameObject.transform.position.z.ToString();
        SLX.text = b.SpawnLocation.position.x.ToString();
        SLY.text = b.SpawnLocation.position.y.ToString();
        SLZ.text = b.SpawnLocation.position.z.ToString();
        Sett.SetActive(false);
        SettEdit.SetActive(true);

    }
    public void GUpdate()
    {
        GameObject.Find(selectedSystem).GetComponent<PhysicSystem>().Gravity = new Vector3(float.Parse(GX.text), float.Parse(GY.text), float.Parse(GZ.text));
    }
    public void SPUpdate()
    {
        GameObject.Find(selectedSystem).GetComponent<PhysicSystem>().transform.position = new Vector3(float.Parse(SPX.text), float.Parse(SPY.text), float.Parse(SPZ.text));
    }
    public void SLUpdate()
    {
        GameObject.Find(selectedSystem).GetComponent<PhysicSystem>().SpawnLocation.position = new Vector3(float.Parse(SLX.text), float.Parse(SLY.text), float.Parse(SLZ.text));
    }
    Vector3 u,i;
    public Transform Love,look;
    public void takeMeToPlace()
    {
        u = Camera.main.transform.position;
        i = look.transform.position;
        Camera.main.GetComponent<SimpleCameraController>().enabled = false;
        StartCoroutine("here");
    }
    IEnumerator here()
    {
        for (float j = 0; j <= 1; j += 0.02f)
        {
            Camera.main.transform.position = Vector3.Lerp(u, Love.position, j);
            look.transform.position = Vector3.Lerp(i, new Vector3(36, 1.5f, -426.04f), j);
            Camera.main.transform.LookAt(look);
            //Camera.main.transform.localEulerAngles = Vector3.Lerp(i, Love.localEulerAngles, j);
            //Camera.main.transform.Rotate(i / 50f);
            yield return new WaitForSeconds(0.01f);
        }
        Camera.main.GetComponent<SimpleCameraController>().enabled = true;
    }
        
    private void Start()
    {
        SystemNumeric = GameManager.SystemCount;
    }
}
