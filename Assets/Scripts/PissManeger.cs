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
    public int Piss;
    public int MaxPiss=6;
    


  public void PissCounterUpdate(int newPiss)
  {
    Piss=newPiss;
    StoredPiss.SetText("Piss: "+newPiss);
    slider.value=newPiss;
  }
void Start()
{
        SetMaxPiss(MaxPiss);
        PissCounterUpdate(Piss);
}

  public void SetMaxPiss(int newMaxPiss)
  {
   MaxPiss = newMaxPiss; 
   slider.maxValue=MaxPiss;
   slider.minValue = 0;
  }
}
