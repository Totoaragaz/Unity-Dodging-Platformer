using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    public GameObject warning;
    public GameObject allLaserCannons;
    public Vector3 size = new Vector3(25, 2, 1);
    private GameObject laserCannon;
    private bool cannonIsOnSideWall;
    private Vector3 laserPosition;
    private float rotation;

    public void Spawn(Transform obstacleParent)
    {
        DetermineLaser();

        GameObject newWarning = Instantiate(warning, obstacleParent.transform);
        if (cannonIsOnSideWall)
        {
            newWarning.GetComponent<LaserWarning>().Spawn(laserPosition, size, rotation);
        }
        else
        {
            newWarning.GetComponent<LaserWarning>().Spawn(laserPosition, size, rotation + 90);
        }
       

        StartCoroutine(SpawnLaser());
    }

    private void DetermineLaser()
    {
        DetermineCannon();
        DetermineRotation();
        DeterminePosition();
    }

    private void DetermineCannon()
    {
        while (true)
        {
            GameObject cannon = allLaserCannons.transform.GetChild(Random.Range(0, 34)).gameObject;
            if (!cannon.GetComponent<LaserCannon>().isFiring())
            {
                laserCannon = cannon;
                laserCannon.GetComponent<LaserCannon>().changeFiringState(true);
                break;
            }
        }
        cannonIsOnSideWall = !(laserCannon.transform.position.y > 6 || laserCannon.transform.position.y < -6);
    }

    private void DetermineRotation()
    {
        if (!cannonIsOnSideWall)
        {
            transform.Rotate(new Vector3(0, 0, 90));
            if (laserCannon.transform.position.x < 0)
            {
                rotation = Random.Range(-30 + laserCannon.transform.position.x * 3.5f, 30);
            }
            else
            {
                rotation = Random.Range(-30, 30 - laserCannon.transform.position.x * 3.5f);
            }
        }
        else
        {
            transform.Rotate(Vector3.zero);
            if (laserCannon.transform.position.y < 0)
            {
                rotation = Random.Range(-30 + laserCannon.transform.position.y * 3.5f, 30);
            }
            else
            {
                rotation = Random.Range(-30, 30 - laserCannon.transform.position.y * 3.5f);
            }
        }
            
    }

    private void DeterminePosition()
    {
        Vector3 cannonPosition = laserCannon.transform.position;
        if (cannonPosition.y > 6)
        {
            laserPosition = new Vector3(laserCannon.transform.position.x + 7.5f * Mathf.Tan(Mathf.Deg2Rad * rotation), 0, 0);
        }
        if (cannonPosition.y < -6)
        {
            laserPosition = new Vector3(laserCannon.transform.position.x - 7.5f * Mathf.Tan(Mathf.Deg2Rad * rotation), 0, 0);
        }
        if (cannonPosition.x > 10)
        {
            laserPosition = new Vector3(0, laserCannon.transform.position.y - 10.5f * Mathf.Tan(Mathf.Deg2Rad * rotation), 0);
        }
        if (cannonPosition.x < -10)
        {
            laserPosition = new Vector3(0, laserCannon.transform.position.y + 10.5f * Mathf.Tan(Mathf.Deg2Rad * rotation), 0);
        }
    }

    private IEnumerator SpawnLaser()
    {
        laserCannon.GetComponent<LaserCannon>().rotateCannon(rotation);

        yield return new WaitForSeconds(3.0f);
        transform.Rotate(new Vector3(0, 0, transform.rotation.z + rotation));
        transform.position = laserPosition;
        transform.localScale = new Vector3(size.x, 0, 1);
        StartCoroutine(GrowLaser());

        yield return new WaitForSeconds(5.0f);
        StartCoroutine(DestroyLaser(rotation));
    }

    private void SetLaserThickness(float thickness)
    {
        if (thickness > 2)
        {
            transform.localScale = new Vector3(size.x, 2, 1);
        }
        else
        {
            if (thickness < 0)
            {
                transform.localScale = new Vector3(size.x, 0, 1);
            }
            else
            {
                transform.localScale = new Vector3(size.x, thickness, 1);
            }
        }
    }

    private IEnumerator GrowLaser()
    {
        float currentThickness = 0;
        while (transform.localScale.y < size.y)
        {
            currentThickness += size.y * Time.deltaTime * 5;
            SetLaserThickness(currentThickness);
            yield return null;
        }
    }

    private IEnumerator ShrinkLaser()
    {
        float currentThickness = size.y;
        while (transform.localScale.y > 0)
        {
            currentThickness -= size.y * Time.deltaTime * 4;
            SetLaserThickness(currentThickness);
            yield return null;
        }
    }

    private IEnumerator DestroyLaser(float rotation)
    {
        StartCoroutine(ShrinkLaser());
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        laserCannon.GetComponent<LaserCannon>().rotateCannon(-rotation);
        laserCannon.GetComponent<LaserCannon>().changeFiringState(false);
    }
}
