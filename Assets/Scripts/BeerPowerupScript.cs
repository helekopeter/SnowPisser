using Unity.VisualScripting;
using UnityEngine;

public class BeerPowerupScript : MonoBehaviour
{
    private SpriteRenderer Sprite;
    private AudioSource Drinking;
    public PissManeger PissMan;
    private void Awake()
    {
        PissMan = FindAnyObjectByType<PissManeger>();
        Drinking = GetComponent<AudioSource>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PissMan.PissCounterUpdate(PissMan.MaxPiss);
        Drinking.Play();
        Sprite.enabled = false;
        Destroy(this.gameObject, 3.0f);
    }
}