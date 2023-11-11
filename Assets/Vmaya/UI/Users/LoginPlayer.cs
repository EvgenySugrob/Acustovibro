using UnityEngine;
using UnityEngine.UI;
using Vmaya.UI.Components;

namespace Vmaya.UI.Users
{
    [RequireComponent(typeof(ModalWindow))]
    public class LoginPlayer : MonoBehaviour
    {
        [SerializeField]
        private InputField login;
        [SerializeField]
        private InputField password;
        [SerializeField]
        private UIPlayer playerManager;
        [SerializeField]

        private void Start()
        {
            login.text = playerManager.DefaultUser;
            password.text = playerManager.DefaultPass;
            login.Select();
        }

        public void Login()
        {
            if (playerManager.login(login.text, password.text)) dialog.hide();
        }

        public void showLogin()
        {
            gameObject.SetActive(true);
        }

        public void CloseDialog()
        {
            if (playerManager.User != null) dialog.hide();
            else if (playerManager.login(playerManager.DefaultUser, playerManager.DefaultPass)) dialog.hide();
        }

        protected ModalWindow dialog => GetComponent<ModalWindow>();
    }
}