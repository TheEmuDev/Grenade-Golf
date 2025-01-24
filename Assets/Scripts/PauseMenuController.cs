using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenuPanel;

    private GameManager gm;

    private void Awake()
    {
        gm = GameManager.instance;
    }

    private void OnEnable()
    {
        GameManager.OnGamePause += EnablePauseMenu;
        GameManager.OnGameResume += DisablePauseMenu;
    }

    private void OnDisable()
    {
        GameManager.OnGamePause -= EnablePauseMenu;
        GameManager.OnGameResume -= DisablePauseMenu;
    }

    private void EnablePauseMenu()
    {
        PauseMenuPanel.SetActive(true);
    }

    private void DisablePauseMenu()
    {
        PauseMenuPanel.SetActive(false);
    }

    public void OnResumeButtonPressed()
    {
        gm.Pause();
    }
    
    public void OnExitButtonPressed()
    {
        gm.ExitGame();
    }
}
