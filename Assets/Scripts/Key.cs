using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] AudioClip keySFX;

    BoxCollider2D myBoxCollider;

    // Start is called before the first frame update
    private void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(myBoxCollider, FindObjectOfType<Player>().GetComponent<PolygonCollider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(keySFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
