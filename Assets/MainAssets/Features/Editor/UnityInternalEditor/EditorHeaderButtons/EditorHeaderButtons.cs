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
            // dont draw anything if is not PropertyEditor
            if (GUIView.current is not HostView { actualView: PropertyEditor propertyEditor }) return false;

            // draw icon
            var defaultGUIColor = GUI.color;
            GUI.color = InspectorModeColorDic[(int)propertyEditor.inspectorMode];
            var clicked = EditorGUI.DropdownButton(rectangle, DebugIcon, FocusType.Passive, EditorStyles.iconButton);
            GUI.color = defaultGUIColor;

            // return if icon not clicked
            if (!clicked) {
                return true;
            }

            // switch InspectorMode
            propertyEditor.inspectorMode = SwitchInspectorMode(propertyEditor.inspectorMode);
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

        private static Dictionary<int, Color> InspectorModeColorDic = new()
        {
            [(int)InspectorMode.Normal] = Color.white,
            [(int)InspectorMode.Debug] = new Color(1f, 0.8f, 0f),
            [(int)InspectorMode.DebugInternal] = new Color(0.918f, 0f, 0f),
        };

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
