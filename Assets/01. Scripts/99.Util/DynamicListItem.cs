using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicListItem : MonoBehaviour
{
    public RectTransform mRectTransform;
    public float HEIGHT
    {
        get { return mRectTransform.sizeDelta.y; }
    }

    public float WIDTH
    {
        get { return mRectTransform.sizeDelta.x; }
    }

    public void SetDynamicItemRect(DynamicList dynamicList)
    {
        switch (dynamicList)
        {
            case DynamicList.VerticalList:
                {
                    mRectTransform.anchorMax = Vector2.up;
                    mRectTransform.anchorMin = Vector2.up;
                    mRectTransform.pivot = Vector2.up;
                }
                break;
            case DynamicList.HorizontalList:
                {

                }
                break;
            case DynamicList.GridList:
                {
                    mRectTransform.anchorMax = Vector2.up;
                    mRectTransform.anchorMin = Vector2.up;
                    mRectTransform.pivot = Vector2.up;
                }
                break;
        }
    }

    private void Awake()
    {
        mRectTransform = GetComponent<RectTransform>();
    }
}