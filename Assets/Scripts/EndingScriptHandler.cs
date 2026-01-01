using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScriptHandler : MonoBehaviour
{
    GameObject Panel_Front;
    GameObject[] txtobj = new GameObject[5];
    

    float fadeDuration = 0.8f;

    bool lastScripteUpMove = false;
    float ScriptMoveSpeed = 125.0f;
    float curTime = 0;
    float endTime = 13.0f;


    private void Start()
    {

        Panel_Front = GameObject.Find("Panel Front");

        

        txtobj[0] = GameObject.Find("txt 1");
        txtobj[1] = GameObject.Find("txt 2");
        txtobj[2] = GameObject.Find("txt 3");
        txtobj[3] = GameObject.Find("txt 4 A");
        txtobj[4] = GameObject.Find("txt 4 B");

        FadeActive(Panel_Front, false, fadeDuration);

        for(int i = 0; i  <5; i++)
        {
            FadeActive(txtobj[i] , false, fadeDuration-0.79f);
        }

        StartCoroutine(EndingCoroutine());
        
    }

    private void FadeActive(GameObject a, bool onOff, float time)
    {
        CanvasGroup cg_A = a.GetComponent<CanvasGroup>();
        if(cg_A == null) { cg_A = a.AddComponent<CanvasGroup>(); }

        if (onOff)
        {
            cg_A.DOFade(1f, time);
        }
        else
        {
            cg_A.DOFade(0f, time);
        }
    }

    IEnumerator EndingCoroutine()
    {
        FadeActive(txtobj[0], true, fadeDuration);
        yield return new WaitForSeconds(2.0f);
        FadeActive(txtobj[0], false, fadeDuration);
        yield return new WaitForSeconds(1.0f);
        FadeActive(txtobj[1], true, fadeDuration);
        yield return new WaitForSeconds(2.0f);
        FadeActive(txtobj[1], false, fadeDuration);
        yield return new WaitForSeconds(1.0f);
        FadeActive(txtobj[2], true, fadeDuration);
        yield return new WaitForSeconds(2.0f);
        FadeActive(txtobj[2], false, fadeDuration);
        yield return new WaitForSeconds(1.0f);

        FadeActive(txtobj[3], true, fadeDuration);
        FadeActive(txtobj[4], true, fadeDuration);

        yield return new WaitForSeconds(2.0f);
        lastScripteUpMove = true;
    }

    IEnumerator ToTheTitle()
    {
        FadeActive(txtobj[3], false, fadeDuration);
        FadeActive(txtobj[4], false, fadeDuration);

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Menu");
    }


    private void Update()
    {

        if (lastScripteUpMove)
        {
            curTime += Time.deltaTime;

            if(curTime >= endTime)
            {
                StartCoroutine(ToTheTitle());
            }

            txtobj[3].transform.Translate(ScriptMoveSpeed * Time.deltaTime * Vector2.up);
            txtobj[4].transform.Translate(ScriptMoveSpeed * Time.deltaTime * Vector2.up);
        }
    }
}
