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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName, -1));
    }

    public void LoadSceneWithFade(int sceneIndex)
    {
        StartCoroutine(FadeAndLoad(null, sceneIndex));
    }

    private IEnumerator FadeAndLoad(string sceneName, int sceneIndex)
    {
        yield return StartCoroutine(Fade(1f));

        AsyncOperation asyncLoad = !string.IsNullOrEmpty(sceneName)
            ? SceneManager.LoadSceneAsync(sceneName)
            : SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return null;

        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        canvasGroup.blocksRaycasts = true;

        while (elapsed < fadeDuration)
        {
            float delta = Mathf.Min(Time.unscaledDeltaTime, 0.05f);
            elapsed += delta;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.blocksRaycasts = targetAlpha > 0f;
    }
}