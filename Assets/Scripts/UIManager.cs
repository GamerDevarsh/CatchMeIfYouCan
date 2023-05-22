using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText, scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOvertimertxt, gameOverScoretxt;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    
    public void ActivateGOPanel(){
        
        gameOverPanel.SetActive(true);
        gameOvertimertxt.text = timerText.text + " Min";
        gameOverScoretxt.text = scoreText.text;
        timerText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }
    public void RestartGame(){
        SceneManager.LoadScene("TutorialScene");
    }
}
