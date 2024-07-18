using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum Directions
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT,
        NONE,
    }
    public enum Type
    {
        Classic,
        Move
    }
    [SerializeField]
    public GameObject topWall;
    [SerializeField]
    public GameObject rightWall;
    [SerializeField]
    public GameObject bottomWall;
    [SerializeField]
    public GameObject leftWall;
    public GameObject floor;
    [SerializeField]
    Type type;
    int ran;
    Dictionary<Directions, GameObject> walls =
      new Dictionary<Directions, GameObject>();

    public Vector2Int Index
    {
        get;
        set;
    }

    public bool visited { get; set; } = false;
    Dictionary<Directions, bool> dirflags =
    new Dictionary<Directions, bool>();
    public Type CellType { get; private set; }
    private void Awake()
    {
        walls[Directions.TOP] = topWall;
        walls[Directions.RIGHT] = rightWall;
        walls[Directions.BOTTOM] = bottomWall;
        walls[Directions.LEFT] = leftWall;
        if (gameObject.transform.position.x == 0 || gameObject.transform.position.x == GameManager.numX - 1 || 
            gameObject.transform.position.y == 0 || gameObject.transform.position.y == GameManager.numY - 1)
        {
            type = Type.Classic;
        }
        else if (Random.Range(0, 15) == 0)
            type = Type.Move;
        else
            type = Type.Classic;
        if (type == Type.Move)
        {
            StartCoroutine(Movewall());
        }
    }
    private void SetActive(Directions dir, bool flag)
    {
        walls[dir].SetActive(flag);
    }

    public void SetDirFlag(Directions dir, bool flag)
    {
        dirflags[dir] = flag;
        SetActive(dir, flag);
    }
    public void ShowFloor()
    {
        floor.SetActive(true);
    }

    public void HideFloor()
    {
        floor.SetActive(false);
    }
    IEnumerator Movewall()
    {
        while (true)  // 무한 루프로 변경
        {
            yield return null;
            List<GameObject> falseWalls = new List<GameObject>();

            // 벽 상태 확인 및 리스트에 추가
            CheckAndAddWall(topWall, falseWalls);
            CheckAndAddWall(bottomWall, falseWalls);
            CheckAndAddWall(rightWall, falseWalls);
            CheckAndAddWall(leftWall, falseWalls);

            // 벽 상태 변경
            SetWallsActiveTrue(falseWalls);

            yield return new WaitForSeconds(3.0f);

            // 벽 상태 원복
            SetWallsActiveFalse(falseWalls);

            yield return new WaitForSeconds(3.0f);
        }
    }

    private void CheckAndAddWall(GameObject wall, List<GameObject> falseWalls)
    {
        if (wall != null)
        {
            if (!wall.activeSelf)
                falseWalls.Add(wall);
        }
    }

    void SetWallsActiveTrue(List<GameObject> walls)
    {
        ran = Random.Range(0, walls.Count);
        {
            walls[ran].SetActive(true);
        }
    }
    void SetWallsActiveFalse(List<GameObject> walls)
    {
        walls[ran].SetActive(false);
    }
}