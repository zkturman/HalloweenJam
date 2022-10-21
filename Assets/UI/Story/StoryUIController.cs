using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StoryUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Label storyText;
    private Label playerSkipMessage;
    [SerializeField]
    private float fadeTimeInSeconds = 2f;
    [System.Serializable]
    private class StoryBlock
    {
        public StoryBlock(string text, float durationInSeconds)
        {
            this.Text = text;
            this.DurationInSeconds = durationInSeconds;
        }

        [TextArea(10, 10)]
        public string Text;
        [Range(0f, 20f)]
        public float DurationInSeconds;
    }
    [SerializeField]
    private StoryBlock[] storyBlocks = { new StoryBlock("Enter text in editor", 10f) };
    [SerializeField]
    private string nextSceneName;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        storyText = new MessageLabel("StoryDetails").GenerateLabel(rootVisualElement);
        playerSkipMessage = new MessageLabel("SkipLabel").GenerateLabel(rootVisualElement);
        StartCoroutine(playStoryBlocks());
    }
    private IEnumerator playStoryBlocks()
    {
        yield return new WaitForSeconds(1f);
        foreach(StoryBlock block in storyBlocks)
        {
            storyText.text = block.Text;
            storyText.ToggleInClassList("FadeTransition");
            float secondsToWait = fadeTimeInSeconds + block.DurationInSeconds;
            yield return new WaitForSeconds(secondsToWait);
            storyText.ToggleInClassList("FadeTransition");
            yield return new WaitForSeconds(fadeTimeInSeconds);
        }
        SceneManager.LoadScene(nextSceneName);
    }

    public void EnableSkip()
    {
        playerSkipMessage.ToggleInClassList("FadeTransition");
    }
}
