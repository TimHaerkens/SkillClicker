using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Devdog.InventorySystem.Editors
{
    [CustomEditor(typeof(CraftingWindowLayoutUI), true)]
    public class CraftingWindowLayoutUIEditor : InventoryEditorBase
    {
        //private CraftingStation item;
        private SerializedProperty _startCraftingCategoryID;

        public override void OnEnable()
        {
            base.OnEnable();

            _startCraftingCategoryID = serializedObject.FindProperty("_startCraftingCategoryID");
        }

        protected override void OnCustomInspectorGUI(params CustomOverrideProperty[] extraSpecific)
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(script);

            GUILayout.Label("Behavior", InventoryEditorStyles.titleStyle);
            _startCraftingCategoryID.intValue = EditorGUILayout.Popup("Crafting category", _startCraftingCategoryID.intValue, ItemManager.database.craftingCategoriesStrings);

            DrawPropertiesExcluding(serializedObject, new string[]
            {
                "m_Script",
                "_startCraftingCategoryID",
            });

            serializedObject.ApplyModifiedProperties();
        }
    }
}