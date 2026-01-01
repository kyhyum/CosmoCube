using UnityEngine;
using DG.Tweening;
public class LinerAutoDeath : MonoBehaviour
{
    private float curTime = 0;
    private float death = 0.2f;
    
    private void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        if(curTime > death)
        {
            Destroy(gameObject);
        }
    }
}
