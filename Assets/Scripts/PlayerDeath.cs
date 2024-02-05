using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D()
    {
        ChangePlayerColor();
        FindObjectOfType<GameManager>().EndGame();
    }

    private void ChangePlayerColor()
    {
        this.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.black;
    }
}
