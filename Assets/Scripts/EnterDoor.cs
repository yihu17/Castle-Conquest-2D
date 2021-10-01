using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] AudioClip openingSFX, closingSFX;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpeningDoorSFX()
    {
        AudioSource.PlayClipAtPoint(openingSFX, Camera.main.transform.position);
    }

    void ClosingDoorSFX()
    {
        AudioSource.PlayClipAtPoint(closingSFX, Camera.main.transform.position);
    }
}
