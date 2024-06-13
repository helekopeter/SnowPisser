using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            UnlockNewLevel();
            SceneController.instance.NextLevel();
        }
    }

    
    void UnlockNewLevel()
    {
    if(SceneManager.GetActiveScene().buildIndex>=PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("reachedIndex",SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.SetInt("UnlockedLevel",PlayerPrefs.GetInt("UnlockedLevel",1)+1);
            PlayerPrefs.Save();
        }
    }
}

