using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManger;
    public float difficulty;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        SceneManager.LoadScene("GameScene");
        gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManger.StartGame(difficulty);
    }
}
