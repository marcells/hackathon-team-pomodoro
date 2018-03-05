using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainViewModel : MonoBehaviour
{
    public Button LoginButton;
    public InputField UserInputField;
    public int MaxLoginName;

    // Use this for initialization
    void Start ()
    {
        LoginButton.onClick.AddListener(Login);
    }

    private bool ValidateName(string name)
    {
        return !string.IsNullOrEmpty(name) && name.Length < MaxLoginName;
    }

    private void Login()
    {        

        if (ValidateName(UserInputField.textComponent.text))
        {
            Debug.Log("user loged in: " + UserInputField.textComponent.text);
            PlayerPrefs.SetString("UserName", UserInputField.text);
            StartCoroutine(LoadYourAsyncScene());
        }
        else
        {
            Debug.LogError("7xxxx xxxx error");
        }
        
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Pomodoro");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
