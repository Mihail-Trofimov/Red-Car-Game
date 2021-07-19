using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusPanel : MonoBehaviour
{
    [SerializeField] private PlayerMovements playerScript;
    [SerializeField] private Slider slider;
    [SerializeField] private Text hpText;
    void Start()
    {
        slider.value = playerScript.nitro;
        hpText.text = playerScript.plHP.ToString();
        StartCoroutine(Nitro());
        StartCoroutine(Health());
    }

    IEnumerator Nitro()
    {
        float _per = playerScript.nitro;
        while (this != null)
        {
            yield return new WaitUntil(() => _per != playerScript.nitro);
            slider.value = playerScript.nitro;
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator Health()
    {
        float _per = playerScript.plHP;
        while (this != null)
        {
            yield return new WaitUntil(() => _per != playerScript.plHP);
            hpText.text = playerScript.plHP.ToString();
            yield return new WaitForFixedUpdate();
        }
    }


}
