using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] string gameScene;
    [SerializeField] Vector3 speed = Vector3.one * 5f;
    private void Update()
    {
        transform.rotation = Quaternion.Euler(speed * Time.deltaTime) * transform.rotation;
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(gameScene);
    }
}
