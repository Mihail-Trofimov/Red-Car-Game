using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void OnPlay()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(1);
    }

    public void OnSettings(GameObject _settings)
    {
        StartCoroutine(Settings(_settings));

    }
    IEnumerator Settings(GameObject _settings)
    {
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
        _settings.SetActive(true);
    }


    public void OnAbout(GameObject _about)
    {
        StartCoroutine(About(_about));
    }
    IEnumerator About(GameObject _about)
    {
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
        _about.SetActive(true);
    }

    public void OnExit()
    {
        StartCoroutine(Exit());
    }
    IEnumerator Exit()
    {
        yield return new WaitForSeconds(0.15f);
        Application.Quit();
    }
}
