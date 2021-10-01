using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    [SerializeField] AudioClip heartSFX;

    PolygonCollider2D myPolygonCollider;

    private void Start()
    {
        myPolygonCollider = GetComponent<PolygonCollider2D>();
        Physics2D.IgnoreCollision(myPolygonCollider, FindObjectOfType<Player>().GetComponent<PolygonCollider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(heartSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddLives();
        Destroy(gameObject);
    }
}
