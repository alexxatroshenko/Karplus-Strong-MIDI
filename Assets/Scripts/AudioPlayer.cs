using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioEditor audioEditor;
    private KeyRaycaster raycaster;

    private KeyCode[] codes =
    {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7,
        KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0,
        KeyCode.Minus, KeyCode.Equals, KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U,
        KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket
    };
    private GameObject key;
    [SerializeField] private List<AudioClip> stringClips;
    [SerializeField] private TextMeshPro displayText;
    private bool pressed;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioEditor= GetComponent<AudioEditor>();
    }
    void Start()
    {
        raycaster = GetComponent<KeyRaycaster>();
        stringClips = audioEditor.GetStringClips();
    }
    void Update()
    {
        PlayMouseButtonKeys();
        PlayKeyboardKeys();
    }

    private void PlayMouseButtonKeys()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit = raycaster.Raycast();
            if (hit.collider != null)
            {
                if (!pressed && !hit.collider.CompareTag("background") && !hit.collider.CompareTag("base"))
                {
                    PressMouseButtonKey(hit);
                }

                if ((hit.collider.CompareTag("background")) && pressed)
                {
                    UnpressMouseButtonKey();
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && pressed)
        {
            UnpressMouseButtonKey();
        }
    }

    private void PlayKeyboardKeys()
    {
        for (int i = 0; i < audioEditor.GetStringClips().Count; i++)
        {
            if (Input.GetKeyDown(codes[i]))
            {
                PressKeyBoardKey(i);
            }

            if (Input.GetKeyUp(codes[i]))
            {
                UnpressKeyBoardKey(i);
            }
        }
    }

    private void PressKeyBoardKey(int i)
    {
        audioSource.PlayOneShot(stringClips[i]);
        displayText.text = stringClips[i].name;
        key = GameObject.FindWithTag(i.ToString());
        key.transform.Rotate(new Vector3(2f, 0f, 0f));
    }

    private void UnpressKeyBoardKey(int i)
    {
        key = GameObject.FindWithTag(i.ToString());
        key.transform.Rotate(new Vector3(-2f, 0f, 0f));
    }

    private void PressMouseButtonKey(RaycastHit hit)
    {
        audioSource.PlayOneShot(stringClips[int.Parse(hit.collider.tag)]);
        displayText.text = stringClips[int.Parse(hit.collider.tag)].name;
        key = GameObject.FindWithTag(hit.collider.tag);
        key.transform.Rotate(new Vector3(2f, 0f, 0f));
        pressed = true;
    }

    private void UnpressMouseButtonKey()
    {
        key.transform.Rotate(new Vector3(-2f, 0f, 0f));
        pressed = false;
    }
}
