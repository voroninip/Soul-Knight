using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public AudioSource music;
    public Button musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    private bool isMusicOn = true;

    private void Start()
    {
        musicButton.onClick.AddListener(ToggleMusic);
    }

    private void ToggleMusic()
    {
        if (isMusicOn)
        {
            music.Pause();
            musicButton.image.sprite = musicOffSprite;
        }
        else
        {
            music.Play();
            musicButton.image.sprite = musicOnSprite;
        }

        isMusicOn = !isMusicOn;
    }
}