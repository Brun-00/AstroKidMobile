using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance { get; private set; }

    [Header("Referências")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Configuração")]
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        // Keep only one SceneFader instance across scenes.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Preserve the scene fader when loading new scenes.
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        // Clear the singleton reference when this instance is destroyed.
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        // Start with the screen fully visible and input enabled.
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void LoadSceneWithFade(string sceneName)
    {
        // Load a scene using its name after fading out.
        StartCoroutine(FadeAndLoad(sceneName, -1));
    }

    public void LoadSceneWithFade(int sceneIndex)
    {
        // Load a scene using its build index after fading out.
        StartCoroutine(FadeAndLoad(null, sceneIndex));
    }

    private IEnumerator FadeAndLoad(string sceneName, int sceneIndex)
    {
        // Fade to black before changing scenes.
        yield return StartCoroutine(Fade(1f));

        AsyncOperation asyncLoad = !string.IsNullOrEmpty(sceneName)
            ? SceneManager.LoadSceneAsync(sceneName)
            : SceneManager.LoadSceneAsync(sceneIndex);

        // Wait until the new scene has finished loading.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return null;

        // Fade back in after the new scene is loaded.
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        // Prevent interaction while the screen is fading.
        canvasGroup.blocksRaycasts = true;

        while (elapsed < fadeDuration)
        {
            // Use unscaled time so the fade also works while paused.
            float delta = Mathf.Min(Time.unscaledDeltaTime, 0.05f);

            elapsed += delta;

            canvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                elapsed / fadeDuration
            );

            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        // Only block input while the screen is fully covered.
        canvasGroup.blocksRaycasts = targetAlpha > 0f;
    }
}