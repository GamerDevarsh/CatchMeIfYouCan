using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoinstSpawnManager : MonoBehaviour
{
    PlaneMovementController planeGameObject;
    public GameObject coinGameobject;
    
    [SerializeField]
    private float minXSpawnDistance = -20f; 
    private float maxXSpawnDistance = 20f;
    GameObject spawnedCoin;
    private static PoinstSpawnManager _pointInstance;
    public static PoinstSpawnManager PointInstance { get { return _pointInstance; } }
     private void Awake()
    {
        if (_pointInstance != null && _pointInstance != this)
        {
            Destroy(this.gameObject);
        } else {
            _pointInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        planeGameObject = GameManager.Instance.playerGameObject;
        StartCoroutine(SpawnCoin());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(spawnedCoin != null && GameManager.Instance.playerGameObject.IsPlaneAlive == true)
        spawnedCoin.transform.position = new Vector3(spawnedCoin.transform.position.x,planeGameObject.transform.position.y,spawnedCoin.transform.position.z);
    }

    

    public IEnumerator SpawnCoin(){ 
        float randomX = Random.Range(minXSpawnDistance, maxXSpawnDistance);
        Vector3 spawnPosition = planeGameObject.transform.position + new Vector3(randomX, 0f, 0f);
        spawnedCoin = Instantiate(coinGameobject, spawnPosition, Quaternion.identity);
        yield return null;
    }

    public CoinObject GetSpawnedCoins(){
        return spawnedCoin.GetComponent<CoinObject>();
    }
    
}
