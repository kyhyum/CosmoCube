using UnityEngine;
using UnityEngine.UI;

public class PlayerO2 : MonoBehaviour
{
    private float curO2;
    private float maxO2;

    private float AutoDecreaseAmount = 5f;

    private float preFill;
    private float postFill;

    private Image O2Bar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxO2 = GameManager.Instance.GetPlayerMaxO2();
        curO2 = GameManager.Instance.GetPlayerCurrentO2();

        preFill = curO2 / maxO2;
        postFill = preFill;

        O2Bar = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Get_LoadingSceneToggle() == false)
        {

            if (GameManager.Instance.GetPlayerCurrentO2() > 0)
            {
                GameManager.Instance.Set_isO2zero(false);
            }
            else
            {
                GameManager.Instance.Set_isO2zero(true);
            }

            preFill = GameManager.Instance.GetPlayerCurrentO2() / GameManager.Instance.GetPlayerMaxO2();
            GameManager.Instance.Decrease_Player_O2(AutoDecreaseAmount * Time.deltaTime);
            postFill = GameManager.Instance.GetPlayerCurrentO2() / GameManager.Instance.GetPlayerMaxO2();

            O2Bar.fillAmount = postFill;
        }
        else
        {

        }


    }
}
