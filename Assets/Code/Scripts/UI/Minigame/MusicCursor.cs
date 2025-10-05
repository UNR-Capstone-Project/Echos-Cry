using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Buffers;

public class MusicCursor : MonoBehaviour
{
    private List<GameObject> activeNotes = new List<GameObject>();
    private InputAction noteHitAction;
    private bool isHitting;

    void Awake()
    {
        noteHitAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        noteHitAction.started += ctx => isHitting = true;
        noteHitAction.canceled += ctx => isHitting = false;
        noteHitAction.Enable();
    }

    void Update()
    {
        if (isHitting)
        {
            foreach (GameObject note in activeNotes)
            {
                note.GetComponent<MusicNote>().addHitTime(Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            activeNotes.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            activeNotes.Remove(other.gameObject);
        }
    }
}
