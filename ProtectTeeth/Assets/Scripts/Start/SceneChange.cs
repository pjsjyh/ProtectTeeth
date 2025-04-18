using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static SceneChange Instance { get; private set; }
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1.0f;
    void Awake()
    {
        // Singleton 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }
    }
    public void FadeAndLoadScene(string sceneName, string sceneAddress="")
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName, sceneAddress));
    }

    // Fade Out -> 씬 로드 -> Fade In
    private IEnumerator FadeOutAndLoadScene(string sceneName, string sceneAddress="")
    {
        // Fade Out
        yield return StartCoroutine(FadeOut(fadeDuration));

        // 씬 비동기 로드
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAddress);
        yield return handle;
        //while (!asyncLoad.isDone)
        //{
        //    yield return null;
        //}
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("❌ Addressables 씬 로드 실패: " + sceneAddress);
        }
        // Fade In
        yield return StartCoroutine(FadeIn(fadeDuration));
    }
    private IEnumerator FadeOut(float duration)
    {
        float startAlpha = fadeCanvasGroup.alpha;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1, t / duration); // 완전히 검은 화면
            yield return null;
        }

        fadeCanvasGroup.alpha = 1; // 완전히 어두워짐
    }

    private IEnumerator FadeIn(float duration)
    {
        float startAlpha = fadeCanvasGroup.alpha;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, t / duration); // 화면이 밝아짐
            yield return null;
        }

        fadeCanvasGroup.alpha = 0; // 완전히 밝은 상태
    }
}
