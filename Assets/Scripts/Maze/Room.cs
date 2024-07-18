using System.Collections;
using System.Collections.Generic;
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

  [SerializeField]
  public GameObject topWall;
  [SerializeField]
  public GameObject rightWall;
  [SerializeField]
  public GameObject bottomWall;
  [SerializeField]
  public GameObject leftWall;
    public GameObject floor;
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

  private void Awake()
  {
    walls[Directions.TOP] = topWall;
    walls[Directions.RIGHT] = rightWall;
    walls[Directions.BOTTOM] = bottomWall;
    walls[Directions.LEFT] = leftWall;
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
}
