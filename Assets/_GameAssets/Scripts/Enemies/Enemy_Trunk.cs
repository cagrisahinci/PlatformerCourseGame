using UnityEngine;

public class Enemy_Trunk : Enemy
{
    [Header("Trunk Details")]
    [SerializeField] private Enemy_Bullet bulletPrefab;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private float bulletSpeed = 7f;
    [SerializeField] private float attackCooldown = 1.5f;
    private float lastTimeAttacked;

    protected override void Update()
    {
        base.Update();

        if (isDead)
            return;

        bool canAttack = Time.time > lastTimeAttacked + attackCooldown;

        if (isPlayerDetected && canAttack)
            Attack();

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
    
    private void Attack()
    {
        idleTimer = idleDuration + attackCooldown;
        lastTimeAttacked = Time.time;
        anim.SetTrigger("attack");
    }

    private void CreateBullet()
    {
        Enemy_Bullet newBullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
        Vector2 bulletVelocity = new Vector2(facingDir * bulletSpeed, 0);
        newBullet.SetVelocity(bulletVelocity);

        if (facingDir == 1)
            newBullet.FlipSprite();

        Destroy(newBullet.gameObject, 10);
    }
}
