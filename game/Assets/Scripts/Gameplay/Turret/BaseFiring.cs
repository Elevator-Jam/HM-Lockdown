using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BaseFiring : MonoBehaviour, IFire
{
    [SerializeField] bool isManual = false;
    [SerializeField] private int currFirepointIdx = 0;
    
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] firepoints;

    [SerializeField] Transform firepoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletCount;
    [SerializeField] float bulletDelayAmount;
    [SerializeField] float cooldownInSeconds;

    private float lastFireTime = 0f;

    public void Fire(Vector3? targetPosition = null)
    {
        if (firepoints.Count() > currFirepointIdx){
            firepoint = firepoints[currFirepointIdx];
        }

        Vector2 direction;
        Quaternion bulletRotation;

        if (targetPosition.HasValue)
        {
            direction = (Vector2)(targetPosition.Value - firepoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            BaseTurret turret = GetComponentInParent<BaseTurret>();
            if (turret != null && turret.GetCurrentTarget() != null)
            {
                direction = (Vector2)(turret.GetCurrentTarget().transform.position - firepoint.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bulletRotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                direction = firepoint.right;
                bulletRotation = firepoint.rotation;
            }
        }

        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, bulletRotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }
        Destroy(bullet, 3f);
    }

    IEnumerator BulletDelay()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            yield return new WaitForSeconds(bulletDelayAmount);

            if (isManual && Pointer.current != null)
            {
                Vector2 screenPos = Pointer.current.position.ReadValue();
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
                Fire(worldPos);
            }
            else
            {
                Fire();
            }
        }
    }

    void Start()
    {
        if (!isManual)
        {
            StartCoroutine(Cooldown());
        }
    }

    private void Update()
    {
        if (isManual && GameManager.Instance.gameState != GameManager.GameState.paused)
        {
            if (Pointer.current != null && Pointer.current.press.isPressed)
            {
                if (Time.time >= lastFireTime + cooldownInSeconds)
                {
                    StartCoroutine(BulletDelay());
                    lastFireTime = Time.time;
                }
            }
        }
    }

    IEnumerator Cooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownInSeconds);
            StartCoroutine(BulletDelay());
        }
    }

    public void SetFirePointIdx(int idx)
    {
        currFirepointIdx = idx;
    }
}
