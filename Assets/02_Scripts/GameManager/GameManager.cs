using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] TextMeshProUGUI _PlayTimeText;

    float _currentTime;  // Play Time
    float _maxTime = 60f;
    float _secondTime = 0;
    int _minuteTime = 0;
    bool _isEscape = false;

    private void Update()
    {
        OnExitPanel();
        GetPlayTime();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    private void OnExitPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isEscape)
            {
                _panel.SetActive(true);
                _isEscape = true;
            }
            else
            {
                _panel.SetActive(false);
                _isEscape = false;
            }
        }
    }

    private void GetPlayTime()
    {
        _currentTime += Time.deltaTime;

        _secondTime = _currentTime % 60;

        if (_currentTime >= _maxTime)
        {
            _minuteTime += 1;
            _currentTime -= _maxTime;
        }

        _PlayTimeText.text = $"Time : {_minuteTime}.{(_secondTime):F2}";
    }
    
}
