using UnityEngine;

public class BladeBehaviour : MonoBehaviour
{
    public Transform bladeOpen;   // assign in prefab (visual that rotates when open)
    public GameObject bladeClose; // assign in prefab (visual when closed)

    bool isOpen;
    float curTime;
    float midTime;
    float maxTime;
    float rotateSpeed;

    // 초기화 호출로 파라미터 설정
    public void Initialize(bool startOpen, float curTime, float openDuration, float closeDuration, float rotateSpeed)
    {
        this.isOpen = startOpen;
        this.curTime = curTime;
        this.midTime = openDuration;
        this.maxTime = closeDuration;
        this.rotateSpeed = rotateSpeed;

        if (bladeOpen != null) bladeOpen.gameObject.SetActive(isOpen);
        if (bladeClose != null) bladeClose.SetActive(!isOpen);
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime < midTime)
        {
            if (!bladeOpen.gameObject.activeSelf) bladeOpen.gameObject.SetActive(true);
            if (bladeClose.gameObject.activeSelf) bladeClose.SetActive(false);
            if (bladeOpen != null)
                bladeOpen.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
        else if (curTime < maxTime)
        {
            if (bladeOpen.gameObject.activeSelf) bladeOpen.gameObject.SetActive(false);
            if (!bladeClose.gameObject.activeSelf) bladeClose.SetActive(true);
        }
        else
        {
            curTime = 0f;
        }
    }
}
