using UnityEngine;

public class Trap_Trampoline : MonoBehaviour
{
    protected Animator anim;
    [SerializeField] private float pushPower = 25f;
    [SerializeField] private float duration = 0.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.Push(transform.up * pushPower, duration);
            anim.SetTrigger("activate");
        }
    }
}
