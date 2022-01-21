using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityTemplateProjects;

public class GameManager : MonoBehaviour
{
    public ObjectEdit objectEdit;
    public static List<PhysicSystem> Systems = new List<PhysicSystem>();
    public static int SystemCount = 0;
    public static PhysicSystem currentSystem;
    public static PhysicObject currentPhysicObject;
    public static GameObject k,uI;
    public GameObject p,u;
    public static GameObject systemPage,objectPage;
    public GameObject e, m,planet,pendulum,spring,button;
    enum fh
    {
        a, b, c
    }
    void Start()
    {

        systemPage = e;
        objectPage = m;
        PhysicSystem[] a;
        a = FindObjectsOfType<PhysicSystem>();
        foreach (PhysicSystem b in a)
            Systems.Add(b);
        SystemCount = Systems.Count;
        k = p;
        uI = u;
    }
    public static void InstantiateSystem(string name,GameObject SysPf)
    {
        GameObject a = Instantiate(SysPf);
        a.name = name;
        a.transform.position = Vector3.zero;
        a.transform.localEulerAngles = Vector3.zero;
        a.transform.localScale = Vector3.one;
        Systems.Add(a.GetComponent<PhysicSystem>());
        SystemCount = Systems.Count;
    }
    public static void DestroySystem(string name,GameObject ui)
    {
        Systems.Remove(GameObject.Find(name).GetComponent<PhysicSystem>());
        Destroy(GameObject.Find(name));
        UI.SystemUIList.Remove(ui.GetComponent<RectTransform>());
        Destroy(ui);
        SystemCount = Systems.Count;
    }
    void Update()
    {
        Camera.main.GetComponent<SimpleCameraController>().boost = Mathf.Clamp(5 * 0.4f / Time.timeScale,4,12);
        Camera.main.GetComponent<SimpleCameraController>().positionLerpTime = 0.2f * Time.timeScale;
        Application.targetFrameRate = 60;
        Ray l;
        RaycastHit p;
        PrefabHolder a;
        PhysicObject b;
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            l = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(l, out p);
            if (p.collider.GetComponent<PrefabHolder>() != null)
            {
                a = p.collider.GetComponent<PrefabHolder>();
                currentSystem.ObjectInstantiation(a.Prefab);
            }
            if(p.collider.GetComponent<PhysicObject>() != null)
            {
                b = p.collider.GetComponent<PhysicObject>();
            }
        }
    }
    public void delete()
    {
        Destroy(planet);
        Destroy(pendulum);
        Destroy(spring);
        Destroy(button);

    }
}