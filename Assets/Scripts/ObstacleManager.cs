using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstacleParent;
    public GameObject baseSawblade;
    public GameObject baseLaser;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartObstacleCycle());
    }

    private IEnumerator StartObstacleCycle()
    {
        yield return new WaitForSeconds(1f);
        float timeBetweenObstacles = 3f;
        while (true)
        {
            int obstacleType = Random.Range(0, 2);
            switch (obstacleType)
            {
                case 0:
                    SpawnSawblade();
                    break;
                case 1:
                    SpawnLaser();
                    break;
            }
            
            
            if (timeBetweenObstacles > 1.5)
            {
                timeBetweenObstacles -= 0.3f;
            }
            yield return new WaitForSeconds(timeBetweenObstacles);
        }
    }

    void SpawnSawblade()
    {
        GameObject newSawblade = Instantiate(baseSawblade, obstacleParent.transform);
        newSawblade.GetComponent<Sawblade>().Spawn(obstacleParent.transform);
    }

    void SpawnLaser()
    {
        GameObject newLaser = Instantiate(baseLaser, obstacleParent.transform);
        newLaser.GetComponent<Laser>().Spawn(obstacleParent.transform);
    }

    void SpawnSawbladeWarning(Vector3 position, float size)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
