using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingScreen : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
