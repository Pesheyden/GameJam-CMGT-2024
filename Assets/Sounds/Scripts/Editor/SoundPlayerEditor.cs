using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.Serialization;

[CustomEditor(typeof(SoundPlayer))]
public class SoundPlayerEditor : Editor
{
    [FormerlySerializedAs("VisualTree")] [SerializeField] private VisualTreeAsset _visualTree;

    private PropertyField _multiClipsToggleProperty;
    private VisualElement _multiClipsGroup;
    private VisualElement _clipsInputField;

    private PropertyField _randomToggleProperty;
    private VisualElement _randomGroup;

    private PropertyField _startTypeProperty;
    private VisualElement _startDelayInputField;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new();


        _visualTree.CloneTree(root);


        _multiClipsToggleProperty = root.Q<PropertyField>("MultipleClips_BoolToggle");
        _randomToggleProperty = root.Q<PropertyField>("Random_BoolToggle");
        _startTypeProperty = root.Q<PropertyField>("TypeStart_EnumField");

        _multiClipsGroup = root.Q<VisualElement>("MultiClip_Group");
        _randomGroup = root.Q<VisualElement>("Random_Group");
        _clipsInputField = root.Q<VisualElement>("AudioClip_InputField");
        _startDelayInputField = root.Q<VisualElement>("StartDelay_FloatField");


        _multiClipsToggleProperty.RegisterCallback<ChangeEvent<bool>>(OnMultiClipBoolChange);
        _randomToggleProperty.RegisterCallback<ChangeEvent<bool>>(OnRandomToggleBoolChange);
        _startTypeProperty.RegisterCallback<ChangeEvent<string>>(OnStartTypeEnumChange);

        return root;
    }
    private void OnMultiClipBoolChange(ChangeEvent<bool> e)
    {
        if (e.newValue)
        {
            _multiClipsGroup.style.display = DisplayStyle.Flex;
            _clipsInputField.style.display = DisplayStyle.None;
        }
            
        else
        {
            _multiClipsGroup.style.display = DisplayStyle.None;
            _clipsInputField.style.display = DisplayStyle.Flex;
        }
            
    }
    private void OnRandomToggleBoolChange(ChangeEvent<bool> e)
    {
        _randomGroup.style.display = e.newValue ? DisplayStyle.Flex : DisplayStyle.None;
    }
    private void OnStartTypeEnumChange(ChangeEvent<string> e)
    {
        switch (e.newValue)
        {
            case "None":
                _startDelayInputField.style.display = DisplayStyle.None;
                break;
            case "Delay":
                _startDelayInputField.style.display = DisplayStyle.Flex;
                break;
            case "Call":
                _startDelayInputField.style.display = DisplayStyle.None;
                break;
            default:
                break;
        }
    }

}
