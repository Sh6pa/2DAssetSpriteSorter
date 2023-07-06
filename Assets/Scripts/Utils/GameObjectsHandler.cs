using UnityEngine;
using System;

[Serializable]
public class GameObjectsHandler
{
    public GameObject[] GameObjectList;
    // Unity requires a default constructor for serialization
    public GameObjectsHandler() { }

    public GameObjectsHandler(GameObject[] GameObjects)
    {
        GameObjectList = GameObjects;
    }

    public void ToAllGameObjects(Action<GameObject> Action)
    {
        if (GameObjectList != null && GameObjectList.Length > 0)
        {
            foreach(GameObject gameObject in GameObjectList)
            {
                Action(gameObject);
            }
        }
    }
}
