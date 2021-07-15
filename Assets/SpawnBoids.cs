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
            Instantiate(boidObject, 
            new Vector3(Random.Range(CameraController.cam.min.x, CameraController.cam.max.x), Random.Range(CameraController.cam.min.y, CameraController.cam.max.y), 0), 
            Quaternion.identity);
            boidCount++;
            yield return new WaitForSeconds(SpawnSeconds/SpawnAmount);
        }
        //yield break;
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
