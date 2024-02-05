using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeWarning : MonoBehaviour
{
    private bool alphaGrowing = false;
    private float time = 3.0f;

    public void Spawn(Vector3 position, float size)
    {
        transform.position = position;
        transform.localScale = new Vector3(size, size, 1);
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
