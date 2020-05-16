using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class MainMenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
