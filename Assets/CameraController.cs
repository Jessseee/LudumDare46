using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    Vector3 originalPos;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    float currentTime = 0.0f;
    public IEnumerator Shake(float shakeDuration = 0.2f, float decreaseFactor = 0.3f, float shakeAmount = 0.1f)
    {
        originalPos = transform.localPosition;
        while (currentTime < shakeDuration)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            currentTime += Time.deltaTime;
            Debug.Log(currentTime);
            yield return 1;
        }
        transform.localPosition = originalPos;
        currentTime = 0.0f;
    }
}
