using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterBoomBoom : MonoBehaviour
{
    [SerializeField] GameObject laserLeft, laserRight, laserCentre;
    [SerializeField] GameObject laserLeftTell, laserRightTell, laserCentreTell;

    private void OnEnable()
    {
        GameManager.instance.OnGameStarted.AddListener(EnableDaLaser);
        GameManager.instance.OnGameOver.AddListener(DisableDaLaser);
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStarted.RemoveListener(EnableDaLaser);
        GameManager.instance.OnGameOver.AddListener(DisableDaLaser);
    }

    void EnableDaLaser()
    {
        int randNum = Random.Range(0, 5);
        switch (randNum)
        {
            case 0:
                StartCoroutine(FireLaserLeft());
                break;
            case 1:
                StartCoroutine(FireLaserRight());
                break;
            case 2:
                StartCoroutine(FireLaserCentre());
                break;
            case 3:
                StartCoroutine(FireLaserAll());
                break;
            case 4:
                StartCoroutine(FireLaserAllMinusCentre());
                break;
            default:
                EnableDaLaser();
                break;
        }
    }
    void DisableDaLaser()
    {
        StopAllCoroutines();
    }

    IEnumerator FireLaserLeft()
    {
        float randNum = Random.Range(0, 20);
        yield return new WaitForSecondsRealtime(randNum);
        laserLeftTell.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        laserLeftTell.SetActive(false);
        laserLeft.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        laserLeft.SetActive(false);
        EnableDaLaser();
    }

    IEnumerator FireLaserRight()
    {
        float randNum = Random.Range(0, 20);
        yield return new WaitForSecondsRealtime(randNum);
        laserRightTell.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        laserRightTell.SetActive(false);
        laserRight.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        laserRight.SetActive(false);
        EnableDaLaser();
    }

    IEnumerator FireLaserCentre()
    {
        float randNum = Random.Range(0, 20);
        yield return new WaitForSecondsRealtime(randNum);
        laserCentreTell.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        laserCentreTell.SetActive(false);
        laserCentre.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        laserCentre.SetActive(false);
        EnableDaLaser();
    }

    IEnumerator FireLaserAll()
    {
        float randNum = Random.Range(0, 20);
        yield return new WaitForSecondsRealtime(randNum);
        laserLeftTell.SetActive(true);
        laserRightTell.SetActive(true);
        laserCentreTell.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        laserLeftTell.SetActive(false);
        laserRightTell.SetActive(false);
        laserCentreTell.SetActive(false);
        laserLeft.SetActive(true);
        laserRight.SetActive(true);
        laserCentre.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        laserLeft.SetActive(false);
        laserRight.SetActive(false);
        laserCentre.SetActive(false);
        EnableDaLaser();
    }


    IEnumerator FireLaserAllMinusCentre()
    {
        float randNum = Random.Range(0, 20);
        yield return new WaitForSecondsRealtime(randNum);
        laserLeftTell.SetActive(true);
        laserRightTell.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        laserLeftTell.SetActive(false);
        laserRightTell.SetActive(false);
        laserLeft.SetActive(true);
        laserRight.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        laserLeft.SetActive(false);
        laserRight.SetActive(false);
        EnableDaLaser();
    }

}
