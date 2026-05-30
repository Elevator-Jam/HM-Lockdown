using UnityEngine;
using System.Collections;
using VContainer;
using VContainer.Unity;

public class BaseHoodlin : MonoBehaviour, IEntity
{
    private static int cnt = 0;
    [SerializeField] private int id;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject scrapPrefab;
    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] int health = 100;
    [SerializeField] int damage = 5;
    [SerializeField] float speed = 5f;
    [SerializeField] int attackCooldownInSeconds;
    [SerializeField] float rockDuration = 0.1f;
    [SerializeField] float rotationAngle = 20f;
    bool canAttack = false;

    private Quaternion backRotL, forwardRotL, backRotR, forwardRotR;
    private Quaternion neutralRot;
    private IObjectResolver _container;
    private HealthManager _healthManager;

    [Inject]
    public void Construct(IObjectResolver container, HealthManager healthManager)
    {
        _container = container;
        _healthManager = healthManager;
    }

    public static void ResetStatics()
    {
        cnt = 0;
    }

    void Start()
    {
        id = cnt;
        cnt++;
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        
        // Precalculate rotations once at the very beginning
        neutralRot = transform.rotation;
        backRotL = neutralRot * Quaternion.Euler(0, 0, -rotationAngle);
        forwardRotL = neutralRot * Quaternion.Euler(0, 0, rotationAngle);
        backRotR = neutralRot * Quaternion.Euler(0, 0, rotationAngle);
        forwardRotR = neutralRot * Quaternion.Euler(0, 0, -rotationAngle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void SetState()
    {

    }
    public void DropScrap() {
        Instantiate(scrapPrefab, transform.position, Quaternion.identity);
    }
    public void Move()
    {
        var distanceFromTarget = new Vector2(target.position.x, rb2D.position.y);
        Vector2 pos = Vector2.MoveTowards(rb2D.position, distanceFromTarget, speed * Time.fixedDeltaTime);

        rb2D.MovePosition(pos);
    }
    public void Attack()
    {
        var manager = _healthManager;
        if (manager != null)
        {
            manager.TakeDamage(damage);
        }
    }

    IEnumerator AttackCooldown()
    {
        while (canAttack)
        {
            float elapsed = 0f;
            bool targetOnLeft = target.position.x < transform.position.x;
            Quaternion backwardRot = targetOnLeft ? backRotL : backRotR;
            Quaternion forwardRot = targetOnLeft ? forwardRotL : forwardRotR;

            // Step 1: Rock backwards (Charge up)
            while (elapsed < rockDuration)
            {
                transform.rotation = Quaternion.Lerp(neutralRot, backwardRot, elapsed / rockDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.rotation = backwardRot;

            // Step 2: Swing all the way forward (The Strike)
            elapsed = 0f;
            while (elapsed < rockDuration)
            {
                transform.rotation = Quaternion.Lerp(backwardRot, forwardRot, elapsed / rockDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.rotation = forwardRot;

            // Deal damage precisely when fully leaned in!
            Attack();

            // Step 3: Return to original (Neutral)
            elapsed = 0f;
            while (elapsed < rockDuration)
            {
                transform.rotation = Quaternion.Lerp(forwardRot, neutralRot, elapsed / rockDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.rotation = neutralRot;

            // Wait the remaining cooldown time before the next attack
            float remainingCooldown = Mathf.Max(0f, attackCooldownInSeconds - (rockDuration * 3));
            yield return new WaitForSeconds(remainingCooldown);
        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            DropScrap();
            Destroy(this.gameObject);
        }
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "House")
        {
            canAttack = true;
            StartCoroutine(AttackCooldown());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "House")
        {
            canAttack = false;
        }
    }
}
