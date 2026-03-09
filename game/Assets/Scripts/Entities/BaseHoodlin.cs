using UnityEngine;
using System.Collections;
public class BaseHoodlin : MonoBehaviour, IEntity
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] int health = 100;
    [SerializeField] int damage = 5;
    [SerializeField] float speed = 5f;
    [SerializeField] int attackCooldownInSeconds;
    bool canAttack = false;
    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void SetState()
    {

    }
    public void DropScrap()
    {

    }
    public void Move()
    {
        var distanceFromTarget = new Vector2(target.position.x, rb2D.position.y);
        Vector2 pos = Vector2.MoveTowards(rb2D.position, distanceFromTarget, speed * Time.fixedDeltaTime);

        rb2D.MovePosition(pos);
    }
    public void Attack()
    {
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<HealthBar>().TakeDamage(damage);
    }

    IEnumerator AttackCooldown()
    {
        while (canAttack)
        {
            yield return new WaitForSeconds(attackCooldownInSeconds);
            Attack();
        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
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
