using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Credits : MonoBehaviour
{
    private UI_FadeEffect fadeEffect;
    [SerializeField] private RectTransform rectT;
    [SerializeField] private float scrollSpeed = 200f;
    [SerializeField] private float offScreenPosition = 1700f;

    [SerializeField] private string mainMenuSceneName = "MainMenu";
    private bool creditsSkipped;

    private void Awake()
    {
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
        fadeEffect.ScreenFade(0, 1f);
    }

    private void Update()
    {
        rectT.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (rectT.anchoredPosition.y > offScreenPosition)
            GoToMainMenu();
    }

    public void SkipCredits()
    {
        if (creditsSkipped == false)
        {
            scrollSpeed *= 10;
            creditsSkipped = true;
        }
        else
        {
            GoToMainMenu();
        }
    }

    private void GoToMainMenu() => fadeEffect.ScreenFade(1, 1f, SwitchToMenuScene);

    private void SwitchToMenuScene()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
