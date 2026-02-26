using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public void Start()
    {
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsPress()
    {
        mainMenu.SetActive(false);
    }

    public void BackfromOptionMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mainMenu.SetActive(true);
        }
    }    
}
