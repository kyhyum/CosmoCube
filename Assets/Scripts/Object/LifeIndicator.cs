using UnityEngine;

public class LifeIndicator : MonoBehaviour
{
    GameObject lifeback1;
    GameObject lifeimg1;
    GameObject lifeback2;
    GameObject lifeimg2;

    public LifeIndicator(GameObject back1, GameObject img1, GameObject back2, GameObject img2)
    {
        lifeback1 = back1;
        lifeimg1 = img1;
        lifeback2 = back2;
        lifeimg2 = img2;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
