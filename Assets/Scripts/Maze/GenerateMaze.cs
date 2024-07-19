using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    public Room room;
    public static GenerateMaze Instance { get; private set; }
    public GameObject roomPrefab;
    public GameObject Endpoint;
    public GameObject HintPrefabs;
    public GameObject ScorePrefabs;
    public GameObject BombPrefabs;
    public GameObject TimePrefabs;
    // The grid.
    Room[,] rooms = null;

    public int numX;
    public int numY;

    // The room width and height.
    float roomWidth;
    float roomHeight;

    // The stack for backtracking.
    Stack<Room> stack = new Stack<Room>();

    private void GetRoomSize()
    {
        SpriteRenderer[] spriteRenderers =
              roomPrefab.GetComponentsInChildren<SpriteRenderer>();

        Vector3 minBounds = Vector3.positiveInfinity;
        Vector3 maxBounds = Vector3.negativeInfinity;
        foreach (SpriteRenderer ren in spriteRenderers)
        {
            minBounds = Vector3.Min(
              minBounds,
              ren.bounds.min);

            maxBounds = Vector3.Max(
              maxBounds,
              ren.bounds.max);
        }

        roomWidth = maxBounds.x - minBounds.x;
        roomHeight = maxBounds.y - minBounds.y;
    }

    private void Start()
    {
        numX = GameManager.numX;
        numY = GameManager.numY;
        int Hint = 4;//GameManager.HintItem;
        int Bomb = GameManager.BombItem;
        int GenPer = GameManager.GenPer;
        GetRoomSize();

        rooms = new Room[numX, numY];

        for (int i = 0; i < numX; ++i)
        {
            for (int j = 0; j < numY; ++j)
            {
                GameObject room = Instantiate(roomPrefab, new Vector3(i * roomWidth, j * roomHeight, 0.0f), Quaternion.identity);
                room.name = "Room_" + i.ToString() + "_" + j.ToString();
                rooms[i, j] = room.GetComponent<Room>();
                rooms[i, j].Index = new Vector2Int(i, j);
                if (UnityEngine.Random.Range(0, GenPer) == 0)
                {
                    switch (UnityEngine.Random.Range(0, 4))
                    {
                        case 0:
                            if (Hint > 0)
                            {
                                GameObject HintItem = Instantiate(HintPrefabs, new Vector3(i * roomWidth, j * roomHeight, 0.0f), Quaternion.identity);
                                Hint--;
                            }
                            break;
                        case 1:
                            GameObject ScoreItem = Instantiate(ScorePrefabs, new Vector3(i * roomWidth, j * roomHeight, 0.0f), Quaternion.identity);
                            break;
                        case 2:
                            if (Bomb > 0)
                            {
                                GameObject BombItem = Instantiate(BombPrefabs, new Vector3(i * roomWidth, j * roomHeight, 0.0f), Quaternion.identity);
                                Bomb--;
                            }
                            break;
                        case 3:
                            GameObject TimeItem = Instantiate(TimePrefabs, new Vector3(i * roomWidth, j * roomHeight, 0.0f), Quaternion.identity);
                            break;
                    }
                }
            }
        }
        Endpoint.transform.position = new Vector2((numX * roomWidth), (numY * roomHeight) - roomHeight);
        CreateMaze();
    }

    private void RemoveRoomWall(
      int x,
      int y,
      Room.Directions dir)
    {
        if (dir != Room.Directions.NONE)
        {
            rooms[x, y].SetDirFlag(dir, false);
        }

        Room.Directions opp = Room.Directions.NONE;
        switch (dir)
        {
            case Room.Directions.TOP:
                if (y < numY - 1)
                {
                    opp = Room.Directions.BOTTOM;
                    ++y;
                }
                break;
            case Room.Directions.RIGHT:
                if (x < numX - 1)
                {
                    opp = Room.Directions.LEFT;
                    ++x;
                }
                break;
            case Room.Directions.BOTTOM:
                if (y > 0)
                {
                    opp = Room.Directions.TOP;
                    --y;
                }
                break;
            case Room.Directions.LEFT:
                if (x > 0)
                {
                    opp = Room.Directions.RIGHT;
                    --x;
                }
                break;
        }
        if (opp != Room.Directions.NONE)
        {
            rooms[x, y].SetDirFlag(opp, false);
        }
    }

    public List<Tuple<Room.Directions, Room>> GetNeighboursNotVisited(
      int cx, int cy)
    {
        List<Tuple<Room.Directions, Room>> neighbours =
          new List<Tuple<Room.Directions, Room>>();

        foreach (Room.Directions dir in Enum.GetValues(
          typeof(Room.Directions)))
        {
            int x = cx;
            int y = cy;

            switch (dir)
            {
                case Room.Directions.TOP:
                    if (y < numY - 1)
                    {
                        ++y;
                        if (!rooms[x, y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(
                              Room.Directions.TOP,
                              rooms[x, y]));
                        }
                    }
                    break;
                case Room.Directions.RIGHT:
                    if (x < numX - 1)
                    {
                        ++x;
                        if (!rooms[x, y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(
                              Room.Directions.RIGHT,
                              rooms[x, y]));
                        }
                    }
                    break;
                case Room.Directions.BOTTOM:
                    if (y > 0)
                    {
                        --y;
                        if (!rooms[x, y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(
                              Room.Directions.BOTTOM,
                              rooms[x, y]));
                        }
                    }
                    break;
                case Room.Directions.LEFT:
                    if (x > 0)
                    {
                        --x;
                        if (!rooms[x, y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(
                              Room.Directions.LEFT,
                              rooms[x, y]));
                        }
                    }
                    break;
            }
        }
        return neighbours;
    }

    private bool GenerateStep()
    {
        if (stack.Count == 0) return true;

        Room r = stack.Peek();
        var neighbours = GetNeighboursNotVisited(r.Index.x, r.Index.y);

        if (neighbours.Count != 0)
        {
            var index = 0;
            if (neighbours.Count > 1)
            {
                index = UnityEngine.Random.Range(0, neighbours.Count);
            }

            var item = neighbours[index];
            Room neighbour = item.Item2;
            neighbour.visited = true;
            RemoveRoomWall(r.Index.x, r.Index.y, item.Item1);

            stack.Push(neighbour);
        }
        else
        {
            stack.Pop();
        }

        return false;
    }

    public void CreateMaze()
    {

        RemoveRoomWall(numX - 1, numY - 1, Room.Directions.RIGHT);

        stack.Push(rooms[0, 0]);

        NodePiercing();
    }

    void NodePiercing()
    {
        bool flag = false;
        while (!flag)
        {
            flag = GenerateStep();
        }
    }
    class BFSCell
    {
        public int x;
        public int y;
        public BFSCell prev;
    }
    public Room[] GetShortestDistance(int x, int y)
    {
        int mazeSize = numX;
        BFSCell[,] visited = new BFSCell[mazeSize, mazeSize];
        Queue<BFSCell> cellList = new();

        var hostCell = new BFSCell { x = x, y = y, prev = null };
        visited[x, y] = hostCell;
        cellList.Enqueue(hostCell);

        int[] dirX = new int[] { 1, -1, 0, 0 };
        int[] dirZ = new int[] { 0, 0, 1, -1 };

        while (cellList.Count != 0)
        {
            var cell = cellList.Dequeue();
            if (cell.x == mazeSize - 1 && cell.y == mazeSize - 1)
            {
                List<Room> path = new();
                while (cell != null)
                {
                    path.Add(rooms[cell.x, cell.y]);
                    cell = cell.prev;
                }
                return path.ToArray();
            }

            for (int i = 0; i < 4; i++)
            {
                int nx = cell.x + dirX[i];
                int nz = cell.y + dirZ[i];

                if (nx < 0 || nx >= mazeSize || nz < 0 || nz >= mazeSize) continue;
                if (visited[nx, nz] != default) continue;
                if (CheckWall(rooms[cell.x, cell.y], rooms[nx, nz])) continue;

                var newCell = new BFSCell { x = nx, y = nz, prev = cell };
                visited[nx, nz] = newCell;
                cellList.Enqueue(newCell);
            }
        }

        return default;
    }
    bool CheckWall(Room curr, Room post)
    {
        if (curr.transform.position.x < post.transform.position.x)
        {
            return curr.rightWall.activeSelf;
        }

        if (curr.transform.position.x > post.transform.position.x)
        {
            return curr.leftWall.activeSelf;
        }

        if (curr.transform.position.y < post.transform.position.y)
        {
            return curr.topWall.activeSelf;
        }

        if (curr.transform.position.y > post.transform.position.y)
        {
            return curr.bottomWall.activeSelf;
        }

        return false;
    }
    public void ShowHint(int x, int y)
    {
        x = x / 7;
        y= y/ 7;
        StartCoroutine(IEShowHint(x, y));
    }
    IEnumerator IEShowHint(int x, int y)
    {
        var path = GetShortestDistance(x, y);
        foreach (var room in path)
        {
            room.ShowFloor();
        }
        yield return new WaitForSeconds(3.0f);
        foreach (var room in path)
        {
            room.HideFloor();
        }
    }
}