using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraShake : MonoBehaviour {

    //Thanks to https://forum.unity.com/threads/how-to-shake-camera-with-cinemachine.485724/

    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;
    public float lerp;

    void Start()
    {
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void shake(float shakeIntensity, float shakeTiming)
    {
        StartCoroutine(_ProcessShake(shakeIntensity, shakeTiming));
    }


    IEnumerator _ProcessShake(float shakeIntensity, float shakeTiming)
    {
        Noise(1, shakeIntensity);

        yield return new WaitForSeconds(shakeTiming - 1);

        //Dumbing down now
        while (noise.m_AmplitudeGain > 0.01 && noise.m_FrequencyGain > 0.01) 
        {
            Noise(Mathf.Lerp(noise.m_AmplitudeGain, 0, lerp * Time.deltaTime), Mathf.Lerp(noise.m_FrequencyGain, 0, lerp * Time.deltaTime));
            yield return null;
        }
        Noise(0, 0);
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }

}
