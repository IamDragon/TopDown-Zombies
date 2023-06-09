using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] private float imageSpacing;
    private List<Image> images;
    private int lastIndex = 0;

    private void Awake()
    {
        images = new List<Image>();
    }

    protected void ShowIcon(Sprite icon)
    {
        if (lastIndex >= images.Count)
            CreateIcon(icon);
        else
            SetAndShowIcon(icon);
    }

    protected void SetAndShowIcon(Sprite icon)
    {
        images[lastIndex].sprite = icon;
        images[lastIndex].transform.gameObject.SetActive(true);
        lastIndex++;
    }

    protected void CreateIcon(Sprite icon)
    {
        Image newImage = Instantiate(image, image.transform.position, image.transform.rotation, transform);
        newImage.sprite = icon;
        newImage.transform.gameObject.SetActive(true);
        images.Add(newImage);
        newImage.transform.position = new Vector3(image.transform.position.x + imageSpacing * images.Count, image.transform.position.y, image.transform.position.z);
        lastIndex++;
    }

    protected void HideIcons()
    {
        foreach (Image image in images)
        {
            image.gameObject.SetActive(false);
        }
        lastIndex = 0;
    }

    protected void ShowIcons()
    {
        foreach (Image image in images)
        {
            image.gameObject.SetActive(true);
        }
        lastIndex = images.Count-1;
    }

    protected void HideLastIcon()
    {
        //need to check if lastIndex exist in array
        images[lastIndex].gameObject.SetActive(false);
        lastIndex--;
    }

    protected void ShowLastIcon()
    {

    }
}
