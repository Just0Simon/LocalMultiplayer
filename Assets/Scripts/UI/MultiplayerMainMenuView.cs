using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerMainMenuView : MonoBehaviour
{
    public event Action OnHost;
    public event Action OnClient;
    public event Action OnShutdown;
    
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
        OnHost?.Invoke();
    }

    private void OnJoinButtonPressed()
    {
        OnClient?.Invoke();
    }

    private void OnShutdownButtonPressed()
    {
        OnShutdown?.Invoke();
    }

    private void OnDestroy()
    {
        _hostButton.onClick.RemoveAllListeners();
        _clientButton.onClick.RemoveAllListeners();
        _shutdownButton.onClick.RemoveAllListeners();
    }
}
