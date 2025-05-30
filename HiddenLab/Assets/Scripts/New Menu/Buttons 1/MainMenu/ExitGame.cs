using UnityEngine;


public class ExitGame : MonoBehaviour
{
    public void OnClick()
    {
        //Leves game
        print("game quit");
        Application.Quit();        
    }
}
