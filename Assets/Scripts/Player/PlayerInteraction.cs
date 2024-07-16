using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    PlayerController Controller;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Score":
                gameManager.ScoreUp();
                collision.gameObject.SetActive(false);
                break;
            case "Time":
                gameManager.TimeUp();
                collision.gameObject.SetActive(false);
                break;
            case "Bomb":
                StartCoroutine(PlayerBomb());
                collision.gameObject.SetActive(false);
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
