using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour
{

    public Camera curCamera;
    public GameObject phoneApps;
    public GameObject videoObjects;
    public UnityEngine.Video.VideoPlayer videoPlayer;
    private bool isVideo;

    // Start is called before the first frame update
    void Start()
    {

        videoPlayer = curCamera.GetComponent<UnityEngine.Video.VideoPlayer>();
        isVideo = false;
    }

    public void stopFitness()
    {

        videoObjects.SetActive(false);
        phoneApps.SetActive(true);
        videoPlayer.Stop();
        isVideo = false;
    }

    public void playFitness()
    {
        videoObjects.SetActive(true);
        phoneApps.SetActive(false);
        videoPlayer.Play();
        isVideo = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isVideo)
        {
            stopFitness();
        }
    }
}
