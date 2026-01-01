using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    private float curHP;
    private float maxHP;

    private float preFill;
    private float postFIll;

    private Image HpBar;

    private bool isO2zero;

    private float NoO2DecreaseHP = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHP = GameManager.Instance.GetPlayerMaxHP();
        curHP = GameManager.Instance.GetPlayerCurrentHP();

        preFill = curHP / maxHP;
        postFIll = preFill;

        HpBar = this.gameObject.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetPlayerCurrentHP() <=0)
        {
            GameManager.Instance.Set_isHPzero(true);
        }
        else
        {
            GameManager.Instance.Set_isHPzero(false);
        }


        preFill = GameManager.Instance.GetPlayerCurrentHP() / GameManager.Instance.GetPlayerMaxHP();

        isO2zero = GameManager.Instance.Get_isO2zero();

        if (isO2zero)
        {
            GameManager.Instance.Decrease_Player_HP(NoO2DecreaseHP * Time.deltaTime);

        }

        postFIll = GameManager.Instance.GetPlayerCurrentHP() / GameManager.Instance.GetPlayerMaxHP();

        HpBar.fillAmount = postFIll;
    }
}
