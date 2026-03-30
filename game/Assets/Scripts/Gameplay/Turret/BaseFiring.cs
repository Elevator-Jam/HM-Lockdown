using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BaseFiring : MonoBehaviour, IFire
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firepoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletCount;
    [SerializeField] float bulletDelayAmount;
    [SerializeField] int cooldownInSeconds;
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet prefab material color to a random color.
        Material bulletMat = bullet.GetComponent<Renderer>().material;
        bulletMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 0.75f, 1);

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
}
