using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollUI : MonoBehaviour
{
    private ScrollRect _scrollRect;
    private ContentSizeFitter _contentSizeFitter;
    private HorizontalLayoutGroup _horizontalLayoutGroup;
    private GridLayoutGroup _gridLayoutGroup;
    private bool _isHorizontal;
    private float _disableMarginX;
    private bool _hasDisabledGridComponents;
        
    private List<RectTransform> items = new List<RectTransform>();
    private Vector2 _newAnchoredPosition = Vector2.zero;
        
    private float _threshold = 100f;
    private int _itemCount;
    private float _recordOffsetX;

    private void Awake()
    { 
        Init();
    }

    public void SetNewItems(ref List<Transform> newItems)
    {
        if (_scrollRect != null)
        {
            if (_scrollRect.content == null && newItems == null) return;
            
            if (items != null)
            { 
                items.Clear();
            }

            for (int i = _scrollRect.content.childCount - 1; i >= 0; i--)
            { 
                Transform child = _scrollRect.content.GetChild(i); 
                child.SetParent(null);
                DestroyImmediate(child.gameObject);
            }
            
            foreach (Transform newItem in newItems)
            { 
                newItem.SetParent(_scrollRect.content);
            }
            
            SetItems();
        }
    }

    private void SetItems()
    {
        for (int i = 0; i < _scrollRect.content.childCount; i++)
        { 
            items.Add(_scrollRect.content.GetChild(i).GetComponent<RectTransform>());
        }
        
        _itemCount = _scrollRect.content.childCount;
    }

    private void Init()
    {
        if (GetComponent<ScrollRect>() != null)
        {
            _scrollRect = GetComponent<ScrollRect>();
            _scrollRect.onValueChanged.AddListener(OnScroll);
                
            if (_scrollRect.content.GetComponent<HorizontalLayoutGroup>() != null)
            { 
                _horizontalLayoutGroup = _scrollRect.content.GetComponent<HorizontalLayoutGroup>();
            }
            if (_scrollRect.content.GetComponent<GridLayoutGroup>() != null)
            {
                _gridLayoutGroup = _scrollRect.content.GetComponent<GridLayoutGroup>();
            }
            if (_scrollRect.content.GetComponent<ContentSizeFitter>() != null)
            {
                _contentSizeFitter = _scrollRect.content.GetComponent<ContentSizeFitter>(); 
            }
            _isHorizontal = _scrollRect.horizontal;
           
            SetItems();
        }
    }

    private void DisableGridComponents()
    {
        if (_isHorizontal)
        { 
            _recordOffsetX = items[1].GetComponent<RectTransform>().anchoredPosition.x - items[0].GetComponent<RectTransform>().anchoredPosition.x;
            if (_recordOffsetX < 0)
            { 
                _recordOffsetX *= -1;
            }
            
            _disableMarginX = _recordOffsetX * _itemCount / 2;
        }
        if (_horizontalLayoutGroup)
        { 
            _horizontalLayoutGroup.enabled = false;
        }
        if (_contentSizeFitter)
        { 
            _contentSizeFitter.enabled = false;
        }
        if (_gridLayoutGroup)
        {
            _gridLayoutGroup.enabled = false;
        }
        _hasDisabledGridComponents = true;
    }

    private void OnScroll(Vector2 pos)
    {
        if (!_hasDisabledGridComponents) 
            DisableGridComponents();

        for (int i = 0; i < items.Count; i++)
        {
            if (_isHorizontal)
            {
                if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).x > _disableMarginX + _threshold)
                { 
                    _newAnchoredPosition = items[i].anchoredPosition; 
                    _newAnchoredPosition.x -= _itemCount * _recordOffsetX;
                    items[i].anchoredPosition = _newAnchoredPosition;
                    _scrollRect.content.GetChild(_itemCount - 1).transform.SetAsFirstSibling();
                }
                else if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).x < -_disableMarginX)
                { 
                    _newAnchoredPosition = items[i].anchoredPosition;
                    _newAnchoredPosition.x += _itemCount * _recordOffsetX;
                    items[i].anchoredPosition = _newAnchoredPosition;
                    _scrollRect.content.GetChild(0).transform.SetAsLastSibling();
                }
            }
        }
    }
}
