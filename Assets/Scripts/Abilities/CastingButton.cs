using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CastingButton : MonoBehaviour
{
    public CastingButton instance;
    public Image cooldownImage;
    public int cooldownTime;

    public Stopwatch cooldownTimer;

    void Start()
    {
        instance = this;
        cooldownImage.fillAmount = 0;
    }

    public void StartCooldown()
    {
        this.GetComponent<Button>().interactable = false;
        cooldownImage.fillAmount = 1;
        cooldownTimer = new Stopwatch();
        cooldownTimer.Start();

        StartCoroutine(SpinImage());
    }

    IEnumerator SpinImage()
    {
        while (cooldownTimer.IsRunning && cooldownTimer.Elapsed.TotalSeconds < cooldownTime)
        {
            cooldownImage.fillAmount = ((float)cooldownTimer.Elapsed.TotalSeconds / cooldownTime);
            yield return null;
        }
        cooldownImage.fillAmount = 0;
        this.GetComponent<Button>().interactable = true;
        cooldownTimer.Stop();
        cooldownTimer.Reset();

        yield return null;
    }
}
