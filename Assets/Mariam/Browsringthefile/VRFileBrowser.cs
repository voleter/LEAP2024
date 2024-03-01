using UnityEngine;
using System.Collections;
using System.IO;
using SimpleFileBrowser;
using UnityEngine.UI;

public class VRFileBrowser : MonoBehaviour
{
    public RawImage imageDisplay;

    // Reference to the button that triggers the file browser
    public Button fileBrowserButton;

    void Awake()
    {
        #if !UNITY_EDITOR && UNITY_ANDROID
        typeof(SimpleFileBrowser.FileBrowserHelpers).GetField("m_shouldUseSAF", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, (bool?)false);
        #endif

        // Add listener to the button's click event
        fileBrowserButton.onClick.AddListener(ShowFileBrowser);
    }

    void ShowFileBrowser()
    {
        // Set filter for image files only
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Image Files", ".png", ".jpg", ".jpeg"));

        // Show a select file dialog using coroutine approach
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from the user
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select Image", "Load");

        // Check if the user successfully selected a file
        if (FileBrowser.Success)
        {
            // Get the path of the selected file
            string[] filePaths = FileBrowser.Result;
            string filePath = filePaths[0]; // Assuming only one file is selected

            // Load and display the image file
            LoadAndDisplayImage(filePath);
        }
    }

    void LoadAndDisplayImage(string filePath)
    {
        // Read the image file and load it into a Texture2D
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Display the image in the RawImage component
        imageDisplay.texture = texture;
    }
}
