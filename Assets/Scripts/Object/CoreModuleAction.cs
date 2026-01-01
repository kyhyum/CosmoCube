using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CoreModuleAction : MonoBehaviour
{
    public int coreModuleID;

    [SerializeField, Range(0, 2.0f)]
    private float floatingSpeed = 0.25f;

    private Vector2 floatingDirection = Vector2.up;
    public bool isUp = true;

    [SerializeField, Range(0, 2.0f)]
    private float dirChangeTimer = 1.0f;
    private float curTimer = 0;

    GameObject moduleInfoPanel;
    CanvasGroup CG;
    GameObject moduleInfoPanel_C;

    GameObject titleTxtObj;
    TextMeshProUGUI titleText;
    GameObject subTxtObj;
    TextMeshProUGUI subText;

    private void Start()
    {
        SetScript();
        // 패널 찾기
        moduleInfoPanel = GameObject.Find("CoreModuleInfo");
        if (moduleInfoPanel == null) { Debug.LogError("CoreModuleInfo 패널을 찾을 수 없습니다!"); return; }
        else { Debug.Log("CoreModuleInfo 패널 찾음"); }

        moduleInfoPanel_C = GameObject.Find("CoreModueInfo C");
        if (moduleInfoPanel_C == null) { Debug.LogError("CoreModueInfo C 패널을 찾을 수 없습니다!"); return; }
        else { Debug.Log("CoreModueInfo C 패널 찾음"); }

        CG = moduleInfoPanel.GetComponent<CanvasGroup>();
        if (CG == null) { Debug.LogError("CanvasGroup 컴포넌트를 찾을 수 없습니다!"); return; }

        CG.alpha = 0;

        // EventTrigger 추가 및 콜백 연결
        AddEventTrigger(moduleInfoPanel, OnPanelClick);
        AddEventTrigger(moduleInfoPanel_C, OnPanelClick);

        // 텍스트 오브젝트 찾기
        titleTxtObj = GameObject.Find("CoreModule Title");
        if (titleTxtObj == null) { Debug.LogError("CoreModule Title 텍스트 오브젝트를 찾을 수 없습니다!"); return; }
        titleText = titleTxtObj.GetComponent<TextMeshProUGUI>();
        if (titleText == null) { Debug.LogError("CoreModule Title 텍스트 컴포넌트를 찾을 수 없습니다!"); return; }

        subTxtObj = GameObject.Find("CoreModule SubTxt");
        if (subTxtObj == null) { Debug.LogError("CoreModule SubTxt 텍스트 오브젝트를 찾을 수 없습니다!"); return; }
        subText = subTxtObj.GetComponent<TextMeshProUGUI>();
        if (subText == null) { Debug.LogError("CoreModule SubTxt 텍스트 컴포넌트를 찾을 수 없습니다!"); return; }

        moduleInfoPanel.SetActive(false);
        Debug.Log("Start() 완료");
    }

    // EventTrigger 추가 함수
    private void AddEventTrigger(GameObject target, UnityEngine.Events.UnityAction<PointerEventData> callback)
    {
        EventTrigger ET = target.GetComponent<EventTrigger>();
        if (ET == null)
        {
            ET = target.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { callback((PointerEventData)data); });

        ET.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        curTimer += Time.deltaTime;

        if (curTimer >= dirChangeTimer)
        {
            isUp = !isUp;
            curTimer = 0;
        }

        if (isUp)
        {
            floatingDirection = Vector2.up;
        }
        else
        {
            floatingDirection = Vector2.down;
        }

        this.gameObject.transform.Translate(floatingDirection * floatingSpeed * Time.deltaTime);
    }

    private void LoadModuleInfo()
    {

        titleText.text = title[coreModuleID];
        subText.text = sub[coreModuleID];

        if (!moduleInfoPanel.activeSelf)
        {
            
            moduleInfoPanel.SetActive(true);
            CG.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                Time.timeScale = 0;
            });
            
        }
        else
        {
            CG.DOFade(1.0f, 2.0f); // 이미 활성화된 경우 페이드인
        }
    }

    public void OnPanelClick(PointerEventData data)
    {
        Time.timeScale = 1;
        SoundManager.instance.GetSFX_BGM().Play();
        CG.DOFade(0, 1.0f).OnComplete(() => { moduleInfoPanel.SetActive(false); }); // 페이드 아웃 후 비활성화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.GetSFX_BGM().Pause();
            SoundManager.instance.GetSFX_CoreModureGet().Play();
            LoadModuleInfo();

            switch (coreModuleID)
            {
                case 0:
                    GameManager.Instance.Set_Meta_max(1);
                    GameManager.Instance.Changer_Meta_Current(1);
                    GameManager.Instance.SetMetaChargingSpeed(2f);
                    break;

                case 1:
                    GameManager.Instance.Set_life_max(1);
                    GameManager.Instance.Set_life_indicator(1);
                    break;
                case 2:
                    GameManager.Instance.Changer_Meta_Current(2);
                    GameManager.Instance.Set_Meta_max(2);
                    break;
                case 3:
                    GameManager.Instance.SetMetaChargingSpeed(3.5f);
                    break;
                case 4:
                    GameManager.Instance.SetMetaChargingSpeed(5f);
                    break;
                case 5:
                    GameManager.Instance.Changer_Meta_Current(3);
                    GameManager.Instance.Set_Meta_max(3);
                    break;
                case 6:
                    GameManager.Instance.Set_life_max(2);
                    GameManager.Instance.Set_life_indicator(2);
                    break;
                case 7:
                    BossAction.instance.MakePlayerGun();
                    break;
                
                
            }

            
            Destroy(gameObject);

        }
    }


    private string[] title = new string[10];
    private string[] sub = new string[10];

    private void SetScript()
    {
        //Stage 2 Get
        title[0] = "< 코어모듈 >\r\n 메타큐브 제너레이터\r\n Rank : Prototype";
        sub[0] = "메타큐브를 자동으로 생성해주는 코어모듈. 초기 버전이기때문에 생성시간이 느리다.\r\n\r\n 메타큐브 보유 +1\r\n메타큐브 충전속도 : 1.2개/분";

        title[1] = "< 코어모듈 >\r\n 라이프 가드\r\n Rank : Advanced";
        sub[1] = "사용자의 생명에 치명적인 위험이 발생했을 때 긴급 회복을 발동하는 코어모듈.\r\n\r\n 스테이지 라이프 보유 +1\r\n";

        title[2] = "< 코어모듈 >\r\n 메타큐브 배터리\r\n Rank : Regular";
        sub[2] = "메타큐브 보유량을 1칸 증가시켜주는 코어모듈.\r\n\r\n 메타큐브 보유 +1";

        title[3] = "< 코어모듈 >\r\n 제너레이터 2000\r\n Rank : Regular";
        sub[3] = "이전 단계의 제너레이터보다 개선된 코어모듈 모델이다. 더 빠르게 메타큐브를 충전한다.\r\n\r\n 메타큐브 충전속도 : 1.8개/분";

        title[4] = "< 코어모듈 >\r\n 제너레이터 3000\r\n Rank : Advanced";
        sub[4] = "이전 단계의 제너레이터보다 더욱 개선된 코어모듈 모델이다. 더 빠르게 메타큐브를 충전한다.\r\n\r\n 메타큐브 충전속도 : 3.0개/분";

        title[5] = "< 코어모듈 >\r\n 메타큐브 배터리 Ver2\r\n Rank : Advanced";
        sub[5] = "메타큐브 보유량을 1칸 증가시켜주는 코어모듈.\r\n\r\n 메타큐브 보유 +2\r\n";

        title[6] = "< 코어모듈 >\r\n 세컨드 라이프\r\n Rank : Ultimate";
        sub[6] = "사용자의 생명에 치명적인 위험이 발생했을 때 긴급 회복을 발동하는 코어모듈.\r\n\r\n 스테이지 라이프 보유 2\r\n";

        title[7] = "< 코어모듈 >\r\n 오토 에너지건\r\n Rank : Advanced";
        sub[7] = "적을 발견하면 자동조준하여 소탕한다. 대기중의 원자를 이용하기 때문에 탄환의 제한이 없다.";


    }

}
