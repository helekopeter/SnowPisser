using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class RestartZone : MonoBehaviour
{
 private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //How do I refere to the game over screen?!?!?!!
            GetComponent<MainMenu>().GameOverScreen();
        }
    }
}
