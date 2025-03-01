using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInteraction : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject button;
    private void Awake()
    {
        // Initially hide the button
        button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("Player entered NPC area!");
            button.SetActive(true); // Show interaction button
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("Player left NPC area!");
            button.SetActive(false); // Hide interaction button
        }
    }
}
