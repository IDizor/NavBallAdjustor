using System.Collections.Generic;
using UnityEngine;

namespace NavBallAdjustor
{
    /// <summary>
    /// Implements functionality for ColorPicker.
    /// </summary>
    public partial class NavBallAdjustor : MonoBehaviour
    {
        /// <summary>
        /// Indicates whether markers priorities is enabled.
        /// </summary>
        [Persistent]
        private bool NBMarkersPrioritiesEnabled = false;

        /// <summary>
        /// The NavBall markers overlapping priorities.
        /// </summary>
        [Persistent]
        private List<MarkerType> NBMarkersPriorities = new List<MarkerType>() {
            MarkerType.Target,
            MarkerType.Burn,
            MarkerType.Prograde,
            MarkerType.Radial,
            MarkerType.Normal,
            MarkerType.Waypoint,
        };

        /// <summary>
        /// The priorities options window rectangle.
        /// </summary>
        private Rect PrioritiesWindowRect = new Rect(300, 200, 250, 60);

        /// <summary>
        /// Builds priorities options window.
        /// </summary>
        /// <param name="windowID">The window identifier.</param>
        private void PrioritiesOptionsWindow(int windowID)
        {
            GUILayout.BeginVertical();

            this.NBMarkersPrioritiesEnabled = GUILayout.Toggle(this.NBMarkersPrioritiesEnabled, ModStrings.OptionLabel.EnableMarkersPriorities);

            if (NBMarkersPrioritiesEnabled)
            {
                GUI.contentColor = this.DefaultContentColor;
            }
            else
            {
                GUI.contentColor = Color.gray;
            }

            GUILayout.Label(new GUIContent("Click on the button to move it up:"));

            for (int i = 0; i < NBMarkersPriorities.Count; i++)
            {
                GUILayout.BeginHorizontal();

                if (GUILayout.Button(NBMarkersPriorities[i].ToString(), GUILayout.MaxWidth(100f)))
                {
                    if (i > 0)
                    {
                        var item = NBMarkersPriorities[i];
                        NBMarkersPriorities.RemoveAt(i);
                        NBMarkersPriorities.Insert(i - 1, item);
                    }
                }

                if (i == 0)
                {
                    GUILayout.Space(10f);
                    GUILayout.Label(new GUIContent("<- Highest"));
                }
                else if (i == NBMarkersPriorities.Count - 1)
                {
                    GUILayout.Space(10f);
                    GUILayout.Label(new GUIContent("<- Lowest"));
                }
                else
                {
                    GUILayout.FlexibleSpace();
                }

                GUILayout.EndHorizontal();
            }

            GUI.contentColor = this.DefaultContentColor;

            GUILayout.Space(10f);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(ModStrings.Button.Save, GUILayout.MinWidth(100f)))
            {
                this.SaveConfig();
                this.ShowPriorityOptions = false;

                return;
            }
            if (GUILayout.Button(ModStrings.Button.Cancel, GUILayout.MinWidth(100f)))
            {
                this.ShowPriorityOptions = false;
                this.LoadConfig();
                this.ApplyLoadedScales();
                this.ApplyLoadedColors();

                return;
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUI.DragWindow();
        }
    }
}
