using TMPro;
using UnityEngine;

public class TextfadeINOUT : MonoBehaviour
{
    private bool isFadeOut = true;

    [SerializeField, Range(0, 3.0f)]
    private float FadeChangeTimer = 1.63f;
    private float curTimer = 0;

    private TextMeshProUGUI text;
    Color color;

    [SerializeField, Range(0, 3.0f)]
    private float FadeTensor = 0.59f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = this.gameObject.GetComponent<TextMeshProUGUI>();
        if(text == null) { Debug.Log("TextFadeINOUT : Text Null"); }

        color = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        curTimer += Time.deltaTime;

        if(curTimer >= FadeChangeTimer)
        {
            curTimer = 0;
            isFadeOut = !isFadeOut;
        }

        if (isFadeOut)
        {
            color.a -= (FadeTensor*Time.deltaTime);
            if(color.a <= 0)
            {
                color.a = 0;
            }
            text.color = color;
        }
        else
        {
            color.a += (FadeTensor * Time.deltaTime);
            if(color.a >= 1)
            {
                color.a = 1;
            }
            text.color = color;
        }
    }
}
