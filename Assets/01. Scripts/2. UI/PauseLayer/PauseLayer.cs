using UnityEngine;
using UnityEngine.UI;

public class PauseLayer : MonoBehaviour
{
    public Button retryBtn;
    public Button ConditnueBtn;

    public Button muteToggleBtn;
    public Image muteToggleOnImg;
    public Image muteToggleOffImg;

    public Button vibrateToggleBtn;
    public Image vibrateToggleOffImg;
    public Image vibrateToggleOnImg;

    void Awake()
    {
        Time.timeScale = 0f;

        if (retryBtn != null)
        {
            retryBtn.onClick.RemoveAllListeners();
            retryBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1f;

                if (typeof(SceneControllerManager) != null)
                {
                    SceneControllerManager.Instance.AsyncLoadingScene("GameScene");
                }
                else
                {
                    LoadingScene.LoadScene("GameScene");
                }
            });
        }

        if (ConditnueBtn != null)
        {
            ConditnueBtn.onClick.RemoveAllListeners();
            ConditnueBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1f;
                if (UIManager.Instance != null)
                    UIManager.Instance.Pop();
            });
        }

        if (muteToggleBtn != null)
        {
            muteToggleBtn.onClick.RemoveAllListeners();
            muteToggleBtn.onClick.AddListener(() =>
            {
                DataManager.Instance.ToggleMute();
                UpdateMuteUI();
            });
        }

        if (vibrateToggleBtn != null)
        {
            vibrateToggleBtn.onClick.RemoveAllListeners();
            vibrateToggleBtn.onClick.AddListener(() =>
            {
                DataManager.Instance.ToggleVibrate();
                UpdateVibrateUI();
            });
        }

        UpdateMuteUI();
        UpdateVibrateUI();
    }

    void OnDestroy()
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }

    void UpdateMuteUI()
    {
        bool muted = DataManager.Instance != null && DataManager.Instance.IsMuted;
        if (muteToggleOnImg != null) muteToggleOnImg.gameObject.SetActive(muted);
        if (muteToggleOffImg != null) muteToggleOffImg.gameObject.SetActive(!muted);
    }

    void UpdateVibrateUI()
    {
        bool vib = DataManager.Instance != null && DataManager.Instance.IsVibrate;
        if (vibrateToggleOnImg != null) vibrateToggleOnImg.gameObject.SetActive(vib);
        if (vibrateToggleOffImg != null) vibrateToggleOffImg.gameObject.SetActive(!vib);
    }
}
