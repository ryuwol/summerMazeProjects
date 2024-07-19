using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    GenerateMaze GenerateMaze;
    GameManager gameManager;
    [SerializeField]
    PlayerController Controller;
    public GameObject Endpoint;
    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
        GenerateMaze = GameObject.Find("GenerateMaze").GetComponent<GenerateMaze>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Score":
                collision.gameObject.SetActive(false);
                gameManager.ScoreUp();
                AudioSource CoinSound = GetComponent<AudioSource>();
                CoinSound.Play();
                break;
            case "Time":
                collision.gameObject.SetActive(false);
                gameManager.TimeUp();
                break;
            case "Bomb":
                StartCoroutine(PlayerBomb());
                collision.gameObject.SetActive(false);
                break;
            case "Hint":
                GenerateMaze.ShowHint(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
                collision.gameObject.SetActive(false);
                break;
            case "Finish":
                if (GameManager.highScore <= GameManager.Score)
                {
                    GameManager.highScore = GameManager.Score;
                    GameManager.highTime = GameManager.GameTime;
                    GameManager.highDiff = GameManager.difiicult;
                }
                SceneManager.LoadScene("MainScenes");
                break;
        }

    }
    IEnumerator PlayerBomb()
    {
        Controller.PlayerMove = false;
        yield return new WaitForSeconds(3.0f);
        Controller.PlayerMove = true;
    }
}
