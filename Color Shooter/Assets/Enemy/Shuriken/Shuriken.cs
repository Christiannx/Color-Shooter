using UnityEngine;

public class Shuriken : MonoBehaviour {
    [HideInInspector] public float speed;
    [HideInInspector] public float damage;

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.GetComponent<Enemy>()) {
            other.GetComponent<Player>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
