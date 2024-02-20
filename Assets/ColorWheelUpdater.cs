using UnityEngine;
using UnityEngine.UI;

public class ColorWheelUpdater : MonoBehaviour
{
    public Image colorWheelImage; // Drag and drop your Color Wheel Image here through the Inspector

    // Method to update the color wheel's color
    public void UpdateColor(Color newColor)
    {
        if (colorWheelImage != null)
        {
            colorWheelImage.color = newColor;
        }
    }
}
