using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataManager : Singleton<DataManager>
{
    public GameStage currentGameStage = GameStage.Stage1;
    GameStage stage = GameStage.Stage1;
    GameStage selectStage;

    // audio / vibration settings
    const string PREF_KEY_MUTE = "Pref_Mute";
    const string PREF_KEY_VIBRATE = "Pref_Vibrate";

    bool isMuted = false;
    bool isVibrate = true;

    public bool IsMuted => isMuted;
    public bool IsVibrate => isVibrate;

    public GameStage STAGE
    {
        get { return stage; }
        set {
                int dataStage = PlayerPrefs.GetInt("Stage");
                if(dataStage < (int)value)
                {
                    stage = value;
                    PlayerPrefs.SetInt("Stage", (int)value);
                }
            }
    }

    public GameStage SELECTSTAGE
    {
        get { return selectStage; }
        set {
                
                selectStage = value; 
            }
    }
    private void Awake()
    {
        initData();
    }

    public void initData()
    {
        stage = (GameStage)PlayerPrefs.GetInt("Stage");

        // load audio/vibrate prefs
        isMuted = PlayerPrefs.GetInt(PREF_KEY_MUTE, 0) == 1;
        isVibrate = PlayerPrefs.GetInt(PREF_KEY_VIBRATE, 1) == 1;

        // Apply mute immediately if SoundManager exists
        // if (SoundManager.instance != null)
        //     SoundManager.instance.AllMute(isMuted);
    }

    public void LoadData()
    {
    }

    public void GetData()
    {

    }

    // public API to toggle settings
    public void SetMute(bool mute)
    {
        isMuted = mute;
        PlayerPrefs.SetInt(PREF_KEY_MUTE, mute ? 1 : 0);
        PlayerPrefs.Save();
        // if (SoundManager.instance != null)
        //     SoundManager.instance.AllMute(isMuted);
    }

    public void ToggleMute()
    {
        SetMute(!isMuted);
    }

    public void SetVibrate(bool vibrate)
    {
        isVibrate = vibrate;
        PlayerPrefs.SetInt(PREF_KEY_VIBRATE, vibrate ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleVibrate()
    {
        SetVibrate(!isVibrate);
        // optional immediate feedback
        if (isVibrate)
        {
            #if UNITY_ANDROID || UNITY_IOS
                Handheld.Vibrate();
            #endif
        }
    }
}