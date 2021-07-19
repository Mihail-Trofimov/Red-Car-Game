using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private GameObject _panelSettings;
    [SerializeField] private GameObject _panelAbout;
    [SerializeField] private GameObject _panelStatus;

    private bool _menu;
    private bool _about;
    private bool _settings;

    void Start()
    {
        Cursor.visible = false;
        _menu = false;
        _about = false;
        _settings = false;
        _panelStatus.SetActive(true);
        _panelMenu.SetActive(false);
        _panelSettings.SetActive(false);
        _panelAbout.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menu)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            _menu = true;
            _panelStatus.SetActive(false);
            _panelMenu.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menu)
        {
            if (_settings)
            {
                _settings = false;
                _panelMenu.SetActive(true);
                _panelSettings.SetActive(false);
                _panelStatus.SetActive(false);
                _panelAbout.SetActive(false);
            }
            else if (_about)
            {
                _about = false;
                _panelMenu.SetActive(true);
                _panelSettings.SetActive(false);
                _panelStatus.SetActive(false);
                _panelAbout.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.visible = false;
                _menu = false;
                _panelStatus.SetActive(true);
                _panelMenu.SetActive(false);
                _panelAbout.SetActive(false);
                _panelSettings.SetActive(false);
            }
        }
    }

    public void OnSettings()
    {
        //StartCoroutine(IESettings());
        _settings = true;
        _panelSettings.SetActive(true);
        _panelMenu.SetActive(false);
        _panelStatus.SetActive(false);
        _panelAbout.SetActive(false);
    }
    IEnumerator IESettings()
    {
        yield return new WaitForSeconds(0.15f);
        _settings = true;
        _panelSettings.SetActive(true);
        _panelMenu.SetActive(false);
        _panelStatus.SetActive(false);
        _panelAbout.SetActive(false);
    }
    public void OnAbout()
    {
        //StartCoroutine(IEAbout());
        _about = true;
        _panelAbout.SetActive(true);
        _panelStatus.SetActive(false);
        _panelSettings.SetActive(false);
        _panelMenu.SetActive(false);
    }
    IEnumerator IEAbout()
    {
        yield return new WaitForSeconds(0.15f);
        _about = true;
        _panelAbout.SetActive(true);
        _panelStatus.SetActive(false);
        _panelSettings.SetActive(false);
        _panelMenu.SetActive(false);
    }
    public void OnMenu()
    {
        //StartCoroutine(IEMenu());
        _about = false;
        _settings = false;
        _panelMenu.SetActive(true);
        _panelAbout.SetActive(false);
        _panelStatus.SetActive(false);
        _panelSettings.SetActive(false);
    }
    IEnumerator IEMenu()
    {
        yield return new WaitForSeconds(0.15f);
        _about = false;
        _settings = false;
        _panelMenu.SetActive(true);
        _panelAbout.SetActive(false);
        _panelStatus.SetActive(false);
        _panelSettings.SetActive(false);
    }
    public void OnGame()
    {
        //StartCoroutine(IEGame());
        Time.timeScale = 1f;
        Cursor.visible = false;
        _about = false;
        _settings = false;
        _menu = false;
        _panelStatus.SetActive(true);
        _panelAbout.SetActive(false);
        _panelSettings.SetActive(false);
        _panelMenu.SetActive(false);
    }
    IEnumerator IEGame()
    {
        yield return new WaitForSeconds(0.15f);
        Time.timeScale = 1f;
        Cursor.visible = false;
        _about = false;
        _settings = false;
        _menu = false;
        _panelStatus.SetActive(true);
        _panelAbout.SetActive(false);
        _panelSettings.SetActive(false);
        _panelMenu.SetActive(false);

    }
    public void OnMainMenu()
    {
        //StartCoroutine(IEMainMenu());
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    IEnumerator IEMainMenu()
    {
        yield return new WaitForSeconds(0.15f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
