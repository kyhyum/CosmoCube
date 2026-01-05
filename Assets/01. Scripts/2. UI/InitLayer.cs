using UnityEngine;
using UnityEngine.UI;

public class InitLayer : MonoBehaviour
{
    public Button startBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Push(UIName.StageSelectLayer);
        });
    }
}
