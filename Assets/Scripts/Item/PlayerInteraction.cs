using System;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject curInteractGameObject;

    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private LayerMask layerMask;
    
    private IInteractable _curInteractable;
    private Camera _camera;
    private float _checkRate = 0.1f;
    private float _maxCheckDistance = 2.5f;
    private float _lastCheckTime = 0f;

    private void Start()
    {
        _camera = Camera.main;
        promptText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Time.time - _lastCheckTime < _checkRate)
        {
            _lastCheckTime = Time.time;
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    _curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                _curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }
    
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = _curInteractable.GetInteractPrompt();
    }
    
    public void Interacting()
    {
        if (_curInteractable != null)
        {
            _curInteractable.OnInteracting();
            curInteractGameObject = null;
            _curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
