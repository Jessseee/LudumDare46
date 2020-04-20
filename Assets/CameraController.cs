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
        while (currentTime < shakeDuration)
        {
            originalPos = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            currentTime += Time.deltaTime;
            Debug.Log(currentTime);
            yield return 1;
        }
        transform.localPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        currentTime = 0.0f;
    }
}
