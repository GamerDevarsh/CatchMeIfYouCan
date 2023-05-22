using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    PlaneMovementController planeGameObject;
    [SerializeField]
    float respawnTime = 10f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        planeGameObject = GameManager.Instance.playerGameObject;
        timer = respawnTime;
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, 20.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Coin Collected ");
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Points = 1;
            PoinstSpawnManager.PointInstance.StartCoroutine(PoinstSpawnManager.PointInstance.SpawnCoin());
            StartCoroutine(DestroyCoin());
        }
    
    }

    IEnumerator DestroyCoin(){
        Destroy(this.gameObject);
        yield return null;
    }

}
