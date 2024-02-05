using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWarning : MonoBehaviour
{
    private bool alphaGrowing = false;
    private float time = 3.0f;

    public void Spawn(Vector3 position, Vector3 size, float rotation)
    {
        transform.Rotate(new Vector3(0, 0, rotation));
        transform.position = position;
        transform.localScale = size;
        StartCoroutine(FlashWarning());
    }

    private IEnumerator FlashWarning()
    {
        Color color = GetComponent<SpriteRenderer>().color;

        while (time > 0)
        {
            time -= Time.deltaTime;
            if (alphaGrowing)
            {
                color.a += 0.01f;
                GetComponent<SpriteRenderer>().color = color;
                if (color.a >= 1f)
                {
                    alphaGrowing = false;
                }
            }
            else
            {
                color.a -= 0.01f;
                GetComponent<SpriteRenderer>().color = color;
                if (color.a <= 0)
                {
                    alphaGrowing = true;
                }
            }
            yield return null;
        }

        Destroy(gameObject);
    }
}
