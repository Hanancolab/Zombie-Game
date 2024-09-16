using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{

    [Header("Asset-Menu")]
	public GameObject pauseMenuUi;



    [Header("All Menu's")]
    public GameObject EndGameMenuUI;
    public GameObject ObjectiveMenuUI;
	public static bool IsPaused = false;



    public void Start()
	{

	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (IsPaused = !IsPaused)
			{
				Time.timeScale = 0.0f;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				IsPaused = false;
				pauseMenuUi.SetActive(true);
			}
        }
        else if (Input.GetKeyDown("tab"))
        {
            if (IsPaused)
            {
                removeObjectives();
                Cursor.lockState = CursorLockMode.Locked;

            }else
            {
                showObjectives();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

	//--------------------------------------------------------------//

	public void ResumeGame()
	{
		Time.timeScale = 1.0f;
		Cursor.visible = false;
		IsPaused = false;
		pauseMenuUi.SetActive(false);
	}

    public void showObjectives()
    {
        ObjectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void removeObjectives()
    {
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        IsPaused = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("QQQQ");
    }
}