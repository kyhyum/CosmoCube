using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectItem : MonoBehaviour
{
    public Image buttonImg;
    public GameObject lockObj;
    public Button stageSelectButton;
    public TextMeshProUGUI stageTxt;
    public GameStage gameStage;
    

    public void SetStage(GameStage stage)
    {
        if(stage > DataManager.Instance.STAGE)
        {
            lockObj.SetActive(true);
            stageSelectButton.interactable = false;
        }
        else
        {
            lockObj.SetActive(false);
            stageSelectButton.interactable = true;
        }
        gameStage = stage;
        SetButton();
    }

    public void SetButton()
    {
        stageTxt.text = gameStage.ToString();
        stageSelectButton.onClick.AddListener(() =>
        {
            DataManager.Instance.SELECTSTAGE = gameStage;
            SceneControllerManager.Instance.AsyncLoadingScene("GameScene");
        });
    }
}
