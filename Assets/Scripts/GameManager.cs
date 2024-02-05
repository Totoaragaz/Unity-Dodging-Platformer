using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(FreezeUntilInput());
    }

    private IEnumerator FreezeUntilInput()
    {
        Time.timeScale = 0;
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
        Time.timeScale = 1.0f;
    }

    public void EndGame()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
}
