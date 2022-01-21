using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicSystem : MonoBehaviour
{
    public string SystemName;
    public List<PhysicObject> Objects = new List<PhysicObject>();
    public Vector3 Gravity = Vector3.zero;
    public Transform SpawnLocation;
    public const float GravitationalConstant = 6.67408e-11f;
    public const float ColoumbsConstant = 9e9f;
    public Vector3 Position;
    GameObject UI;
    void Start()
    {
        SpawnLocation.position = Vector3.zero;
        SpawnLocation.localEulerAngles = Vector3.zero;
        SpawnLocation.localScale = Vector3.one;
        PhysicObject[] obj = GetComponentsInChildren<PhysicObject>();
        bool p = true;
        for(int j = 9;j <= 31 && p == true;j++)
        {
            gameObject.layer = j;
            p = false;
            foreach(PhysicSystem a in GameManager.Systems)
            {
                if(a != this && p == false)
                {
                    if(gameObject.layer == a.gameObject.layer)
                    {
                        p = true;
                    }
                }
            }

        }
        foreach (PhysicObject a in obj)
        {
            Objects.Add(a);
        }
    }
    public void ObjectInstantiation(GameObject obj)
    {
        PhysicObject a;
        GameObject h;
        if (obj.GetComponent<PhysicObject>() != null)
        {
            a = obj.GetComponent<PhysicObject>();
            a.physicSystem = this;
        }
        h = Instantiate(obj, gameObject.transform);
        h.transform.position = SpawnLocation.position;
        h.gameObject.layer = gameObject.layer;
        //UI = GameObject.Instantiate(GameManager.uI, GameManager.k.transform);
        //UI.transform.parent = GameManager.k.transform;
        //UI.GetComponent<ObjectHolder>().obj = a.gameObject;

    }
    void Update()
    {
        PhysicObject[] g = GetComponentsInChildren<PhysicObject>();
        foreach(PhysicObject a in g)
        {
            if (Objects.Contains(a) == false)
                Objects.Add(a);
        }
        Position = transform.position;
    }
}
