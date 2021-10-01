using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] AudioClip diamondSFX;
    [SerializeField] int diamondValue = 150;

    PolygonCollider2D myPolygonCollider;

    private void Start()
    {
        myPolygonCollider = GetComponent<PolygonCollider2D>();
        Physics2D.IgnoreCollision(myPolygonCollider, FindObjectOfType<Player>().GetComponent<PolygonCollider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(diamondSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddScore(diamondValue);
        Destroy(gameObject);
    }
}
