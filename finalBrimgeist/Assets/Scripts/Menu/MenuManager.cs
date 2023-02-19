using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] InputActionReference pauseRef;
    [SerializeField] InputActionReference cPanelRef;

    public static MenuManager current;

    private void Awake()
        
    {
        if (current == null) current = this;
        else Destroy(gameObject);
    }
    private void OnEnable()
    {
        pauseRef.action.Enable();
        cPanelRef.action.Enable();
    }
    private void OnDisable()
    {
        pauseRef.action.Disable();
        cPanelRef.action.Disable();
    }

    private void Update()
    {
        if (pauseRef.action.triggered)
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
            }
            else pauseMenu.SetActive(false);
        }

        if (cPanelRef.action.triggered)
        {
            if (!characterPanel.activeSelf)
            {
                pauseMenu.SetActive(true);
            }
            else pauseMenu.SetActive(false);
        }
    }

}
