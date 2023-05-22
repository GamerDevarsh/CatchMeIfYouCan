using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float missileSpawnRadius = 40f;
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public GameObject cameraGameObject;
    public PlaneMovementController playerGameObject;
    public UIManager uiManager;
    public GameObject missileGameObject;
    private int points = 0;
    private float currentTime = 0;
    //public TextMeshProUGUI timerText, scoreText;

    //the more "Minutes" you play the more value (X Minutes) of points you get 
    public int Points{
    get{
        return points;
        } 
    set{
        points += (value + (int)(currentTime / 10f));
        Debug.Log("points set:" + points);
        }
    }

     private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    private void Start() {
        LaunchMissile();
    }

    void FixedUpdate()
    {
        if(playerGameObject.IsPlaneAlive)
        cameraGameObject.transform.position = new Vector3(playerGameObject.transform.position.x, playerGameObject.transform.position.y + 35.0f, playerGameObject.transform.position.z);
    }
    private void Update() {
        if(playerGameObject.IsPlaneAlive)
        {
        currentTime += Time.deltaTime;

       uiManager.timerText.text = "TIME PLAYED: " + currentTime.ToString("0.00");
       uiManager.scoreText.text = "SCORE: " + points.ToString();
        }
    }
    public void LaunchMissile()
    {
        Vector3 randomOffset = Random.insideUnitSphere * missileSpawnRadius;
        Vector3 spawnPosition = playerGameObject.transform.position + randomOffset;
        GameObject missile = Instantiate(missileGameObject, spawnPosition, Quaternion.identity);
    }

}
