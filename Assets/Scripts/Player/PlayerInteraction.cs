using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    PlayerController Controller;
    public GameObject Endpoint;
    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Score":
                collision.gameObject.SetActive(false);
                gameManager.ScoreUp();
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
                collision.gameObject.SetActive(false);
                ShowHint();
                break;
            case "Finish":
                SceneManager.LoadScene("MainScenes");
                break;
        }

    }
    void ShowHint()
    {

    }
    IEnumerator PlayerBomb()
    {
        Controller.PlayerMove = false;
        yield return new WaitForSeconds(3.0f);
        Controller.PlayerMove = true;
    }
}
