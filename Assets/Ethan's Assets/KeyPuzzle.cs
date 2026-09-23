using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadPuzzle : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [Tooltip("The correct code sequence, e.g. 1, 4, 2, 3")]
    [SerializeField] private List<int> correctCode = new List<int> { 1, 4, 2, 3 };

    [Header("Key / Unlock Object")]
    [Tooltip("The key object that will be enabled or unlocked when solved.")]
    [SerializeField] private GameObject keyObject;

    private List<int> currentInput = new List<int>();
    private bool isSolved = false;

    public void PressButton(int buttonNumber)
    {
        if (isSolved) return;

        currentInput.Add(buttonNumber);
        Debug.Log("Button pressed: " + buttonNumber + " | Current entry length: " + currentInput.Count);

        // Check if the entered digits match the correct sequence so far
        int index = currentInput.Count - 1;

        if (currentInput[index] != correctCode[index])
        {
            Debug.Log("Wrong code! Resetting keypad...");
            currentInput.Clear();
            return;
        }

        // Check if the complete code has been successfully entered
        if (currentInput.Count == correctCode.Count)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        isSolved = true;
        Debug.Log("Keypad Solved! Key unlocked.");

        if (keyObject != null)
        {
            // Activate or reveal the key
            keyObject.SetActive(true);
        }
    }
}