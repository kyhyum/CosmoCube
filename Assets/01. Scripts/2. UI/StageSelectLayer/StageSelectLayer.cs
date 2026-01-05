using System.Collections.Generic;
using UnityEngine;

public class StageSelectLayer : MonoBehaviour
{
    public DynamicListView dynamicListView;

    private void Awake()
    {
        dynamicListView.Init();
        List <DynamicListItem> dynamicListItems = dynamicListView.GetHorizontalList(System.Enum.GetValues(typeof(GameStage)).Length);
        for(int i = 0; i < dynamicListItems.Count; i++)
        {
            StageSelectItem stageSelectItem = dynamicListItems[i].GetComponent<StageSelectItem>();
            stageSelectItem.SetStage((GameStage)i);
        }

    }
}
