using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeElapsed = 0f;
    private bool isTiming = true;
    public TextMeshProUGUI timerText;


    void Update()
    {
        if (isTiming)
        {
            timeElapsed += Time.deltaTime;
            timerText.text = timeElapsed.ToString("F2") + "s";
        }
    }

    public void StopTimer()
    {
        isTiming = false;
        Debug.Log("Level finished in " + timeElapsed.ToString("F2") + " seconds.");
    }
}
