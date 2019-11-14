using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] Sprite[] sprites;
    [HideInInspector] public MyColor color;
    [HideInInspector] public float damage;

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.GetComponent<Player>()) {
            if (other.GetComponent<Enemy>()?.color == color)
                other.GetComponent<Enemy>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetColor(MyColor color) {
        this.color = color;
        var renderer = GetComponent<SpriteRenderer>();
        switch (color) {
            case MyColor.red: renderer.sprite = sprites[0]; break;
            case MyColor.blue: renderer.sprite = sprites[1]; break;
            case MyColor.green: renderer.sprite = sprites[2]; break;
            default: renderer.sprite = sprites[0]; break;
        }
    }
}
