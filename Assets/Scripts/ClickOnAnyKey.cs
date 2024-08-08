using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClickOnAnyKey : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown) // A key was pressed.
        {
            // Make sure the key wouldn't also click the button if the key is Submit and the button is currently selected:
            var isCurrentlySelected = EventSystem.current.currentSelectedGameObject == this.gameObject;
            if (!(isCurrentlySelected && Input.GetButtonDown("Submit")))
            {
                // Simulate a button click:
                GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
