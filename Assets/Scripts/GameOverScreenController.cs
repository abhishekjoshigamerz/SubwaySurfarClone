using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour
{
    public void ContinueGame(){
        Application.LoadLevel(0);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
