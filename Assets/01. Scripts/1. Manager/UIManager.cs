using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Stack<GameObject> stackLayers = new Stack<GameObject>();
    public Transform uiParentsTrs;
    string path = "UIPrefabs/{0}";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Pop()
    {
        GameObject topLayer = stackLayers.Peek();
        Destroy(topLayer.gameObject);
        stackLayers.Pop();
    }

    public GameObject Push(UIName layerName)
    {
        GameObject layerObj = Resources.Load<GameObject>(string.Format(path, layerName));
        GameObject uiObj = Instantiate(layerObj, uiParentsTrs);
        stackLayers.Push(uiObj);
        return uiObj;
    }

    public void AllPop()
    {
        int cnt = stackLayers.Count;
        for (int i = 0; i < cnt - 1; i++)
        {
            Pop();
        }
    }

    public GameObject GetPeekLayer()
    {
        return stackLayers.Peek().gameObject;
    }
}