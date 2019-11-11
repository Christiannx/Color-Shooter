using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponHead;
    [SerializeField] Sprite[] weaponHeadSprites;

    Vector2 movement;
    MyColor color;

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Mathf.Atan2(mousePos.y - weapon.transform.position.y, mousePos.x - weapon.transform.position.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0, 0, angle - 45);
    }

    void FixedUpdate() {
        rigidbody.MovePosition(rigidbody.position + movement * moveSpeed * Time.deltaTime);
    }

    public void SetColor(MyColor color) {
        this.color = color;
        switch(color) {
            case MyColor.red: weaponHead.GetComponent<SpriteRenderer>().sprite = weaponHeadSprites[0]; break;
            case MyColor.blue: weaponHead.GetComponent<SpriteRenderer>().sprite = weaponHeadSprites[1]; break;
            case MyColor.green: weaponHead.GetComponent<SpriteRenderer>().sprite = weaponHeadSprites[2]; break;
        }
    }
}
