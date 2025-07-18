using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public int choosenSkinId;
    public static SkinManager Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetSkinId(int id) => choosenSkinId = id;
    public int GetSkinId() => choosenSkinId;
}
