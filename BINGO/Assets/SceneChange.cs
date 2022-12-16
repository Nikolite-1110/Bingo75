using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneChange : MonoBehaviour
{

    public void StartBingo(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed){
            SceneManager.LoadScene("Bingo");
        }
    }

    public void ExitBingo(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed){
            Application.Quit();
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
