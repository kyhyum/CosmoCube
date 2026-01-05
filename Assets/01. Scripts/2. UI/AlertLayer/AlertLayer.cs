using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertLayer : MonoBehaviour
{
    public CanvasGroup baseAlertCG;

    public Image bg1Img;

    public TextMeshProUGUI titleText;
    
    public Button cancleBtn;
    public Button confirmBtn;

    public TextMeshProUGUI cancleText;
    public TextMeshProUGUI confirmText;

    private Action<int> callback;

    void Awake()
    {
        this.cancleBtn.onClick.AddListener(delegate { OnClickBtn(0); });
        this.confirmBtn.onClick.AddListener(delegate { OnClickBtn(1); });
    }

    public void ShowTwoButton(string title, string cancleText = null, string confirmText = null, Action<int> callback = null, Sprite iconSprite = null, Sprite buttonSprite = null)
    {
        baseAlertCG.alpha = 1;
        baseAlertCG.interactable = baseAlertCG.blocksRaycasts = true;

        titleText.text = title;
        this.confirmText.text = confirmText;
        this.cancleText.text = cancleText;

        this.callback = callback;
    }


    void OnClickBtn(int idx)
    {
        UIManager.Instance.Pop();

        if (this.callback != null)
        {
            this.callback(idx);
        }
    }
}
