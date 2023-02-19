using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Scroller : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] Vector2 velocity;

    // Update is called once per frame
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + velocity * Time.deltaTime, image.uvRect.size);
    }
}
