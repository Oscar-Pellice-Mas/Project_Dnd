using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private string entityName;
    private Vector3 entityPosition;
    private GameObject entityObject;
    private Sprite entityPortrait;
    private int entitySize;

    public Entity(string name, Vector3 position)
    {
        entityName = name;
        entityPosition = position;
    }

    public string GetName()
    {
        return entityName;
    }

    public void SetName(string name)
    {
        entityName = name;
    }

    public Vector3 GetPosition()
    {
        return entityPosition;
    }

    public void SetPosition(Vector3 vector3)
    {
        entityPosition = vector3;
    }

    public GameObject GetObject()
    {
        return entityObject;
    }

    public void SetObject(GameObject gameObject)
    {
        entityObject = gameObject;
    }

    public Sprite GetPortrait()
    {
        return entityPortrait;
    }

    public void SetPortrait(Sprite sprite)
    {
        entityPortrait = sprite;
    }

    public int GetSize()
    {
        return entitySize;
    }

    public void SetSize(int size)
    {
        entitySize = size;
    }
}
