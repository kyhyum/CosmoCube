using System.Security.Cryptography;
using UnityEngine;

public class AutoReturn : MonoBehaviour
{
    public PoolItemType poolItemType;
    public float lifeTime;
    private bool autoReturn;
    private float timer = 0;

    void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        if (autoReturn)
        {
            timer += Time.deltaTime;
            if (timer >= lifeTime)
            {
                ObjectPoolManager.Instance.ReturnToPool(poolItemType, gameObject);
                autoReturn = false; // 다시 실행 안 되게
            }
        }
    }
}
