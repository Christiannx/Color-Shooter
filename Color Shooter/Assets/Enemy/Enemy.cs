using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] public MyColor color;
    [Space]
    [SerializeField] float health;
    [SerializeField] float movementSpeed;
    [SerializeField] float maxDistanceToPlayer = 1.5f;
    [Header("Shuriken")]
    [SerializeField] Shuriken shuriken;
    [SerializeField] float damage;
    [SerializeField] float shurikenSpeed;
    [SerializeField] float fireRate;

    Transform target;

    void Start() {
        target = FindObjectOfType<Player>().transform;
        InvokeRepeating(nameof(Shoot), fireRate + Random.Range(-0.5f, 0.5f), fireRate);
    }

    void Update() {
        if (Vector2.Distance(transform.position, target.position) > maxDistanceToPlayer)
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
    }

    public void Shoot() {
        var instance = Instantiate(shuriken, transform.position, Quaternion.identity);
        instance.damage = damage;

        var direction = (target.position - transform.position).normalized;
        instance.GetComponent<Rigidbody2D>().AddForce(direction * shurikenSpeed, ForceMode2D.Impulse);
    }

    public void TakeDamage(float amount) {
        if (amount < health) {
            health -= amount;
        } else {
            Die();    
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
