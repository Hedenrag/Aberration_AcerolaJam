using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AdditiveLoader
{
#if UNITY_EDITOR
    [SerializeField] SceneAsset asset;
    [SerializeField, Tooltip("if false add scene to build settings")] bool working;
#endif
    [SerializeField] string sceneToLoad;

    public AsyncOperation AddScene()
    {
        return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }

    public AsyncOperation RemoveScene()
    {
        return SceneManager.UnloadSceneAsync(sceneToLoad, UnloadSceneOptions.None);
    }

}