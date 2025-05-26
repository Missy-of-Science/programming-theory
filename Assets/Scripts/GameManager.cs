using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;

    public GameObject raindrops;

    public GameObject cloud;

    private float horizontalInput;

    [SerializeField]
    private float speed;
    Vector3 cloud_size;

    // Start is called before the first frame update
    public void StartGame(float difficulty)
    {
        isGameActive = true;
        cloud_size = cloud.GetComponent<Renderer>().bounds.size;
        StartCoroutine(MakeItRain(difficulty));
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        cloud.transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        if (cloud.transform.position.x < 17)
        {
            cloud.transform.position = new Vector3(
                17,
                cloud.transform.position.y,
                cloud.transform.position.z
            );
        }
        else if (cloud.transform.position.x > 25)
        {
            cloud.transform.position = new Vector3(
                25,
                cloud.transform.position.y,
                cloud.transform.position.z
            );
        }
    }

    IEnumerator MakeItRain(float difficulty)
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(difficulty);
            Vector3 cloud_pos = cloud.transform.position;
            float zpos = Random.Range(cloud_pos.z - cloud_size.z, cloud_pos.z + cloud_size.z);
            Instantiate(
                raindrops,
                new Vector3(cloud_pos.x, cloud_pos.y, zpos),
                raindrops.transform.rotation
            );
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
