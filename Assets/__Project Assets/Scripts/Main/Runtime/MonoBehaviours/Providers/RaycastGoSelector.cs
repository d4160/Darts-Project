using UnityEngine;

public class RaycastGoSelector : GameObjectSelector
{
    [SerializeField] protected LayerMask _mask;
    [SerializeField] protected string _tag;

    public override void CheckSelection()
    {
        CheckIn();

        CheckOut();
    }

    private void CheckIn()
    {
        bool pointerDown = false;

#if UNITY_STANDALONE || UNITY_WEBGL
        pointerDown = Input.GetMouseButtonDown(0);
#else
        pointerDown = Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began;
#endif
        if (pointerDown)
        {
            Vector3 pointerPos = default;

#if UNITY_STANDALONE || UNITY_WEBGL
            pointerPos = Input.mousePosition;
#else
            pointerPos = Input.GetTouch(0).position;
#endif

            Ray ray = Camera.main.ScreenPointToRay(pointerPos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, _mask))
            {
                bool valid = true;

                if (!string.IsNullOrEmpty(_tag))
                {
                    valid = hit.transform.gameObject.CompareTag(_tag);
                }

                if (valid)
                {
                    if (!_selectedObject)
                    {
                        _selectedObject = hit.transform.gameObject;

                        onSelected?.Invoke(_selectedObject);
                    }
                }
            }
        }
    }

    private void CheckOut()
    {
        bool pointerUp = false;

#if UNITY_STANDALONE || UNITY_WEBGL
        pointerUp = Input.GetMouseButtonUp(0);
#else
        pointerUp = Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended;
#endif

        if (pointerUp)
        {
            Vector3 pointerPos = default;

#if UNITY_STANDALONE || UNITY_WEBGL
            pointerPos = Input.mousePosition;
#else
            pointerPos = Input.GetTouch(0).position;
#endif
            if (_selectedObject)
            {
                onDeselected?.Invoke(_selectedObject);
                _selectedObject = null;
            }
        }
    }
}
