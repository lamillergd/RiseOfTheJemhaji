using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledPiece : MonoBehaviour
{
    public bool falling;
    float speed = 2f;
    float gravity = 16f;
    Vector3 moveDir;
    SpriteRenderer sr;

    public void Init(Sprite piece, Vector2 start)
    {
        falling = true;

        moveDir = Vector2.up;
        moveDir.x = Random.Range(-1f, 1f);
        moveDir *= speed / 2;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = piece;

        transform.position = start;
    }


    void Update()
    {
        if (!falling)
        {
            return;
        }

        moveDir.y -= Time.deltaTime * gravity;
        moveDir.x = Mathf.Lerp(moveDir.x, 0, Time.deltaTime);
        transform.position += moveDir * Time.deltaTime * speed;
        if (transform.position.x < -32f || transform.position.x > Screen.width + 32f || transform.position.y < -32f || transform.position.y > Screen.height + 32f)
        {
            falling = false;
        }
    }
}
