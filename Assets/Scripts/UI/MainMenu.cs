using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsPress()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackfromOptionMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }    
}
