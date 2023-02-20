using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSystem : MonoBehaviour
{
    [SerializeField] Vector2 velocity;
    Vector2 offset;
    Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    private void FixedUpdate()
    {
        material.mainTextureOffset += new Vector2(velocity.x, 0);
    }
}
