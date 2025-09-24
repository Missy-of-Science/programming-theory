using System.Collections;
using System.Collections.Generic;
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
    private int percentage = 0;
    private int time = 25;

    private List<GameObject> plants = new List<GameObject>();
    private int allPlants;
    private int growingPlants;

    public void StartGame()
    {
        gameStarted = true;
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Plant"))
        {
            plants.Add(fooObj);
        }
        allPlants = plants.Count;
        growingPlants = allPlants;
        StartCoroutine(Timer());
    }

    private void OnTriggerEnter(Collider other)
    {
        GrowPlants(other.gameObject.transform.position, 0.6f);

        Destroy(other.gameObject);
        if (percentage < 100)
        {
            percentage = 100 * (allPlants - growingPlants) / allPlants;
            CounterText.text = $"Plants grown: {percentage}%";
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
            Plant plant = collider.gameObject.GetComponent<Plant>();
            if (plant)
            {
                if (!plant.GetFullyGrown())
                {
                    plant.Grow();
                }
                else
                {
                    plants.Remove(collider.gameObject);
                    growingPlants = plants.Count;
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
