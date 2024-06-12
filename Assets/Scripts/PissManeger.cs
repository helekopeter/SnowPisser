using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PissManeger : MonoBehaviour
{
    //Reference the piss slider
    public Slider slider;

    //Reference the piss counter
    public TextMeshProUGUI StoredPiss;

    //Referece the piss value
    public int Piss=3;
    public int MaxPiss=6;

void Start()
{
    PissCounterUpdate(Piss);
}

  public void PissCounterUpdate(int newPiss)
  {
    Piss=newPiss;
    StoredPiss.text="Piss: "+Piss;
    slider.value=Piss;
  }

  public void SetMaxPiss(int Piss)
  {
    slider.maxValue=MaxPiss;
  }



}
