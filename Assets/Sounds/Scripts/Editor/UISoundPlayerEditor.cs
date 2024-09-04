using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(UISoundManager))]
public class UISoundPlayerEditor : Editor
{
        [SerializeField] private VisualTreeAsset _visualTree;

        private UISoundManager _uiSoundManager;

        private Button _enumRefreshButton;
        


        private void OnEnable()
        {
                _uiSoundManager = (UISoundManager)target;
        }

        public override VisualElement CreateInspectorGUI()
        {
                VisualElement root = new();

                _visualTree.CloneTree(root);

                _enumRefreshButton = root.Q<Button>("EnumRefresh_Button");
                _enumRefreshButton.clicked += _uiSoundManager.RefreshEnums;
                
                return root;
        }

        
}

