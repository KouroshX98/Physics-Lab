using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHolder : MonoBehaviour
{
    /// <summary>
    /// Physic Object Variable
    /// </summary>
    public PhysicObject obj;
    public Text nm;
    public GameObject edit, current;
    public GameObject self;
    public void Start()
    {
        nm.text = obj.name;
        edit = GameManager.objectPage;
        current = GameManager.systemPage;
    }
    public void moveOntoEdit()
    {
        GameManager.currentPhysicObject = obj;
        edit.SetActive(true);
        edit.GetComponent<ObjectEdit>().Initialization();
        current.SetActive(false);
    }
    public void delete()
    {
        Destroy(obj.gameObject);
        Destroy(self);
    }
}
