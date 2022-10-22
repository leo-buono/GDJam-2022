using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] float speed = 5;
    private void Update()
    {
        transform.rotation = Quaternion.Euler(Time.deltaTime * speed, Time.deltaTime * speed, Time.deltaTime * speed) * transform.rotation;
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("TestScene");
    }
}
