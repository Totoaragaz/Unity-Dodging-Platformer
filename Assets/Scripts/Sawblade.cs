using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Sawblade : MonoBehaviour
{
    public GameObject warning;
    private float size;
    private Vector3 position;

    public void Spawn(Transform obstacleParent)
    {
        position = new Vector3(Random.Range(-10, 10), Random.Range(-7, 7), 0);
        size = Random.Range(3, 8);

        GameObject newWarning = Instantiate(warning, obstacleParent.transform);
        newWarning.GetComponent<SawbladeWarning>().Spawn(position, size);

        StartCoroutine(SpawnSaw());
    }

    private IEnumerator SpawnSaw()
    {
        yield return new WaitForSeconds(3.0f);
        transform.position = position;
        transform.localScale = new Vector3(0, 0, 1);
        StartCoroutine(SpinSaw());
        StartCoroutine(GrowSaw());

        yield return new WaitForSeconds(5.0f);
        StartCoroutine(DestroySaw());
    }

    private IEnumerator SpinSaw(float time = 5.5f)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, transform.rotation.z - 3f));
            yield return null;
        }
    }

    private void SetSawSize(float newSize)
    {
        if (newSize > size)
        {
            transform.localScale = new Vector3(size, size, 1);
        }
        else
        {
            if (newSize < 0)
            {
                transform.localScale = new Vector3(0, 0, 1);
            }
            else
            {
                transform.localScale = new Vector3(newSize, newSize, 1);
            }
        }
    }
    private IEnumerator GrowSaw()
    {
        float currentSize = 0;
        while (transform.localScale.x < size)
        {
            currentSize += size * Time.deltaTime;
            SetSawSize(currentSize);
            yield return null;
        }
    }

    private IEnumerator ShrinkSaw()
    {
        float currentSize = size;
        while (transform.localScale.x > 0)
        {
            currentSize -= size * Time.deltaTime * 2;
            transform.localScale = new Vector3(currentSize, currentSize, 1);
            yield return null;
        }
    }

    private IEnumerator DestroySaw()
    {
        StartCoroutine(ShrinkSaw());
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
