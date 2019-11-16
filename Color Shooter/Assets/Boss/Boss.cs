using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    [SerializeField] float shield;
    [SerializeField] float health;
    [Header("Shuriken")]
    [SerializeField] Shuriken shuriken;
    [SerializeField] float shurikenMoveSpeed;
    [SerializeField] float shootTime;
    [Header("Belt")]
    [SerializeField] SpriteRenderer belt;
    [SerializeField] Sprite[] belts;
    [SerializeField] Sprite flare;
    [SerializeField] GameObject healthBar;

    [HideInInspector] public MyColor color;
    Transform player;
    bool shieldActive = true;
    Slider healthBarSlider;

    void Start() {
        player = FindObjectOfType<Player>().transform;

        InvokeRepeating(nameof(ShootMultiple), shootTime, shootTime);
        InvokeRepeating(nameof(ShootSingle), shootTime * 1.5f, shootTime);

        color = (MyColor) Random.Range(0, 3);
        belt.sprite = belts[(int)color];

        var healthbarInstance = Instantiate(healthBar, Vector3.zero, Quaternion.identity, FindObjectOfType<Canvas>().transform);
        healthBarSlider = healthbarInstance.GetComponentInChildren<Slider>();
        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;
    }
    
    public void TakeDamage(float amount, MyColor color) {
        if (shieldActive) {
            if (this.color == color) {
                if (shield > amount)
                    shield -= amount;
                else {
                    shieldActive = false;
                    belt.sprite = flare;
                }
            }
        } else {
            if (health > amount) {
                health -= amount;
                healthBarSlider.value = health;
            } else {
                healthBarSlider.value = 0;
                Destroy(gameObject);
                Destroy(healthBarSlider.transform.parent.gameObject);
            }
        }
    } 

    void ShootMultiple() {
        var directions = new Vector2[] {
            new Vector2(0,      1),
            new Vector2(0,     -1),
            new Vector2(0.5f,   0.5f),
            new Vector2(0.5f,  -0.5f),
            new Vector2(-0.5f,  0.5f),
            new Vector2(-0.5f, -0.5f),
            new Vector2(1,      0),
            new Vector2(-1,     0),
        };

        foreach (var direction in directions) {
            var instance = Instantiate(shuriken, transform.position, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shurikenMoveSpeed, ForceMode2D.Impulse);
        }
    }
    
    void ShootSingle() {
        var instance = Instantiate(shuriken, transform.position, Quaternion.identity);
        var direction = player.position - transform.position;
        instance.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shurikenMoveSpeed * 1.5f, ForceMode2D.Impulse);
    }
}