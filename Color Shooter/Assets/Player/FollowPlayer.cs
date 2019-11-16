using UnityEngine;

public class FollowPlayer : MonoBehaviour{
    [SerializeField] float speed;
    [SerializeField] float maxDistanceToPlayer;

    Transform player;

    void Start() => player = FindObjectOfType<Player>().transform;

    void Update() {
        if (Vector2.Distance(transform.position, player.position) > maxDistanceToPlayer)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}