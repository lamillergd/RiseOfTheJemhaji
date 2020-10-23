using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Sprite[] pieces;
    public int width;
    public int height;
    int[] fills;
    public GameObject nodePiece;
    public GameObject killedBoard;
    public GameObject killedPiece;
    public Sprite newPiece;

    //board stores all of the pieces
    public Node[,] board;
    System.Random random;
    List<NodePiece> update;
    List<FlippedPieces> flipped;
    List<NodePiece> dead;
    List<KilledPiece> killed;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        List<NodePiece> finishedUpdating = new List<NodePiece>();
        for (int i = 0; i < update.Count; i++)
        {
            NodePiece piece = update[i];
            if (!piece.UpdatePiece())
            {
                finishedUpdating.Add(piece);
            }
        }

        for (int i = 0; i < finishedUpdating.Count; i++)
        {
            NodePiece piece = finishedUpdating[i];
            FlippedPieces flip = GetFlipped(piece);
            NodePiece flippedPiece = null;

            int x = (int)piece.index.x;
            fills[x] = Mathf.Clamp(fills[x] - 1, 0, width);

            List<Point> connected = isConnected(piece.index, true);

            bool wasFlipped = (flip != null);
            if (wasFlipped)
            {
                flippedPiece = flip.GetOtherPiece(piece);
                AddPoints(ref connected, isConnected(flippedPiece.index, true));
            }

            if (connected.Count == 0)
            {
                if (wasFlipped)
                {
                    FlipPieces(piece.index, flippedPiece.index, false);
                }
            }
            else
            {
                foreach (Point pnt in connected)
                {
                    KillPiece(pnt);
                    Node node = GetNodeAtPoint(pnt);
                    NodePiece nodePiece = node.GetPiece();
                    if (nodePiece != null)
                    {
                        nodePiece.gameObject.SetActive(false);
                        dead.Add(nodePiece);
                    }
                    node.SetPiece(null);
                }
                ApplyGravityToBoard();
            }
            flipped.Remove(flip);
            update.Remove(piece);
        }
    }

    //the y and ny loop make the board fall backwards lol
    //can keep for now, looks kinda dodge tho
    void ApplyGravityToBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = (height - 1); y >= 0; y--)
            {
                Point p = new Point(x, y);
                Node node = GetNodeAtPoint(p);
                int val = GetValueAtPoint(p);
                if (val != 0)
                {
                    continue;
                }

                for (int ny = (y-1); ny >= -1; ny--)
                {
                    Point next = new Point(x, ny);
                    int nextVal = GetValueAtPoint(next);
                    if (nextVal == 0)
                    {
                        continue;
                    }
                    if (nextVal != -1)
                    {
                        Node nextNode = GetNodeAtPoint(next);
                        NodePiece piece = nextNode.GetPiece();

                        node.SetPiece(piece);
                        update.Add(piece);

                        nextNode.SetPiece(null);
                    }
                    else
                    {
                        int newVal = fillPiece();
                        NodePiece piece;
                        Point fallPoint = new Point(x, (-1 - fills[x]));
                        if (dead.Count > 0)
                        {
                            NodePiece revived = dead[0];
                            revived.gameObject.SetActive(true);
                            piece = revived;

                            dead.RemoveAt(0);
                        }
                        else
                        {
                            GameObject obj = Instantiate(nodePiece, this.gameObject.transform);
                            NodePiece n = obj.GetComponent<NodePiece>();
                            piece = n;
                        }
                        
                        piece.Init(newVal, p, pieces[newVal - 1]);
                        piece.transform.position = GetPosFromPoint(fallPoint);
                        Node hole = GetNodeAtPoint(p);
                        hole.SetPiece(piece);
                        ResetPiece(piece);
                        fills[x]++;
                    }

                    break;
                }
            
            }
        }
    }

    FlippedPieces GetFlipped (NodePiece p)
    {
        FlippedPieces flip = null;
        for (int i = 0; i < flipped.Count; i++)
        {
            if (flipped[i].GetOtherPiece(p) != null)
            {
                flip = flipped[i];
                break;
            }
        }

        return flip;
    }

    void StartGame()
    {   
        fills = new int[width];
        string seed = GetRandomSeed();
        random = new System.Random(seed.GetHashCode());
        update = new List<NodePiece>();
        flipped = new List<FlippedPieces>();
        dead = new List<NodePiece>();
        killed = new List<KilledPiece>();

        InitBoard();
        VerifyBoard();
        CreateBoard();
        Player.instance.currentMana = 0;
    }

    void InitBoard()
    {
        board = new Node[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                board[x, y] = new Node(fillPiece(), new Point(x, y));
            }
        }
    }

    void VerifyBoard()
    {
        List<int> remove;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Point p = new Point(x, y);
                int val = GetValueAtPoint(p);
                if (val <= 0)
                {
                    continue;
                }

                remove = new List<int>();
                while (isConnected(p, true).Count > 0)
                {
                    val = GetValueAtPoint(p);
                    if (!remove.Contains(val))
                    {
                        remove.Add(val);
                    }
                    SetValueAtPoint(p, NewValue(ref remove));
                }
            }
        }
    }

    void CreateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = GetNodeAtPoint(new Point(x, y));
                int val = node.value;
                if (val <= 0)
                {
                    continue;
                }
                GameObject p = Instantiate(nodePiece, new Vector2(x, y), Quaternion.identity);
                p.transform.parent = this.transform;
                NodePiece piece = p.GetComponent<NodePiece>();
                piece.Init(val, new Point(x, y), pieces[val - 1]);
                node.SetPiece(piece);
            }
        }
    }

    public void ResetPiece(NodePiece piece)
    {
        piece.ResetPos();
        //piece.flipped = null;
        update.Add(piece);
    }

    public void FlipPieces(Point p1, Point p2, bool main)
    {
        if (GetValueAtPoint(p1) < 0)
        {
            return;
        }

        Node n1 = GetNodeAtPoint(p1);
        NodePiece piece1 = n1.GetPiece();
        if (GetValueAtPoint(p2) > 0)
        {
            Node n2 = GetNodeAtPoint(p2);
            NodePiece piece2 = n2.GetPiece();
            n1.SetPiece(piece2);
            n2.SetPiece(piece1);

            //piece1.flipped = piece2;
            //piece2.flipped = piece1;

            if (main)
            {
                flipped.Add(new FlippedPieces(piece1, piece2));
            }

            update.Add(piece1);
            update.Add(piece2);
        }
        else
        {
            ResetPiece(piece1);
        }
    }

    void KillPiece (Point p)
    {
        List<KilledPiece> available = new List<KilledPiece>();

        for (int i = 0; i < killed.Count; i++)
        {
            if (!killed[i].falling)
            {
                available.Add(killed[i]);
            }
        }

        KilledPiece set = null;
        if (available.Count > 0)
        {
            set = available[0];
        }
        else
        {
            GameObject kill = Instantiate(killedPiece, killedBoard.transform);
            KilledPiece kPiece = kill.GetComponent<KilledPiece>();
            set = kPiece;
            killed.Add(kPiece);
        }

        int val = GetValueAtPoint(p) - 1;
        if (set != null && val >= 0 && val < pieces.Length)
        {
            set.Init(pieces[val], GetPosFromPoint(p));
        }
    }

    List<Point> isConnected(Point p, bool main)
    {
        List<Point> connected = new List<Point>();
        int val = GetValueAtPoint(p);
        Point[] directions =
        {
            Point.Up,
            Point.Right,
            Point.Down,
            Point.Left
        };

        //2+ of same type
        foreach (Point dir in directions) 
        {
            List<Point> line = new List<Point>();
            int same = 0;

            for (int i = 1; i < 3; i++)
            {
                Point check = Point.Add(p, Point.Mult(dir, i));
                if(GetValueAtPoint(check) == val)
                {
                    line.Add(check);
                    same++;
                }
            }

            if (same > 1) //if there are more than one of the same in the dir = match
            {
                AddPoints(ref connected, line); //add points to overarching list
            }

            ScoreSystem.instance.AddMana(same, 2, main);
            ScoreSystem.instance.AddMana(same, 3, main);
            ScoreSystem.instance.AddMana(same, 4, main);
        }

        //checking if we are in the middle of a match
        for (int i = 0; i < 2; i++)
        {
            List<Point> line = new List<Point>();

            int same = 0;
            Point[] check = { Point.Add(p, directions[i]), Point.Add(p, directions[i + 2]) };

            foreach (Point next in check)
            {
                if (GetValueAtPoint(next) == val)
                {
                    line.Add(next);
                    same++;
                }
            }

            if (same > 1)
            {
                AddPoints(ref connected, line);
            }

            ScoreSystem.instance.AddMana(same, 2, main);
            ScoreSystem.instance.AddMana(same, 3, main);
            ScoreSystem.instance.AddMana(same, 4, main);
        }

        //2x2
        //for (int i = 0; i < 4; i++)
        //{
        //    List<Point> square = new List<Point>();
        //    int same = 0;
        //    int next = i + 1;
        //    if (next >= 4)
        //    {
        //        next -= 4;
        //    }

        //    Point[] check = 
        //    {
        //        Point.Add(p, directions[i]), 
        //        Point.Add(p, directions[next]),
        //        Point.Add(p, Point.Add(directions[i], directions[next]))
        //    };

        //    foreach (Point nextPoint in check)
        //    {
        //        if (GetValueAtPoint(nextPoint) == val)
        //        {
        //            square.Add(nextPoint);
        //            same++;
        //        }
        //    }

        //    if (same > 2)
        //    {
        //        AddPoints(ref connected, square);
        //    }

        //    //match square
        //    if (same == 3 && main)
        //    {
        //        score++;
        //        Debug.Log("Match square: " + score);
        //    }
        //}

        if(main)
        {
            for (int i = 0; i < connected.Count; i++)
            {
                AddPoints(ref connected, isConnected(connected[i], false));
            }
        }

        return connected;
    }

    void AddPoints(ref List<Point> points, List<Point> add)
    {
        foreach(Point p in add)
        {
            bool doAdd = true;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Equals(p))
                {
                    doAdd = false;
                    break;
                }
            }

            if (doAdd)
            {
                points.Add(p);
            }
        }
    }

    int fillPiece()
    {
        int val = 1;
        val = Random.Range(1, pieces.Length + 1);
        return val;
    }

    int GetValueAtPoint(Point p)
    {
        if (p.x < 0 || p.x >= width || p.y < 0 || p.y >= height)
        {
            return -1;
        }
        return board[p.x, p.y].value;
    }

    void SetValueAtPoint(Point p, int v)
    {
        board[p.x, p.y].value = v;
    }

    Node GetNodeAtPoint(Point p)
    {
        return board[p.x, p.y];
    }

    int NewValue(ref List<int> remove)
    {
        List<int> available = new List<int>();
        for (int i = 0; i < pieces.Length; i++)
        {
            available.Add(i + 1);
        }
        foreach (int i in remove)
        {
            available.Remove(i);
        }
        if (available.Count <= 0)
        {
            return 0;
        }

        return available[random.Next(0, available.Count)];
    }
    
    string GetRandomSeed()
    {
        string seed = "";
        string acceptableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!$%^&()";
        for (int i = 0; i < 20; i++)
        {
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        }

        return seed;
    }

    public Vector2 GetPosFromPoint(Point p)
    {
        return new Vector2(p.x, p.y);
    }
}

[System.Serializable]
public class Node
{
    public int value; //0 = blank, 1 = orange, 2 = blue, 3 = red, 4 = purple, 5 = pink, -1 = hole
    public Point index;
    NodePiece piece;

    public Node(int v, Point i)
    {
        value = v;
        index = i;
    }

    public void SetPiece (NodePiece p)
    {
        piece = p;
        value = (piece == null) ? 0 : piece.value;
        if (piece == null)
        {
            return;
        }
        piece.SetIndex(index);
    }

    public NodePiece GetPiece()
    {
        return piece;
    }
}

[System.Serializable]
public class FlippedPieces
{
    public NodePiece one;
    public NodePiece two;

    public FlippedPieces(NodePiece n1, NodePiece n2)
    {
        one = n1;
        two = n2;
    }

    public NodePiece GetOtherPiece (NodePiece p)
    {
        if (p == one)
        {
            return two;
        }
        else if (p == two)
        {
            return two;
        }
        else
        {
            return null;
        }
    }
}