using UnityEngine;

public class CallPauseMenu : MonoBehaviour
{
    public bool GameIsPaused {  get; private set; }

    [Header("Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject Settings;

    private void OnEnable()
    {
        Player.menuWasPressed += PauseGame;
    }

    public void PauseGame()
    {
        GameIsPaused = !GameIsPaused;

        if (GameIsPaused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
            Settings.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Player.menuWasPressed -= PauseGame;
    }
}
