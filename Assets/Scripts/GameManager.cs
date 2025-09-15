using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject raindrops;
    public GameObject cloud;
    private Counter counter;
    private float horizontalInput;

    [SerializeField]
    private float speed;
    Vector3 cloud_size;

    // Start is called before the first frame update
    public void Start()
    {
        cloud_size = cloud.GetComponent<Renderer>().bounds.size;
        counter = GameObject.Find("Plane").GetComponent<Counter>();
        StartCoroutine(MakeItRain());
    }

    // Update is called once per frame
    void Update()
    {
        if (
            DataPersistanceManager.Instance != null
            && DataPersistanceManager.Instance.isGameActive == true
        )
        {
            if (!counter.gameStarted)
            {
                counter.StartGame();
            }
            MoveCloud();
        }
    }

    IEnumerator MakeItRain()
    {
        while (DataPersistanceManager.Instance.isGameActive)
        {
            yield return new WaitForSeconds(DataPersistanceManager.Instance.difficulty);
            Vector3 cloud_pos = cloud.transform.position;
            float zpos = Random.Range(cloud_pos.z - cloud_size.z, cloud_pos.z + cloud_size.z);
            Instantiate(
                raindrops,
                new Vector3(cloud_pos.x, cloud_pos.y, zpos),
                raindrops.transform.rotation
            );
        }
    }

    private void MoveCloud()
    {
        int maxLeft = 17;
        int maxRight = 25;

        horizontalInput = Input.GetAxis("Horizontal");
        cloud.transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        if (cloud.transform.position.x < maxLeft)
        {
            cloud.transform.position = new Vector3(
                maxLeft,
                cloud.transform.position.y,
                cloud.transform.position.z
            );
        }
        else if (cloud.transform.position.x > maxRight)
        {
            cloud.transform.position = new Vector3(
                maxRight,
                cloud.transform.position.y,
                cloud.transform.position.z
            );
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
