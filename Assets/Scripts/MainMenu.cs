using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    //
    InputAction PauseAction;
    [SerializeField]
    GameObject PauseMenu;
    

    private void Start()
    {
        PauseAction=InputSystem.actions.FindAction("Pause");
    }

    private void Update()
    {
        if(PauseAction.WasPressedThisFrame())
        {
            PauseMenu.SetActive(true);
            Time.timeScale=0;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale=1;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale=1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale=1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }






}
