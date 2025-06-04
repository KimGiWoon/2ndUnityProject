using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    [SerializeField] GameObject _clearWindow;
    [SerializeField] GameObject _optionWindow;
    [SerializeField] GameObject _clearObject;
    [SerializeField] GameObject _scoreText;
    [SerializeField] CatController _catCon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.position = _catCon._firstPosition;
            _optionWindow.SetActive(false);
            _clearWindow.SetActive(true);
            _clearObject.SetActive(true);
            _scoreText.SetActive(false);
        } 
    }

}
