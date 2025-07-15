using UnityEngine;

public class Enemy_Chicken : Enemy
{
    [Header("Chicken Details")]
    [SerializeField] private float aggroDuration;
    [SerializeField] private float detectionRange;

    [SerializeField] private float aggroTimer;
    private bool canFlip = true;

    protected override void Awake()
    {
        base.Awake();
        canMove = true;
    }

    protected override void Update()
    {
        base.Update();

        aggroTimer -= Time.deltaTime;

        if (isDead)
            return;

        if (isPlayerDetected)
        {
            canMove = true;
            aggroTimer = aggroDuration;
            Debug.Log("Player Detected");
        }

        if (aggroTimer < 0)
            canMove = false;

        HandleMovement();
        if (isGrounded)
            HandleTurnAround();
    }

    private void HandleTurnAround()
    {
        if (!isGroundInFrontDetected || isWallDetected)
        {
            Flip();
            canMove = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;
        HandleFlip(player.transform.position.x);

        
        rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocityY);
    }

    protected override void HandleFlip(float xValue)
    {
        if (xValue < transform.position.x && facingRight || xValue > transform.position.x && !facingRight)
        {
            if (canFlip)
            {
                canFlip = false;
                Invoke(nameof(Flip), .3f);
            }
        }
    }

    protected override void Flip()
    {
        base.Flip();
        canFlip = true;
    }
}
