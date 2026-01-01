using UnityEngine;
using UnityEngine.Audio;

public class BladeFlower : MonoBehaviour
{

    GameObject Blade_Open;
    Transform Blade_Open_Transform;
    GameObject Blade_Close;
    Transform Blade_Close_Transform;


    private bool isOpen= false;
    public float curTime = 0;
    public float midTime = 2.0f;
    public float maxTime = 4f;

    private float bladeRotateSpeed = 300;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (gameObject.name.Contains("Close"))
        {
            //curTime = 1.0f;
        }
        else
        {
            isOpen = true;
        }


        Blade_Open_Transform = this.gameObject.transform.GetChild(0);
        Blade_Close_Transform = this.gameObject.transform.GetChild(1);

        Blade_Open = Blade_Open_Transform.gameObject;
        Blade_Close = Blade_Close_Transform.gameObject;

        if (isOpen)
        {
            
            Blade_Open.SetActive(true);
            Blade_Close.SetActive(false);
        }
        else
        {
            
            Blade_Open.SetActive(false);
            Blade_Close.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        curTime += Time.deltaTime;
        if(curTime < midTime)
        {
            Blade_Open.SetActive(true);
            Blade_Close.SetActive(false);
           
            Blade_Open.transform.Rotate(bladeRotateSpeed * Time.deltaTime * Vector3.forward);
        }else if(curTime >= midTime && curTime < maxTime)
        {
            
            Blade_Open.SetActive(false);
            Blade_Close.SetActive(true);
        }
        else
        {
            
            curTime = 0;
        }

    }

}
