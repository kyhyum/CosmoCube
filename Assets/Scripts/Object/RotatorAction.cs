using UnityEngine;

public class RotatorAction : MonoBehaviour
{
    public GameObject railObj;
    Transform coreTrans;
    Transform railTrans;

    private float coreRotateSpeed = 100.0f;
    private float railRotateSpeed = 100.0f;

    private bool rotateDir; // true = clockwise


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coreTrans = transform;
        railTrans = transform.GetChild(0);
        rotateDir = false;

        railObj = railTrans.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotateDir)
        {
            this.gameObject.transform.Rotate(coreRotateSpeed * Vector3.forward * Time.deltaTime);
            railObj.transform.Rotate(railRotateSpeed * Vector3.forward * Time.deltaTime);
        }
        else
        {
            this.gameObject.transform.Rotate(-coreRotateSpeed * Vector3.forward * Time.deltaTime);
            railObj.transform.Rotate(-railRotateSpeed * Vector3.forward * Time.deltaTime);
        }
        
    }
}
