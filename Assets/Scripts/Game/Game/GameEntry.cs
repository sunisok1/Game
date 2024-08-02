using Framework;
using UnityEngine.SceneManagement;

namespace Game.Game
{
    public class GameEntry : Entry
    {
        private void Start()
        {
            SceneManager.LoadScene("Chess");
        }
    }
}