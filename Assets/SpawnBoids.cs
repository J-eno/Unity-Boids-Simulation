using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoids : MonoBehaviour
{
    public GameObject boidObject;
    public int SpawnAmount;
    public int SpawnSeconds;
    int boidCount;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawn(SpawnAmount);
    }



    IEnumerator SpawnEm() {
        while( boidCount < SpawnAmount) {
            SpawnABoid();
            yield return new WaitForSeconds(SpawnSeconds/SpawnAmount);
        }
        //yield break;
    }

    public void SpawnABoid()
    {
        if(boidCount >= TheBrain.MAX_SPAWN_AMOUNT)
            return;
        //Spawn Boid At Random Position
        Instantiate(boidObject, 
        new Vector3(Random.Range(CameraController.cam.min.x, CameraController.cam.max.x), Random.Range(CameraController.cam.min.y, CameraController.cam.max.y), 0), 
        Quaternion.identity);
        boidCount++;
    }
    public void SpawnABoid(Vector3 position)
    {
        if(boidCount >= TheBrain.MAX_SPAWN_AMOUNT)
            return;
        //Spawn Boid At Given Position
        Instantiate(boidObject, 
        position, 
        Quaternion.identity);
        boidCount++;
    }
    
    public void StartSpawn(int sa) {
        SpawnAmount = sa;
        SpawnSeconds = SpawnAmount / 4;
        StartCoroutine(SpawnEm());
    }
    public void ResetBoidCount() {
        boidCount = 0;
    }
}
