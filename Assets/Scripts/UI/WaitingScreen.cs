using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WaitingScreen : MonoBehaviour
    {
        public void Show()
        {
            GameManager.Instance.ChangeCurrentGameState(GameState.Paused);

            gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
