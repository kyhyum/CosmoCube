using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    [Header("UI")]
    public Oxygen oxygen;
    public JoyStick joystick;
    public LifeUI lifeUI;

    [Header("Button")]
    public Button settingBtn;
    public Button destroyBtn;
    public Button metaBtn;
    public TextMeshProUGUI metaCntTxt;

    private void Awake()
    {
        SetButton();
    }

    public void SetButton()
    {
        settingBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 0f;
            UIManager.Instance.Push(UIName.PauseLayer);
        });

        destroyBtn.onClick.AddListener(() =>
        {
            Player player = InGameManager.Instance.player;
            if (player == null || player.controller == null) return;
            PlayerController ctrl = player.controller;
            ctrl.sm.SetState(ctrl.dicState[PlayerState.Destroy]);
        });

        metaBtn.onClick.AddListener(() =>
        {
            Player player = InGameManager.Instance.player;
            if (player == null || player.controller == null || player.metaCount == 0) return;
            PlayerController ctrl = player.controller;
            ctrl.sm.SetState(ctrl.dicState[PlayerState.Meta]);
            player.metaCount--;
            UpdateMetaBtn();
        });
    }

    public void UpdateMetaBtn()
    {
        metaCntTxt.text = InGameManager.Instance.player.metaCount.ToString();
    }
}
