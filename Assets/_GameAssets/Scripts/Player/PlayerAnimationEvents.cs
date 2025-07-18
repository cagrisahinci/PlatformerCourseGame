using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;

    private void Awake() 
    {
        player = GetComponentInParent<Player>();    
    }

    public void FinishRespawn() => player.RespawnedFinished(true);
}
