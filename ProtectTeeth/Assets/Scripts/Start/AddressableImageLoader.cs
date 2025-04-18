using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;

public static class AddressableImageLoader
{
    public static void SetImageFromAddress(Image image, string address)
    {
        Addressables.LoadAssetAsync<Sprite>(address).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                image.sprite = handle.Result;
            }
        };
    }
    public static void SetRawImageFromAddress(RawImage rawImage, string address)
    {
        Addressables.LoadAssetAsync<Texture>(address).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                rawImage.texture = handle.Result;
            }
        };
    }
    public static void LoadScene(string address, LoadSceneMode mode = LoadSceneMode.Single)
    {
        Addressables.LoadSceneAsync(address, mode).Completed += OnSceneLoaded;
    }
    public static void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("✅ 씬 로드 완료!");
        }
        else
        {
            Debug.LogError("❌ 씬 로드 실패!");
        }
    }
}
