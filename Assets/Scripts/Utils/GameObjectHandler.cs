using UnityEngine;
using System;

[Serializable]
public class GameObjectHandler
{
    public GameObject GameObject;
    // Unity requires a default constructor for serialization
    public GameObjectHandler() { }

    public GameObjectHandler(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    public void ToGameObject(Action<GameObject> Action)
    {
        if (GameObject == null) return;
        Action(GameObject);
    }
    public void  Enable()
    {
        Action<GameObject> enable = (GameObject go) =>
        {
            go.SetActive(true);
        };
        ToGameObject(enable);
    }
    public void Disable()
    {
        Action<GameObject> disable = (GameObject go) =>
        {
            go.SetActive(false);
        };
        ToGameObject(disable);
    }
}
