using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementComponent : MonoBehaviour
{
    [SerializeField]
    private Image[] _elementalImages;
    [SerializeField]
    private Text _text;

    public void UpdateElements(ElementalType targetElements)
    {
        _text.gameObject.SetActive(false);
        bool isAny = false;

        ElementalType type = targetElements;

        ElementalType[] array = (ElementalType[])Enum.GetValues(typeof(ElementalType));

        for (int i = 0; i < array.Length; i++)
        {
            bool result = HasFlag(array[i], type);
            _elementalImages[i].gameObject.SetActive(result);

            if(result)
                isAny = true;

            Debug.Log("HAS FLAG: " + result + " typeof: " + array[i]);
        }

        if (!isAny)
        {
            _text.gameObject.SetActive(true);
        }
    }

    private bool HasFlag(ElementalType type, ElementalType targetType)
    {
        return (type & targetType) == type;
    }
}
