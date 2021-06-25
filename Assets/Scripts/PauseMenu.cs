using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused =false;
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        isPaused = true;
    }
}
