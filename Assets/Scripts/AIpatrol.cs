using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIpatrol : MonoBehaviour
{
    private Animator anim;

    GameObject scoreCounter;
    public float walkSpeed;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    public int HP = 2;
    bool isAlive;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    void Start()
    {
        mustPatrol = true;
        scoreCounter = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol == true)
        {
            Patrol();
        }
        float IsRunning = Input.GetAxis("Horizontal");
        IsRunning = Mathf.Abs(IsRunning);
        anim.SetFloat("Run", IsRunning);
    }
    private void FixedUpdate()
    {
        if (mustPatrol == true)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }
    void Patrol()
    {
        if (mustTurn == true || bodyCollider.IsTouchingLayers(groundLayer))
        {
            flip();
        }
        rb.velocity = new Vector3(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            flip();
        }
    }
    public void TakeDamage()
    {
        HP--;
        if (HP <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
        scoreCounter.GetComponent<Scores>().scoreAdd();
    }
}