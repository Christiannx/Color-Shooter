using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField] new Rigidbody2D rigidbody;
    [Header("Details")]
    [SerializeField] float health;
    [SerializeField] float damage;
    [SerializeField] float moveSpeed;
    [Header("Weapon")]
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponHead;
    [SerializeField] Sprite[] weaponHeadSprites;
    [SerializeField] int rotationOffset = -45;
    [Header("Projectile")]
    [SerializeField] Projectile projectile;
    [SerializeField] float projectileSpeed;

    Vector2 movement;
    MyColor color;
 
    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Mathf.Atan2(mousePos.y - weapon.transform.position.y, mousePos.x - weapon.transform.position.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Shoot();
        }
    }

    void FixedUpdate() {
        var new_position = rigidbody.position + movement * moveSpeed * Time.deltaTime;
        new_position.x = Mathf.Clamp(new_position.x, -8.4f, 8.4f);
        new_position.y = Mathf.Clamp(new_position.y, -4.5f, 4.5f); 
        rigidbody.MovePosition(new_position);
    }

    public void SetColor(MyColor color) {
        this.color = color;
        switch(color) {
            case MyColor.red: weaponHead.GetComponent<SpriteRenderer>().sprite = weaponHeadSprites[0]; break;
            case MyColor.blue: weaponHead.GetComponent<SpriteRenderer>().sprite = weaponHeadSprites[1]; break;
            case MyColor.green: weaponHead.GetComponent<SpriteRenderer>().sprite = weaponHeadSprites[2]; break;
        }
    }

    public void TakeDamage(float amount) {
        if (amount < health) {
            health -= amount;
        } else {
            Destroy(gameObject);
        }
    }

    void Shoot() {
        var projectileInstance = Instantiate(projectile, weaponHead.transform.position, weaponHead.transform.rotation);
        projectileInstance.SetColor(color);
        projectileInstance.GetComponent<Rigidbody2D>().AddForce(weaponHead.transform.up * projectileSpeed, ForceMode2D.Impulse);
        projectileInstance.damage = damage;
    }
}
