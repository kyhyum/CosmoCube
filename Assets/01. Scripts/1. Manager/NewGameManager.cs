using UnityEngine;

public class NewGameManager : Singleton<NewGameManager>
{
    public GameStep step;
    

    int moduleNo = 1;
    public int MODULENO
    {
        get { return moduleNo; }
        set { moduleNo = value; }
    }

    FSMManager fSMManager = new FSMManager();

    private void Awake()
    {
        Application.targetFrameRate = 30;
        DontDestroyOnLoad(this.gameObject);
        SetStep();
    }

    public void SetStep()
    {
        switch (step)
        {
            case GameStep.Init:
                {
                    //PlayerPrefs.SetInt("Stage", 1);
                    fSMManager.ChangeState(fSMManager.InitGameState);
                }
                break;
            case GameStep.InGame:
                fSMManager.ChangeState(fSMManager.initScreenState);
                break;
        }
    }
}
