using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReturnGame());
    }

    IEnumerator ReturnGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Game");
    }
}
