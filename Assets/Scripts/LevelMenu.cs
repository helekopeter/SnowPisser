using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;

    private void Awake()
    {
        ButtonToArray();
        int unlockedLevel=PlayerPrefs.GetInt("UnlockedLevel", 1);
        Debug.Log(unlockedLevel);
        for(int i=0;i<buttons.Length;i++)
        {
            buttons[i].interactable=false;
        }
        for(int i=0;i<unlockedLevel;i++)
        {
            buttons[i].interactable=true;
        }
    }

    public void OpenLevel(int levelID)
    {
        string levelName="Level"+levelID;
        SceneManager.LoadScene(levelName);
    }

    void ButtonToArray()
    {
        int childCount=levelButtons.transform.childCount;
        buttons=new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i]=levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
