using Unity.VisualScripting;
using UnityEngine;

public class BeerPowerupScript : MonoBehaviour
{
    public PissManeger PissMan;
    private void Awake()
    {
        PissMan = FindAnyObjectByType<PissManeger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PissMan.PissCounterUpdate(PissMan.MaxPiss);
        Destroy(this.gameObject);
    }
}