using UnityEngine;

public class Deadzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.Damage();
            player.Die();
            GameManager.Instance.RespawnPlayer();
        }

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
            enemy.Die();
    }
}
