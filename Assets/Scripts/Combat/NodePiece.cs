using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodePiece : MonoBehaviour
{ 
    public int value;
    public Point index;
    public Vector2 pos;

    SpriteRenderer sr;
    bool updating;

    public void Init(int v, Point p, Sprite piece)
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = piece;
        value = v;
        SetIndex(p);
    }

    public void SetIndex(Point p)
    {
        index = p;
        ResetPos();
        UpdateName();
    }

    public void ResetPos()
    {
        pos = new Vector2(index.x, index.y);
    }

    public void MovePos(Vector2 v)
    {
        transform.position = v * Time.deltaTime;
    }

    public void MovePosTo (Vector2 v)
    {
        Vector2 newPos = Vector2.Lerp(pos, v, Time.deltaTime);
        transform.position = newPos;
    }

    public bool UpdatePiece()
    {
        if (Vector2.Distance(transform.position, pos) > 1)
        {
            MovePosTo(pos);
            updating = true;
            return true;
        }
        else
        {
            transform.position = pos;
            updating = false;
            return false;
        }
    }

    void UpdateName()
    {
        transform.name = "Node [" + index.x + ", " + index.y + "]";
    }

    public void OnMouseDown()
    {
        if (updating)
        {
            return;
        }
        MovePieces.instance.MovePiece(this);
    }

    public void OnMouseUp()
    {
        MovePieces.instance.DropPiece();
    }
}
