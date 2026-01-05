using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearLayer : MonoBehaviour
{
    public Button nextStageBtn;
    public Button mainBtn;

    void Awake()
    {
        nextStageBtn.onClick.AddListener(() =>
        {
            StartCoroutine(NextStageRoutine());
        });

        mainBtn.onClick.AddListener(() =>
        {
            // 다음 스테이지 결정
            GameStage current = DataManager.Instance.SELECTSTAGE;
            GameStage next = ++current;
            DataManager.Instance.STAGE = next;
            DataManager.Instance.SELECTSTAGE = next;
            
            SceneControllerManager.Instance.AsyncLoadingScene("Main");
        });
    }

    IEnumerator NextStageRoutine()
    {
        // 다음 스테이지 결정
        GameStage current = DataManager.Instance.SELECTSTAGE;
        GameStage next = ++current;
        DataManager.Instance.STAGE = next;
        DataManager.Instance.SELECTSTAGE = next;

        // 로딩 씬으로 전환(LoadingScene에서 GameScene을 비동기로 로드함)
        LoadingScene.LoadScene("GameScene");

        yield break;
    }
}
