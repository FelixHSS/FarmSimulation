using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    private Animator[] Animators { get; set; }
    [field: SerializeField] public SpriteRenderer HeldItem { get; set; }
    [field: Header("List of CharacterPart Animator")]
    [field: SerializeField] public List<AnimatorType> AnimatorsTypes { get; set; }
    private Dictionary<string, Animator> AnimatorNameDict { get; set; } = new Dictionary<string, Animator>();
    private void Awake()
    {
        Animators = GetComponentsInChildren<Animator>();

        foreach (var animator in Animators)
        {
            // names are Body, Hair, Arm same as values of CharacterPart
            AnimatorNameDict[animator.name] = animator;
        }
    }

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
    }

    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        ActionType currentType = itemDetails.ItemType switch
        {
            ItemType.Seed => ActionType.Carry,
            ItemType.Commodity => ActionType.Carry,
            _ => ActionType.None
        };

        if (!isSelected)
        {
            currentType = ActionType.None;
            HeldItem.enabled = false;
        }
        else
        {
            if (currentType == ActionType.Carry)
            {
                HeldItem.sprite = itemDetails.ItemOnWorldSprite;
                HeldItem.enabled = true;
            }
        }
        SwitchAnimator(currentType);
    }

    private void SwitchAnimator(ActionType actionType)
    {
        foreach (var animatorType in AnimatorsTypes)
        {
            if(animatorType.ActionType == actionType)
            {
                AnimatorNameDict[animatorType.CharacterPart.ToString()].runtimeAnimatorController = animatorType.OverrideController;
            }
        }
    }
}
