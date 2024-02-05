using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : MonoBehaviour
{
    private bool currentlyFiring = false;

    public void changeFiringState(bool state)
    {
        currentlyFiring = state;
    }

    public bool isFiring()
    {
        return currentlyFiring;
    }
    public void rotateCannon(float angle)
    {
        StartCoroutine(FlipCannon(angle));
    }

    private IEnumerator FlipCannon(float angle)
    {
        angle += 180;
        float degreesLeft = angle;
        while (degreesLeft > 0)
        {
            transform.Rotate(new Vector3(0, 0, Mathf.Min(angle * Time.deltaTime * 3, degreesLeft)));
            degreesLeft -= Mathf.Min(angle * Time.deltaTime * 3, degreesLeft);
            yield return null;
        }
    }
}
