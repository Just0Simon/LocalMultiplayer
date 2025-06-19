using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerMainMenuController : MonoBehaviour
{
    [SerializeField]
    private MultiplayerMainMenuView _view;

    private void Awake()
    {
        _view.OnHost += OnHost;
        _view.OnClient += OnClient;
        _view.OnShutdown += OnShutdown;
    }

    private void OnHost()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnServerStopped += OnServerStopped;
        NetworkManager.Singleton.StartHost();
    }

    private void OnServerStopped(bool obj)
    {
        SceneManager.UnloadSceneAsync(Constants.GAMEPLAY_SCENE);
    }

    private void OnServerStarted()
    {
        NetworkManager.Singleton.SceneManager.LoadScene(Constants.GAMEPLAY_SCENE, LoadSceneMode.Additive);
        
        NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
    }
    
    private void OnClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    private void OnShutdown()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.UnloadSceneAsync(Constants.GAMEPLAY_SCENE);
    }

    private void OnDestroy()
    {
        if(_view is null)
            return;
        
        _view.OnHost -= OnHost;
        _view.OnClient -= OnClient;
        _view.OnShutdown -= OnShutdown;
    }
}
