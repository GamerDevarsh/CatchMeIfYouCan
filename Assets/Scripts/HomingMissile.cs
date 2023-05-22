using System.Collections;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    
    [SerializeField]
    private float speed = 40;

    // The base rotation speed of the missile, in radians per frame.
    [SerializeField]
    [Range(0, 2)]
    private float rotationSpeed;

    // The distance at which this object stops following its target and continues on its last known trajectory. 
    [SerializeField]
    private float focusDistance = 5;

    private Transform target;

    private bool isFollowingTarget = true;

    public GameObject blastPrefab;

    [SerializeField]    
    private bool faceTarget;

    private Vector3 tempVector;

    private void Start()
    {
        target = GameManager.Instance.playerGameObject.transform;
        
        if(target == null){
            Debug.LogError("No missile target reference found");
        }
        StartCoroutine(IncreaseMissileSpeed());     
    }

    private void FixedUpdate()
    {

        if (Vector3.Distance(transform.position, target.position) < focusDistance)
        {
            isFollowingTarget = false;
            Debug.Log("stopped following target: "+isFollowingTarget);

            //transform.position = new Vector3(transform.position.x,target.position.y,transform.position.z);
            
        }
        if (Vector3.Distance(transform.position, target.position) > 40.0f && isFollowingTarget == false) //
        {
            StartCoroutine(DestroyMissile());
            StartCoroutine(LaunchNewMissile());
        }

        Vector3 targetDirection = target.position - transform.position;

        if (faceTarget)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

            MoveForward(Time.deltaTime);

            if (isFollowingTarget)
            {
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        else
        {            
            if (isFollowingTarget)
            {
                tempVector = targetDirection.normalized;

                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(tempVector * Time.deltaTime * speed, Space.World);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DestroyAllOnGameEnd(other.gameObject));
        }
    }


    private void MoveForward (float rate)
    {
        transform.Translate(Vector3.forward * rate * speed, Space.Self);
    }

    IEnumerator IncreaseMissileSpeed(){
        for(;;)
        {
         speed+=5.0f;
         Debug.Log("speed increased, now its: "+ speed);
         yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator LaunchNewMissile(){
        GameManager.Instance.LaunchMissile();
        yield return null;
    }
    IEnumerator DestroyMissile(){
        Instantiate(blastPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        yield return null;
    }

    IEnumerator DestroyAllOnGameEnd(GameObject _object){
        GameManager.Instance.playerGameObject.IsPlaneAlive = false;
        Instantiate(blastPrefab, transform.position, Quaternion.identity);
        Destroy(PoinstSpawnManager.PointInstance.GetSpawnedCoins().gameObject);
        Destroy(this.gameObject);
        Destroy(_object);
        GameManager.Instance.uiManager.ActivateGOPanel();
        yield return null;
    }
}