using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class RestartZone : MonoBehaviour
{
    //Refere to MainMenu script
public MainMenu menu;

    private void Awake()
    {
        menu=FindAnyObjectByType<MainMenu>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Execute order 66
            menu.GameOverScreen();
        }
    }
}
