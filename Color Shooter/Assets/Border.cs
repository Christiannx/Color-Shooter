using UnityEngine;

public class Border : MonoBehaviour {
    void OnTriggerEnter2D (Collider2D other) {
        if (!other.GetComponent<Enemy>())
            Destroy(other.gameObject);
    }     
}
