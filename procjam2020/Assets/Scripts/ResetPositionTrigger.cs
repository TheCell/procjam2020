using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionTrigger : MonoBehaviour
{
    private PlayerController playerController;

    public void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerController.ResetPosition();
    }
}
