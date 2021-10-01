using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] float secondsToLoad = 2f;
    [SerializeField] AudioClip openingSFX, closingSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetTrigger("Open");   
    }

    public void StartLoadingNextLevel()
    {
        GetComponent<Animator>().SetTrigger("Close");
        AudioSource.PlayClipAtPoint(closingSFX, Camera.main.transform.position);
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(secondsToLoad);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
    }

    void OpeningDoorSFX()
    {
        AudioSource.PlayClipAtPoint(openingSFX, Camera.main.transform.position);
    }
}
