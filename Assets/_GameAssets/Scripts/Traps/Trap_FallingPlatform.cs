using System.Collections;
using UnityEngine;

public class Trap_FallingPlatform : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D[] colliders;

    [SerializeField] private float travelDistance;
    [SerializeField] private float speed = 0.75f;
    private Vector3[] waypoints;
    private int waypointIndex;
    private bool canMove = false;

    [Header("Platform fall details")]
    [SerializeField] private float impactSpeed = 3f;
    [SerializeField] private float impactDuration = .1f;
    [SerializeField] private float fallDelay = .5f;
    private float impactTimer;
    private bool impactHappened;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<BoxCollider2D>();
    }

    private IEnumerator Start()
    {
        SetupWaypoints();
        float randomDelay = Random.Range(0, 0.6f);

        yield return new WaitForSeconds(randomDelay);

        canMove = true;
    }

    private void Update()
    {
        HandleMovement();
        HandleImpact();
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex], speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, waypoints[waypointIndex]) < .1f)
        {
            waypointIndex++;

            if (waypointIndex >= waypoints.Length)
                waypointIndex = 0;
        }
    }

    private void HandleImpact()
    {
        if (impactTimer < 0)
            return;

        impactTimer -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3.down * 10), impactSpeed * Time.deltaTime);
    }

    private void SetupWaypoints()
    {
        waypoints = new Vector3[2];

        float yOffset = travelDistance / 2;

        waypoints[0] = transform.position + new Vector3(0, yOffset, 0);
        waypoints[1] = transform.position + new Vector3(0, -yOffset, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (impactHappened)
            return;

        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Invoke(nameof(SwitchOffPlatform), fallDelay);
            impactTimer = impactDuration;
            impactHappened = true;
        }
    }

    private void SwitchOffPlatform()
    {
        anim.SetTrigger("deactivate");

        canMove = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3.5f;
        rb.linearDamping = .5f;

        foreach (BoxCollider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
