using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnStartGameClicked(){
        SceneManager.LoadScene("GameplayScene");
    }

    public void ExitGame(){
            Application.Quit();
    }
}
