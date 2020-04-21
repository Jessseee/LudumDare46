using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    InputController controls;

    private void Awake()
    {
        controls = new InputController();
    }

    void Start()
    {
        controls.UI.StartGame.performed += ctx => SceneManager.LoadScene(1);
    }
}
