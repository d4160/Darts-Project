using UltEvents;
using UnityEngine.UI;

namespace GameFramework
{
    using System;
    using UnityEngine;
    using TMPro;

    public class AuthenticationController : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _usernameInfoText;
        [SerializeField] protected TextMeshProUGUI _usernameInfoTextMultiplayer;
        [SerializeField] protected TMP_InputField _loginInput;
        [SerializeField] protected Button _loginButton;
        [SerializeField] protected bool _authenticateAtStart;
        [SerializeField] protected bool _connectToChatWhenAuthenticated;

        // TODO Made changes
        [SerializeField] protected UltEvent _onAllLoginComplete;

        private void Start()
        {
            if (_authenticateAtStart)
            {
                if (DataManager.Instance.GameData.Authenticator.CanAuthenticate)
                    LoginAll();
                else
                    SetUsernameUI();
            }
            else
            {
                SetUsernameUI();
            }
        }

        private void SetUsernameUI()
        {
            if (_usernameInfoText)
                _usernameInfoText.text = $"Logged as {DataManager.Instance.GameData.Authenticator.Username}";

            if (_usernameInfoTextMultiplayer)
                _usernameInfoTextMultiplayer.text = DataManager.Instance.GameData.Authenticator.Username;
        }

        public void LoginAll()
        {
            LoginAll(() =>
            {
                SetUsernameUI();

                if (_connectToChatWhenAuthenticated)
                    NetworkingManager.Instance.Chat.Connect();
            });
        }

        public void LoginAll(Action onCompleted, Action onFailed = null)
        {
            if (_loginInput)
            {
                if (string.IsNullOrEmpty(_loginInput.text))
                {
                    NotificationPrefabsManager.Instance.InstancedMain?.Notify("Nombre está vacío...", 2f);
                    return;
                }
                
                if (_loginInput.text.Length < 2)
                {
                    NotificationPrefabsManager.Instance.InstancedMain?.Notify("Tu nombre es demasiado corto...",
                        2f);
                    return;
                }
            }

            _loginInput.interactable = false;
            _loginButton.interactable = false;

            // Since all share the same authenticator
            DataManager.Instance.GameData.Authenticator.Login(() =>
            {
                onCompleted?.Invoke();
                _onAllLoginComplete?.Invoke();

                _loginInput.interactable = true;
                _loginButton.interactable = true;
            }, () => {
                _loginInput.interactable = true;
                _loginButton.interactable = true;

                onFailed?.Invoke();
            });
        }

        public void LogoutAll()
        {
            // Since all share the same authenticator
            DataManager.Instance.GameData.Authenticator.Logout();
        }

        public void SetOverrideAuthentication(bool setValue)
        {
            // Since all share the same authenticator
            DataManager.Instance.GameData.Authenticator.AllowOverrideAuthentication = setValue;
        }
    }
}