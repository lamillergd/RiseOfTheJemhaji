using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePieces : MonoBehaviour
{
    public static MovePieces instance;
    Board game;
    NodePiece movingPiece;
    Point newIndex;
    Vector2 mouseStart;

    void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        game = GetComponent<Board>();
    }

    
    void Update()
    {
        if (movingPiece != null)
        {
            Vector2 dir = ((Vector2)Input.mousePosition - mouseStart);
            Vector2 nDir = dir.normalized;
            Vector2 aDir = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

            newIndex = Point.Clone(movingPiece.index);
            Point add = Point.Zero;
            if (dir.magnitude > 32)
            {
                if (aDir.x > aDir.y)
                {
                    add = (new Point((nDir.x > 0) ? 1 : -1, 0));
                }
                else if (aDir.y > aDir.x)
                {
                    add = (new Point(0, (nDir.y > 0) ? 1 : -1));
                }
            }
            newIndex.Add(add);

            Vector2 pos = game.GetPosFromPoint(movingPiece.index);
            if (!newIndex.Equals(movingPiece.index))
            {
                pos += Point.Mult(add, 16).ToVector();
            }
            movingPiece.MovePosTo(pos);
        }
    }

    public void MovePiece(NodePiece piece)
    {
        if (movingPiece != null)
        {
            return;
        }
        movingPiece = piece;
        mouseStart = Input.mousePosition;
    }

    public void DropPiece()
    {
        if (movingPiece == null)
        {
            return;
        }
        
        if (!newIndex.Equals(movingPiece.index))
        {
            game.FlipPieces(movingPiece.index, newIndex, true);
        }
        else
        {
            game.ResetPiece(movingPiece);
        }
        movingPiece = null;
    }
}
