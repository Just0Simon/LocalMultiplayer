using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerMainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _hostButton;
    
    [SerializeField]
    private Button _clientButton;

    [SerializeField]
    private Button _shutdownButton;

    private void Awake()
    {
        _hostButton.onClick.AddListener(OnHostButtonPressed);
        _clientButton.onClick.AddListener(OnJoinButtonPressed);
        _shutdownButton.onClick.AddListener(OnShutdownButtonPressed);
    }

    private void OnHostButtonPressed()
    {
        NetworkManager.Singleton.StartHost();
    }

    private void OnJoinButtonPressed()
    {
        NetworkManager.Singleton.StartClient();
    }

    private void OnShutdownButtonPressed()
    {
        NetworkManager.Singleton.Shutdown();
    }

    private void OnDestroy()
    {
        _hostButton.onClick.RemoveAllListeners();
        _clientButton.onClick.RemoveAllListeners();
        _shutdownButton.onClick.RemoveAllListeners();
    }
}
