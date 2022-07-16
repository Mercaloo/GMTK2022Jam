using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private bool muted = false;
    [SerializeField] Camera mainCamera;
    [SerializeField] Sprite unmutedButton;
    [SerializeField] Sprite mutedButton;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleMute);
        GetComponent<SpriteRenderer>().sprite = unmutedButton;
    }

    void ToggleMute()
    {
        mainCamera.GetComponent<AudioListener>().enabled = muted;
        muted = !muted;
        if (muted)
        {
            GetComponent<SpriteRenderer>().sprite = mutedButton;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = unmutedButton;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }
    }
     
}
