using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicListView : MonoBehaviour
{
    public int initCreateCount;
    public float itemHorizontalPadding;
    public float itemVerticalPadding;

    public ScrollRect scrollRect;
    public DynamicList dynamicList;

    public GameObject dynamicItem;
    RectTransform dynamicItemRect;
    DynamicListItem dynamicListItem;

    List<DynamicListItem> objectPool = new List<DynamicListItem>();
    List<DynamicListItem> onShowingPool = new List<DynamicListItem>();

    public void Init()
    {
        dynamicItemRect = dynamicItem.GetComponent<RectTransform>();
        dynamicListItem = dynamicItem.GetComponent<DynamicListItem>();

        dynamicItem.SetActive(false);
        for (int i = 0; i < initCreateCount; i++)
        {
            GameObject createObj = Instantiate(dynamicItem, dynamicItem.transform.parent);
            createObj.SetActive(false);
            DynamicListItem dynamicListItem = createObj.GetComponent<DynamicListItem>();
            dynamicListItem.SetDynamicItemRect(dynamicList);
            objectPool.Add(dynamicListItem);
        }
    }

    private DynamicListItem GetItem()
    {
        if (objectPool.Count == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                objectPool.Add(Instantiate(dynamicItem, dynamicItem.transform.parent).GetComponent<DynamicListItem>());
            }
        }
        DynamicListItem obj = objectPool[0];
        objectPool.Remove(obj);
        onShowingPool.Add(obj);
        return obj;
    }

    public List<DynamicListItem> GetVerticalList(int itemCnt)
    {
        float offset = 0;
        for (int i = 0; i < itemCnt; i++)
        {
            DynamicListItem item = GetItem();
            item.gameObject.SetActive(true);
            item.mRectTransform.anchoredPosition = new Vector3(0, offset);

            offset += -(item.HEIGHT + itemVerticalPadding);
        }
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, Mathf.Abs(offset) - itemVerticalPadding);

        return onShowingPool;
    }

    public List<DynamicListItem> GetHorizontalList(int itemCnt)
    {
        float offset = 0;
        for (int i = 0; i < itemCnt; i++)
        {
            DynamicListItem item = GetItem();
            item.gameObject.SetActive(true);
            item.mRectTransform.anchoredPosition = new Vector3(offset, 0);

            offset += (item.WIDTH + itemHorizontalPadding);
        }
        scrollRect.content.sizeDelta = new Vector2(Mathf.Abs(offset) - itemHorizontalPadding, scrollRect.content.sizeDelta.y);

        return onShowingPool;
    }

    public List<DynamicListItem> GetGridList(int itemCnt, int fixedColumnCnt)
    {
        for (int i = 0; i < itemCnt; i++)
        {
            int columnIdx = i % fixedColumnCnt;
            int rowIdx = i / fixedColumnCnt;

            DynamicListItem item = GetItem();
            item.gameObject.SetActive(true);
            item.mRectTransform.anchoredPosition = new Vector3((item.WIDTH + itemHorizontalPadding) * columnIdx, -(item.HEIGHT + itemVerticalPadding) * rowIdx);

        }
        scrollRect.content.sizeDelta = new Vector2(Mathf.Abs(fixedColumnCnt * (dynamicListItem.WIDTH)) - itemHorizontalPadding, ((itemCnt / fixedColumnCnt) + 1) * (dynamicListItem.HEIGHT + itemVerticalPadding) - itemVerticalPadding);

        return onShowingPool;
    }

    public void ReturnItem()
    {
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, 0);
        int cnt = onShowingPool.Count;
        for (int i = 0; i < cnt; i++)
        {
            onShowingPool[0].gameObject.SetActive(false);
            objectPool.Add(onShowingPool[0]);
            onShowingPool.RemoveAt(0);
        }
    }

}