using UnityEngine;

public class Enemy_Mushroom : Enemy
{

    protected override void Update()
    {
        base.Update();

        if (isDead)
            return;

        HandleMovement();
        if (isGrounded)
            HandleTurnAround();
    }

    private void HandleTurnAround()
    {
        if (!isGroundInFrontDetected || isWallDetected)
        {
            Flip();
            idleTimer = idleDuration;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if (idleTimer > 0)
            return;

        rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocityY);
    }

    protected override void Flip()
    {
        base.Flip();
    }
}
