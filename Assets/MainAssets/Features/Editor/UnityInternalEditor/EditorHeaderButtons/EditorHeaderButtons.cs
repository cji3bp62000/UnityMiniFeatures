using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityMiniFeatures.Editor
{
    /// <summary>
    /// コンポーネントののエディターのヘッダーにボタンを追加する機能。
    /// このスクリプトを "Assembly-CSharp-Editor-testable" という名の asmdef 以下に格納してください<br/>
    /// Add buttons to component inspector header.
    /// This script should be under asmdef called "Assembly-CSharp-Editor-testable"
    /// </summary>
    public static class EditorHeaderButtons
    {
        private static GUIContent SwitchDebugModeIcon = EditorGUIUtility.IconContent("DebuggerDisabled");
        private static GUIContent ShowPropertiesIcon = EditorGUIUtility.IconContent("Update-Available");

        /// <summary>
        /// デバッグ表示 Show Debug Switch Button
        /// </summary>
        [EditorHeaderItem(typeof(Component), -999)]
        public static bool DrawSwitchDebugModeButton(Rect rectangle, Object[] targets)
        {
            if (!EditorGUI.DropdownButton(rectangle, SwitchDebugModeIcon, FocusType.Passive, EditorStyles.iconButton)) return true;

            // 現在の InspectorWindow を取得
            var currentPropertyEditor = EditorWindow.mouseOverWindow as PropertyEditor;
            if (!currentPropertyEditor) return true;
            currentPropertyEditor.inspectorMode = SwitchInspectorMode(currentPropertyEditor.inspectorMode);
            return true;
        }

        private static InspectorMode SwitchInspectorMode(InspectorMode currentInspectorMode)
        {
            return currentInspectorMode switch {
                InspectorMode.Normal => InspectorMode.Debug,
                InspectorMode.Debug => InspectorMode.DebugInternal,
                InspectorMode.DebugInternal => InspectorMode.Normal,
                _ => InspectorMode.Normal,
            };
        }

        /// <summary>
        /// プロパティボタン表示 Show Properties Button
        /// </summary>
        [EditorHeaderItem(typeof(Component), -998)]
        public static bool DrawShowPropertiesButton(Rect rectangle, Object[] targets)
        {
            if (!EditorGUI.DropdownButton(rectangle, ShowPropertiesIcon, FocusType.Passive, EditorStyles.iconButton)) return true;
            PropertyEditor.OpenPropertyEditor(targets);

            return true;
        }
    }
}
