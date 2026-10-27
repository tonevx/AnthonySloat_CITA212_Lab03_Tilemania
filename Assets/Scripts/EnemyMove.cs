using UnityEngine;

public class EmenyMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRB;
    void Start()
    {
       myRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        myRB.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRB.linearVelocity.x)), 1f);
    }
}
