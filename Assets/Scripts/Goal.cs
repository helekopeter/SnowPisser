using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    //Unlocks the next level
    void UnlockNewLevel()
    {
    //Checks if in right level to unlock next
    if(PlayerPrefs.GetInt("UnlockedLevel", 1)<SceneManager.GetActiveScene().buildIndex+1)
        {
            Debug.Log(PlayerPrefs.GetInt("UnlockedLevel", 1));
            PlayerPrefs.SetInt("UnlockedLevel",PlayerPrefs.GetInt("UnlockedLevel", 1)+1);
            Debug.Log(PlayerPrefs.GetInt("UnlockedLevel", 1));
            PlayerPrefs.Save();
        }
    }

//Moves player to next level when hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            UnlockNewLevel();
            SceneController.instance.NextLevel();
            Destroy(this);
        }
    }

    

}

