using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetaUI : MonoBehaviour
{
    GameObject metaBarObj;
    Image barImg;

    GameObject MetaTxtObj;
    TextMeshProUGUI metaTxt;

    private float metaChargingSpeed = 0;

    private int curMeta;
    private int maxMeta;
    private int initMeta;

    private float maxBar = 100.0f;
    private float curBar = 0.0f;
    private float fill;

    private bool isChargeActive = false;

    public void RegisterMetaVariables(GameObject bar, GameObject txt)
    {
        metaBarObj = bar;
        MetaTxtObj = txt;

        barImg = metaBarObj.GetComponent<Image>();
        metaTxt = MetaTxtObj.GetComponent<TextMeshProUGUI>();

    }

    private void writeTxt(string txt)
    {
        metaTxt.text = txt;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxMeta = GameManager.Instance.Get_Meta_Max();
        initMeta = GameManager.Instance.Get_Meta_Init();
        curMeta = initMeta;

        writeTxt(curMeta.ToString());
        fill = curBar / maxBar;
    }

    // Update is called once per frame
    void Update()
    {
        metaChargingSpeed = GameManager.Instance.GetMetaChargingSpeed();
        //Debug.Log($"cur:{curMeta}/max:{maxMeta}/init:{initMeta}");

        curMeta = GameManager.Instance.Get_Meta_Current();
        writeTxt(curMeta.ToString());

        if (curMeta <= maxMeta)
        {
            isChargeActive = true;
        }
        else
        {
            isChargeActive = false;
        }




        if (isChargeActive)
        {
            if(GameManager.Instance.Get_Meta_Current() == GameManager.Instance.Get_Meta_Max())
            {
                curBar = 0;
            }
            else
            {
                curBar += metaChargingSpeed * Time.deltaTime;
            }

            if(curBar > maxBar)
            {
                curBar = 0;
                GameManager.Instance.Changer_Meta_Current(+1);
            }
        }
        

        
        fill = curBar / maxBar;
        barImg.fillAmount = fill;

        
    }
}
        
