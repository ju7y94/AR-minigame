using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshPro randomNumberText;
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI progressText;

    private List<int> availableNumbers; // Use List instead of int[]
    private int targetNumber;

    void Start()
    {
        InitializeAvailableNumbers();
        GenerateRandomNumber();
        UpdateRandomNumberText();
        StartCoroutine(ScaleText());
    }

    void InitializeAvailableNumbers()
    {
        AudioManager.Instance.PlayIntro();
        promptText.text = "Tap and match the number";
        // Initialize the list with numbers from 1 to 10
        availableNumbers = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            availableNumbers.Add(i);
        }
    }

    void GenerateRandomNumber()
    {
        if (availableNumbers.Count == 5)
        {
            AudioManager.Instance.PlayHalfWay();
        }

        // Check if there are available numbers left
        if (availableNumbers.Count > 0)
        {
            // Choose a random index from the available numbers list
            int randomIndex = Random.Range(0, availableNumbers.Count);
            
            // Set the target number from the chosen index
            targetNumber = availableNumbers[randomIndex];
            
            // Remove the chosen number from the available numbers list
            availableNumbers.RemoveAt(randomIndex);
            RefreshProgress();
        }
        else
        {
            AudioManager.Instance.PlayCongrats();
            promptText.text = "Yeah! I knew I can count on you !";
            progressText.text = "Congratulations!";
            LoadMainMenuScene();

            // All numbers have been identified
            // THE END
        }

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 randomPosition;
        
        do
        {
            randomPosition = new Vector3(Random.Range(-15f, 15f) + cameraPosition.x, Random.Range(0f, 2f) + 1f, Random.Range(-15f, 15f) + cameraPosition.z);
        } while (Vector3.Distance(randomPosition, cameraPosition) < 10f);
        
        randomNumberText.transform.position = randomPosition;
        randomNumberText.transform.forward = randomNumberText.transform.position - cameraPosition;
    }

    void UpdateRandomNumberText()
    {
        // Display the random number in the UI Text element
        randomNumberText.text = targetNumber.ToString();
    }

    void RefreshProgress()
    {
        progressText.text = "You have " + (availableNumbers.Count + 1) + " numbers left to find";

        if( availableNumbers.Count == 0)
        progressText.text = "You have " + (availableNumbers.Count + 1) + " number left to find";
    }

    public void OnButtonClick(int buttonNumber)
    {
        // Called when a button is clicked
        if (buttonNumber == targetNumber)
        {
            promptText.color = new Color(0, 175, 0);
            randomNumberText.color = new Color(0, 175, 0);
            promptText.text = "Nice, keep going!";
            AudioManager.Instance.PlayCorrectMatch();
            // You can add more logic here for what happens when the correct button is pressed.
            // For example, you might want to generate a new random number and update the text.
            
            GenerateRandomNumber();
            UpdateRandomNumberText();
            RefreshProgress();
        }
        else
        {
            promptText.color = new Color(175, 0, 0);
            randomNumberText.color = new Color(175, 0, 0);
            promptText.text = "Come on! I know you can!";
            AudioManager.Instance.PlayWrongMatch();
            // You can add more logic here for what happens when the incorrect button is pressed.
            // For example, you might want to display a message or perform some other action.
        }
    }

    private IEnumerator ScaleText()
    {
        while (true)
        {
            // Scale up
            while (randomNumberText.transform.localScale.x < 1.5f)
            {
                float newScale = randomNumberText.transform.localScale.x + (0.5f * Time.deltaTime);
                randomNumberText.transform.localScale = new Vector3(newScale, newScale, 1f);
                yield return null;
            }

            // Scale down
            while (randomNumberText.transform.localScale.x > 1.0f)
            {
                float newScale = randomNumberText.transform.localScale.x - (0.5f * Time.deltaTime);
                randomNumberText.transform.localScale = new Vector3(newScale, newScale, 1f);
                yield return null;
            }
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}