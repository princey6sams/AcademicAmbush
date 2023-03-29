using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        tempPos.x = 12.6f;
        tempPos.y = 5.2f;
        StartCoroutine(MainMenuCameraMovement());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public IEnumerator MainMenuCameraMovement()
    {
        tempPos = transform.position;
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (tempPos.x >= 12.5)
            {
                while (tempPos.x >= 1.5)
                {
                    tempPos.x -= 0.005f;
                    transform.position = tempPos;
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else if (tempPos.x <= 1.5)
            {
                while (tempPos.x <= 12.5)
                {
                    tempPos.x += 0.005f;
                    transform.position = tempPos;
                    yield return new WaitForSeconds(0.01f);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        // if (PlayerController.Instance.transform.position.x <= 38.5 && PlayerController.Instance.transform.position.x >= -38.5) { tempPos.x = 0.7f * PlayerController.Instance.transform.position.x; }
        // if (PlayerController.Instance.transform.position.z <= 0.75 && PlayerController.Instance.transform.position.z >= -26.5) { tempPos.z = PlayerController.Instance.transform.position.z + 6; }


    }
}

