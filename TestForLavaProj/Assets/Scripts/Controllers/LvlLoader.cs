using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class LvlLoader : MonoBehaviour
    {
        public void RestartLvl()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
