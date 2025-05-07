using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveUI : MonoBehaviour
{
    public List<GameObject> gameObjects;

    public void DeactiveAll() 
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].SetActive(false);
        }
    }
}
