using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;
    public Text TimerText;
    public Button restartButton;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameWonText;

    public bool gameStarted = false;
    private int level = 0;
    private int time = 20;

    [SerializeField]
    int allFlowers;
    int hiddenFlowers;

    public void StartGame()
    {
        gameStarted = true;
        hiddenFlowers = allFlowers;
        StartCoroutine(Timer());
    }

    private void OnTriggerEnter(Collider other)
    {
        GrowPlants(other.gameObject.transform.position, 0.5f);

        Destroy(other.gameObject);
        if (level < 100)
        {
            level = 100 * (allFlowers - hiddenFlowers) / allFlowers;
            CounterText.text = $"Flowers grown: {level}%";
        }
        else if (DataPersistanceManager.Instance.isGameActive)
        {
            GameOver(true);
        }
    }

    void GrowPlants(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Plant"))
            {
                MeshRenderer renderer = collider.GetComponent<MeshRenderer>();
                if (!renderer.enabled)
                {
                    hiddenFlowers -= 1;
                    renderer.enabled = true;
                }
            }
        }
    }

    public IEnumerator Timer()
    {
        while (true)
        {
            TimerText.text = $"Seconds left: {time}";

            yield return new WaitForSeconds(1);

            time--;
            if (time < 0)
                GameOver(false);

            if (!DataPersistanceManager.Instance.isGameActive)
                break;
        }
    }

    void GameOver(bool won)
    {
        DataPersistanceManager.Instance.isGameActive = false;
        if (won)
        {
            gameWonText.gameObject.SetActive(true);
        }
        else
        {
            gameOverText.gameObject.SetActive(true);
        }

        restartButton.gameObject.SetActive(true);
    }
}
