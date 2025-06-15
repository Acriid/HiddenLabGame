using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
public class CharacterButtonSelection : MonoBehaviour
{
    public List<Button> characterButtons;
    private int currentIndex = 0;

    void Start()
    {
        if (characterButtons.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(characterButtons[currentIndex].gameObject);

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {

            currentIndex = (currentIndex + 1) % characterButtons.Count;
            EventSystem.current.SetSelectedGameObject(characterButtons[currentIndex].gameObject);  

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentIndex = (currentIndex - 1 + characterButtons.Count) % characterButtons.Count;
            EventSystem.current.SetSelectedGameObject(characterButtons[currentIndex].gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            characterButtons[currentIndex].onClick.Invoke();
        }

    }
}






