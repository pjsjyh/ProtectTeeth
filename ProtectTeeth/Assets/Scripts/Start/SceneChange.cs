using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static SceneChange Instance { get; private set; }
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1.0f;
    void Awake()
    {
        // Singleton ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� ����
        }
    }
    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    // Fade Out -> �� �ε� -> Fade In
    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // Fade Out
        yield return StartCoroutine(FadeOut(fadeDuration));

        // �� �񵿱� �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Fade In
        yield return StartCoroutine(FadeIn(fadeDuration));
    }
    private IEnumerator FadeOut(float duration)
    {
        float startAlpha = fadeCanvasGroup.alpha;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1, t / duration); // ������ ���� ȭ��
            yield return null;
        }

        fadeCanvasGroup.alpha = 1; // ������ ��ο���
    }

    private IEnumerator FadeIn(float duration)
    {
        float startAlpha = fadeCanvasGroup.alpha;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, t / duration); // ȭ���� �����
            yield return null;
        }

        fadeCanvasGroup.alpha = 0; // ������ ���� ����
    }
}
