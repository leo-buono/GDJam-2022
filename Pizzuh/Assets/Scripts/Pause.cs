using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private InputAction pause;
    [SerializeField] private GameObject canvas;
    [SerializeField] private float pauseTime = 0.05f;
    private bool isPaused = false;


    private void Awake()
    {
        pause.started += ctx =>
        {
            if (!isPaused)
            {
                isPaused = true;
                canvas.SetActive(true);
                Time.timeScale = pauseTime;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Resume();
            }
        };
        pause.Enable();
        canvas.SetActive(false);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        SceneManager.LoadScene("TestScene");
    }

    private void OnDestroy()
    {
        pause.Disable();
        pause.Dispose();
    }

}
