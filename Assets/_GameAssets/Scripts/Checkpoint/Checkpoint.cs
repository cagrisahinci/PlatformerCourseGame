using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private bool active;

    [SerializeField] private bool canBeReactivated;

    private void Start()
    {
        canBeReactivated = GameManager.Instance.canReactivate;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active && canBeReactivated == false)
            return;

        Player player = collision.GetComponent<Player>();

        if(player!= null)
        {
            ActivateCheckpoint();
        }
    }

    private void ActivateCheckpoint()
    {
        active = true;
        anim.SetTrigger("activate");
        GameManager.Instance.UpdateRespawnPosition(transform);
    }
}
