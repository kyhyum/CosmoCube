using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();
                if (instance == null)
                {
                    return null;
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            return;
        }

        instance = GetComponent<T>();
    }
}