using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] public MyColor color;
    [SerializeField] float health;
    [Header("Shuriken")]
    [SerializeField] Shuriken shuriken;
    [SerializeField] float damage;
    [SerializeField] float shurikenSpeed;
    [SerializeField] float fireRate;

    Transform target;
    public static int count = 0;

    void Start() {
        target = FindObjectOfType<Player>().transform;
        InvokeRepeating(nameof(Shoot), fireRate + Random.Range(-0.5f, 0.5f), fireRate);
        count++;
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
        count--;
        Destroy(gameObject);
    }
}
