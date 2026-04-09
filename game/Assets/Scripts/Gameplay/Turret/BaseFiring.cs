using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class BaseFiring : MonoBehaviour, IFire
{
    [SerializeField] private static int currFirepointIdx = 0;
    public static void ResetStatics()
    {
        currFirepointIdx = 0;
    }
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] firepoints;

    [SerializeField] Transform firepoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletCount;
    [SerializeField] float bulletDelayAmount;
    [SerializeField] int cooldownInSeconds;
    public void Fire()
    {
        if (firepoints.Count() > currFirepointIdx){
            firepoint = firepoints[currFirepointIdx];
        }
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(firepoint.right * bulletSpeed, ForceMode2D.Impulse);
        }
        Destroy(bullet, 3f);
        
    }
    IEnumerator BulletDelay()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            yield return new WaitForSeconds(bulletDelayAmount);
            Fire();
        }
    }
    void Start()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownInSeconds);
            StartCoroutine(BulletDelay());
        }
    }

    public static void SetFirePointIdx(int idx)
    {
        currFirepointIdx = idx;
    }
}
