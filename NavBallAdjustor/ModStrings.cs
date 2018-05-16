namespace NavBallAdjustor
{
    /// <summary>
    /// Provides string constants for mod.
    /// </summary>
    public static class ModStrings
    {
        /// <summary>
        /// The mod name.
        /// </summary>
        public const string ModName = "NavBallAdjustor";

        /// <summary>
        /// The configuration file name.
        /// </summary>
        public const string ConfigFile = "NavBallAdjustor.cfg";

        /// <summary>
        /// The options window title.
        /// </summary>
        public const string OptionsWindowTitle = "NavBall Options";

        /// <summary>
        /// The color options window title.
        /// </summary>
        public const string ColorOptionsWindowTitle = "NavBall Markers Colors";

        /// <summary>
        /// The priorities options window title.
        /// </summary>
        public const string PrioritiesOptionsWindowTitle = "NavBall Markers Overlapping Priorities";

        /// <summary>
        /// String constants for mod options labels.
        /// </summary>
        public static class OptionLabel
        {
            /// <summary>
            /// The prograde label.
            /// </summary>
            public const string Prograde = "Prograde";

            /// <summary>
            /// The retrograde label.
            /// </summary>
            public const string Retrograde = "Retrograde";

            /// <summary>
            /// The radial-in label.
            /// </summary>
            public const string RadialIn = "Radial In";

            /// <summary>
            /// The radial-out label.
            /// </summary>
            public const string RadialOut = "Radial Out";

            /// <summary>
            /// The normal label.
            /// </summary>
            public const string Normal = "Normal";

            /// <summary>
            /// The anti-normal label.
            /// </summary>
            public const string AntiNormal = "AntiNormal";

            /// <summary>
            /// The target label.
            /// </summary>
            public const string Target = "Target";

            /// <summary>
            /// The anti-target label.
            /// </summary>
            public const string AntiTarget = "AntiTarget";

            /// <summary>
            /// The markers scale section label.
            /// </summary>
            public const string MarkersScaleSection = "Markers Scale";

            /// <summary>
            /// The cursor label.
            /// </summary>
            public const string Cursor = "Direction Cursor";

            /// <summary>
            /// The prograde & retrograde label.
            /// </summary>
            public const string ProgradeRetrograde = "Prograde & Retrograde";

            /// <summary>
            /// The radial-in & radial-out label.
            /// </summary>
            public const string RadialInOut = "Radial In & Out";

            /// <summary>
            /// The normal & anti-normal label.
            /// </summary>
            public const string NormalAntiNormal = "Normal & AntiNormal";

            /// <summary>
            /// The maneuver burn vector label.
            /// </summary>
            public const string Burn = "Maneuver Vector";

            /// <summary>
            /// The target & anti-target label.
            /// </summary>
            public const string TargetAntiTarget = "Target & AntiTarget";

            /// <summary>
            /// The survey navigation waypoint label.
            /// </summary>
            public const string NavWaypoint = "Survey Waypoint";

            /// <summary>
            /// The ghost markers label.
            /// </summary>
            public const string GhostMarkersSection = "Ghost Markers";

            /// <summary>
            /// The ghost markers toggle label.
            /// </summary>
            public const string EnableGhostMarkers = "Enable Ghost Markers";

            /// <summary>
            /// The ghost markers opacity label.
            /// </summary>
            public const string GhostMarkersOpacity = "Ghost Markers Opacity";

            /// <summary>
            /// The ghost markers scale label.
            /// </summary>
            public const string GhostMarkersScale = "Ghost Markers Scale";

            /// <summary>
            /// The miscellaneous section label.
            /// </summary>
            public const string MiscellaneousSection = "Miscellaneous";

            /// <summary>
            /// The hide NavBall on map label.
            /// </summary>
            public const string HideOnMap = "Stock AutoHide NavBall on enter Map View";

            /// <summary>
            /// The always hide NavBall on map label.
            /// </summary>
            public const string AlwaysHideOnMap = "Always hide NavBall on enter Map View";

            /// <summary>
            /// The afraid mouse cursor on map label.
            /// </summary>
            public const string AfraidMouseOnMap = "NavBall avoids mouse on Map View";

            /// <summary>
            /// The afraid mouse cursor on flight label.
            /// </summary>
            public const string AfraidMouseOnFlight = "NavBall avoids mouse on Flight View";

            /// <summary>
            /// The markers priorities toggle label.
            /// </summary>
            public const string EnableMarkersPriorities = "Enable Priorities";
        }

        /// <summary>
        /// String constants for mod buttons.
        /// </summary>
        public static class Button
        {
            /// <summary>
            /// The reset button.
            /// </summary>
            public const string Reset = "Reset";

            /// <summary>
            /// The save button.
            /// </summary>
            public const string Save = "Save";

            /// <summary>
            /// The cancel button.
            /// </summary>
            public const string Cancel = "Cancel";

            /// <summary>
            /// The close button.
            /// </summary>
            public const string Close = "Close";

            /// <summary>
            /// The OK button.
            /// </summary>
            public const string OK = "OK";

            /// <summary>
            /// The edit colors button.
            /// </summary>
            public const string EditColors = "Edit Markers Colors";

            /// <summary>
            /// The edit colors button.
            /// </summary>
            public const string EditPriorities = "Edit Markers Overlapping Priorities";

            /// <summary>
            /// The default button.
            /// </summary>
            public const string Default = "Default";

            /// <summary>
            /// The default 1 button.
            /// </summary>
            public const string Default1 = "Default 1";

            /// <summary>
            /// The default 2 button.
            /// </summary>
            public const string Default2 = "Default 2";
        }

        /// <summary>
        /// String constants for options toggle.
        /// </summary>
        public static class OptionsToggle
        {
            /// <summary>
            /// The icon "off" path.
            /// </summary>
            public const string IconOffPath = "NavBallAdjustor/Icons/Off";

            /// <summary>
            /// The icon "on" path.
            /// </summary>
            public const string IconOnPath = "NavBallAdjustor/Icons/On";

            /// <summary>
            /// The icon "push" path.
            /// </summary>
            public const string IconPushPath = "NavBallAdjustor/Icons/Push";
        }

        /// <summary>
        /// String constants for NavBall transforms names.
        /// </summary>
        public static class NavBallTransform
        {
            /// <summary>
            /// The NavBall transform name.
            /// </summary>
            public const string NavBall = "IVAEVACollapseGroup";

            /// <summary>
            /// The cursor transform name.
            /// </summary>
            public const string Cursor = "NavBallCursor";

            /// <summary>
            /// The vectors transform name.
            /// </summary>
            public const string Vectors = "NavBallVectorsPivot";

            /// <summary>
            /// The prograde vector transform name.
            /// </summary>
            public const string Prograde = "ProgradeVector";

            /// <summary>
            /// The retrograde vector transform name.
            /// </summary>
            public const string Retrograde = "RetrogradeVector";

            /// <summary>
            /// The radial-in vector transform name.
            /// </summary>
            public const string RadialIn = "RadialInVector";

            /// <summary>
            /// The radial-out vector transform name.
            /// </summary>
            public const string RadialOut = "RadialOutVector";

            /// <summary>
            /// The normal vector transform name.
            /// </summary>
            public const string Normal = "NormalVector";

            /// <summary>
            /// The antinormal vector transform name.
            /// </summary>
            public const string AntiNormal = "AntiNormalVector";

            /// <summary>
            /// The burn vector transform name.
            /// </summary>
            public const string Burn = "BurnVector";

            /// <summary>
            /// The burn vector arrow transform name.
            /// </summary>
            public const string BurnArrow = "BurnVectorArrow";

            /// <summary>
            /// The target vector transform name.
            /// </summary>
            public const string Target = "ProgradeWaypoint";

            /// <summary>
            /// The antitarget vector transform name.
            /// </summary>
            public const string AntiTarget = "RetrogradeWaypoint";

            /// <summary>
            /// The survey navigation waypoint transform name.
            /// </summary>
            public const string NavWaypoint = "NavWaypointVisual";

            /// <summary>
            /// Maneuver delta v tarnsform path.
            /// </summary>
            public const string DeltaVReadOut = "IVAEVACollapseGroup/DeltaVGauge/dVGauge/deltaVreadout";
        }
    }
}
