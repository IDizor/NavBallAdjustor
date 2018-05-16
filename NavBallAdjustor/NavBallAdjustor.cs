using System;
using KSP.IO;
using KSP.UI;
using KSP.UI.Screens.Flight;
using UnityEngine;

namespace NavBallAdjustor
{
    /// <summary>
    /// Implements functionality for NavBallAdjustor KSP mod.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public partial class NavBallAdjustor : MonoBehaviour
    {
        #region Private_Fields
        /// <summary>
        /// The toggle left offset.
        /// </summary>
        private float ToggleLeftOffset = -57f;

        /// <summary>
        /// The NavBall cursor transform anchor offset X.
        /// </summary>
        private float CursorOffsetX = 2.654514f;

        /// <summary>
        /// The NavBall cursor transform anchor offset Y.
        /// </summary>
        private float CursorOffsetY = 9.725695f;

        /// <summary>
        /// The NavBall cursor scale.
        /// </summary>
        [Persistent]
        private float NBCursorScale = 1f;

        /// <summary>
        /// The NavBall prograde vectors scale.
        /// </summary>
        [Persistent]
        private float NBProgradeScale = 1f;

        /// <summary>
        /// The NavBall radial vectors scale.
        /// </summary>
        [Persistent]
        private float NBRadialScale = 1f;

        /// <summary>
        /// The NavBall normal vectors scale.
        /// </summary>
        [Persistent]
        private float NBNormalScale = 1f;

        /// <summary>
        /// The NavBall burn vector scale.
        /// </summary>
        [Persistent]
        private float NBBurnScale = 1f;

        /// <summary>
        /// The NavBall target vectors scale.
        /// </summary>
        [Persistent]
        private float NBTargetScale = 1f;

        /// <summary>
        /// The NavBall survey navigation vector scale.
        /// </summary>
        [Persistent]
        private float NBNavWaypointScale = 1f;

        /// <summary>
        /// Stock AutoHide NavBall on enter Map View.
        /// </summary>
        [Persistent]
        private bool NBHideOnMap = true;

        /// <summary>
        /// Always hide NavBall on enter Map View.
        /// </summary>
        [Persistent]
        private bool NBAlwaysHideOnMap = false;

        /// <summary>
        /// The NavBall afraids mouse cursor on Map View.
        /// </summary>
        [Persistent]
        private bool NBAfraidMouseOnMapView = false;

        /// <summary>
        /// The NavBall afraids mouse cursor on Flight View.
        /// </summary>
        [Persistent]
        private bool NBAfraidMouseOnFlightView = false;

        /// <summary>
        /// The NavBall ghost markers feature.
        /// </summary>
        [Persistent]
        private bool NBGhostMarkersEnabled = false;

        /// <summary>
        /// The NavBall ghost markers opacity.
        /// </summary>
        [Persistent]
        private float NBGhostMarkersOpacity = 0.3f;

        /// <summary>
        /// The NavBall ghost markers scale.
        /// </summary>
        [Persistent]
        private float NBGhostMarkersScale = 0.5f;

        /// <summary>
        /// The mod hash code.
        /// </summary>
        private int HashCode;

        /// <summary>
        /// The NavBall helper.
        /// </summary>
        private NavBallHelper NBall;

        /// <summary>
        /// The NavBall cursor.
        /// </summary>
        private Transform NavBallCursor = null;

        /// <summary>
        /// The NavBall prograde marker.
        /// </summary>
        private Transform NavBallPrograde = null;

        /// <summary>
        /// The NavBall retrograde marker.
        /// </summary>
        private Transform NavBallRetrograde = null;

        /// <summary>
        /// The NavBall radial-in marker.
        /// </summary>
        private Transform NavBallRadialIn = null;

        /// <summary>
        /// The NavBall radial-out marker.
        /// </summary>
        private Transform NavBallRadialOut = null;

        /// <summary>
        /// The NavBall normal marker.
        /// </summary>
        private Transform NavBallNormal = null;

        /// <summary>
        /// The NavBall antinormal marker.
        /// </summary>
        private Transform NavBallAntiNormal = null;

        /// <summary>
        /// The NavBall burn marker.
        /// </summary>
        private Transform NavBallBurn = null;

        /// <summary>
        /// The NavBall burn arrow marker.
        /// </summary>
        private Transform NavBallBurnArrow = null;

        /// <summary>
        /// The NavBall target marker.
        /// </summary>
        private Transform NavBallTarget = null;

        /// <summary>
        /// The NavBall antitarget marker.
        /// </summary>
        private Transform NavBallAntiTarget = null;

        /// <summary>
        /// The NavBall survey navigation waypoint marker.
        /// </summary>
        private Transform NavBallNavWaypoint = null;

        /// <summary>
        /// The NavBall cursor initial scale.
        /// </summary>
        private Vector3 NBCursorInitialScale = new Vector3();

        /// <summary>
        /// The NavBall vectors initial scale.
        /// </summary>
        private Vector3 NBVectorsInitialScale = new Vector3();

        /// <summary>
        /// The NavBall cursor initial position.
        /// </summary>
        private Vector3 NBCursorInitialPosition = new Vector3();

        /// <summary>
        /// The options window display state.
        /// </summary>
        private bool ShowOptions = false;

        /// <summary>
        /// The colors options window display state.
        /// </summary>
        private bool ShowColorOptions = false;

        /// <summary>
        /// The markers priorities options window display state.
        /// </summary>
        private bool ShowPriorityOptions = false;

        /// <summary>
        /// The mod options window rectangle.
        /// </summary>
        private Rect OptionsWindowRect = new Rect(300, 200, 558, 300);

        /// <summary>
        /// The GUI toggle details.
        /// </summary>
        private OptionsToggle Toggle;

        /// <summary>
        /// The previous NavBall position X.
        /// </summary>
        private float PrevNavBallPosX = 0f;

        /// <summary>
        /// The previous NavBall position Y.
        /// </summary>
        private float PrevNavBallPosY = 0f;

        /// <summary>
        /// Customized horizontal slider style.
        /// </summary>
        private GUIStyle SliderStyle;

        /// <summary>
        /// The default content color.
        /// </summary>
        private Color DefaultContentColor;

        /// <summary>
        /// Indicates whether the NavBall is scared now.
        /// </summary>
        private bool IsNavBallScared = false;

        /// <summary>
        /// Indicates whether the nav waypoint color is updated.
        /// </summary>
        private bool IsNavWaypointColorUpdated = false;
        #endregion

        #region Private_Properties
        /// <summary>
        /// Gets a value indicating whether current build mode is debug.
        /// </summary>
        private static bool IsDebug
        {
            get
            {
                #if DEBUG
                return true;
                #endif
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the NavBall cursor scale.
        /// </summary>
        private float NavBallCursorScale
        {
            get
            {
                return this.NBCursorScale;
            }
            set
            {
                this.NBCursorScale = value;

                if (this.NavBallCursor != null)
                {
                    this.ScaleNavBallCursor(this.NBCursorScale);
                }
            }
        }

        /// <summary>
        /// Gets or sets the NavBall prograde vectors scale.
        /// </summary>
        private float NavBallProgradeScale
        {
            get
            {
                return this.NBProgradeScale;
            }
            set
            {
                this.NBProgradeScale = value;

                if (this.NavBallPrograde != null)
                {
                    this.NavBallPrograde.localScale = this.NavBallRetrograde.localScale = this.NBVectorsInitialScale * value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the NavBall radial vectors scale.
        /// </summary>
        private float NavBallRadialScale
        {
            get
            {
                return this.NBRadialScale;
            }
            set
            {
                this.NBRadialScale = value;

                if (this.NavBallRadialIn != null)
                {
                    this.NavBallRadialIn.localScale = this.NavBallRadialOut.localScale = this.NBVectorsInitialScale * value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the NavBall normal vectors scale.
        /// </summary>
        private float NavBallNormalScale
        {
            get
            {
                return this.NBNormalScale;
            }
            set
            {
                this.NBNormalScale = value;

                if (this.NavBallNormal != null)
                {
                    this.NavBallNormal.localScale = this.NavBallAntiNormal.localScale = this.NBVectorsInitialScale * value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the NavBall burn vector scale.
        /// </summary>
        private float NavBallBurnScale
        {
            get
            {
                return this.NBBurnScale;
            }
            set
            {
                this.NBBurnScale = value;

                if (this.NavBallBurn != null)
                {
                    this.NavBallBurn.localScale = this.NBVectorsInitialScale * value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the NavBall target vectors scale.
        /// </summary>
        private float NavBallTargetScale
        {
            get
            {
                return this.NBTargetScale;
            }
            set
            {
                this.NBTargetScale = value;

                if (this.NavBallTarget != null)
                {
                    this.NavBallTarget.localScale = this.NavBallAntiTarget.localScale = this.NBVectorsInitialScale * value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the NavBall survey navigation vector scale.
        /// </summary>
        private float NavBallNavWaypointScale
        {
            get
            {
                return this.NBNavWaypointScale;
            }
            set
            {
                this.NBNavWaypointScale = value;

                if (this.NavBallNavWaypoint != null)
                {
                    this.NavBallNavWaypoint.localScale = this.NBVectorsInitialScale * value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether active vessel has maneuver nodes with some delta v.
        /// </summary>
        private bool HasNonZeroManeuverNode
        {
            get
            {
                return this.HasManeuverNode
                    && FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes[0].DeltaV != Vector3d.zero;
            }
        }

        /// <summary>
        /// Gets a value indicating whether active vessel has maneuver node.
        /// </summary>
        private bool HasManeuverNode
        {
            get
            {
                return FlightGlobals.ActiveVessel != null
                    && FlightGlobals.ActiveVessel.patchedConicSolver != null
                    && FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes.Count > 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether active vessel has navigation waypoint.
        /// </summary>
        private bool HasNavWaypoint
        {
            get
            {
                return HighLogic.LoadedSceneIsFlight
                    && FlightGlobals.ActiveVessel?.navigationWaypoint != null;
            }
        }
        #endregion

        #region Public_Methods
        /// <summary>
        /// Called on flight begin.
        /// </summary>
        public void Start()
        {
            this.HashCode = ModStrings.ModName.GetHashCode();
            this.ColorPickerTexture = LinuxGuruGamer.GetTexture("NavBallAdjustor/Icons/ColorPicker");

            LoadConfig();

            GameEvents.onLevelWasLoaded.Add(OnLevelLoaded);
            MapView.OnEnterMapView += OnEnterMap;
        }

        /// <summary>
        /// Called on instance destroy.
        /// </summary>
        public void OnDestroy()
        {
            GameEvents.onLevelWasLoaded.Remove(OnLevelLoaded);
            MapView.OnEnterMapView -= OnEnterMap;
        }

        /// <summary>
        /// Update is called once per frame.
        /// It is the main workhorse function for frame updates.
        /// </summary>
        public void Update()
        {
            if (this.NBall == null || this.NBall.NavBall == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.ShowOptions = false;
                this.ShowColorOptions = false;
                this.ShowPriorityOptions = false;
            }

            // Afraid mouse cursor.
            if (!FlightDriver.Pause && !GameSettings.MODIFIER_KEY.GetKey())
            {
                if ((this.NBAfraidMouseOnMapView && MapView.MapIsEnabled) ||
                    (this.NBAfraidMouseOnFlightView && !MapView.MapIsEnabled))
                {
                    if (this.IsPixelOnNavBall(Mouse.screenPos))
                    {
                        if (NavBallToggle.Instance.panel.expanded)
                        {
                            this.IsNavBallScared = true;
                            NavBallToggle.Instance.panel.Collapse();
                        }
                    }
                    else if (NavBallToggle.Instance.panel.collapsed && this.IsNavBallScared)
                    {
                        this.IsNavBallScared = false;
                        NavBallToggle.Instance.panel.Expand();
                    }
                }
                else if (this.IsNavBallScared && NavBallToggle.Instance.panel.collapsed)
                {
                    this.IsNavBallScared = false;
                    NavBallToggle.Instance.panel.Expand();
                }
            }

            // Update nav waypoint color.
            bool navWaypointIsActive = HasNavWaypoint;

            if (navWaypointIsActive != this.IsNavWaypointColorUpdated)
            {
                this.IsNavWaypointColorUpdated = navWaypointIsActive;

                if (this.IsNavWaypointColorUpdated)
                {
                    this.PopulateNavWaypointMarker();

                    // Apply color
                    this.NavWaypointMaterial.SetColor(PropertyIDs._TintColor, this.ShowColorOptions && this.NavWaypointSelected
                        ? this.NewColor
                        : this.NavWaypointColor);

                    // Apply scale
                    this.NavBallNavWaypointScale = this.NBNavWaypointScale;
                }
            }
        }

        /// <summary>
        /// Called multiple times per frame in response to GUI events.
        /// The Layout and Repaint events are processed first,followed
        /// by a Layout and keyboard/mouse event for each input event.
        /// </summary>
        public void OnGUI()
        {
            if (this.Toggle == null || !this.IsCameraTurnedOn() ||
                FlightUIModeController.Instance.navBall.panelTransform == null)
            {
                return;
            }

            // Check if NavBall position changed.
            if (this.PrevNavBallPosX != this.NBall.Position.x
                || this.PrevNavBallPosY != this.NBall.Position.y)
            {
                this.PrevNavBallPosX = this.NBall.Position.x;
                this.PrevNavBallPosY = this.NBall.Position.y;

                // Reposition toggle button.
                this.Toggle.Width = OptionsToggle.DefaultWidth * this.NBall.Scale.x * GameSettings.UI_SCALE;
                this.Toggle.Height = OptionsToggle.DefaultHeight * this.NBall.Scale.y * GameSettings.UI_SCALE;
                this.Toggle.Left = this.NBall.TransformScreenCenterX + (ToggleLeftOffset * this.NBall.Scale.x * GameSettings.UI_SCALE);
                this.Toggle.Top = this.NBall.TextureTopScreenY - this.Toggle.Height;

                if (this.Toggle.Top > GameSettings.SCREEN_RESOLUTION_HEIGHT - 50f)
                {
                    this.Toggle.Top = GameSettings.SCREEN_RESOLUTION_HEIGHT + 50f;
                }
            }

            if (this.SliderStyle == null)
            {
                this.DefaultContentColor = GUI.contentColor;
                this.SliderStyle = GUI.skin.horizontalSlider;
                this.SliderStyle.margin = new RectOffset(0, 0, 9, 0); // add top margin for better look
                this.ColorOptionsLabelsOffsetStyle = new GUIStyle
                {
                    margin = new RectOffset((int)(this.ColorPickerRect.xMin + this.ColorPickerRect.width + 10), 0, 0, 0)
                };
            }

            bool toggleResult = !FlightDriver.Pause && GUI.Toggle(
                this.Toggle.Rectangle,
                this.ShowOptions || this.ShowColorOptions || this.ShowPriorityOptions,
                GUIContent.none,
                this.Toggle.Style);

            this.ShowOptions = toggleResult && !this.ShowColorOptions && !this.ShowPriorityOptions;
            this.ShowColorOptions = toggleResult && !this.ShowOptions && !this.ShowPriorityOptions;
            this.ShowPriorityOptions = toggleResult && !this.ShowOptions && !this.ShowColorOptions;

            if (CameraManager.Instance.currentCameraMode != CameraManager.CameraMode.IVA)
            {
                if (this.ShowOptions)
                {
                    // Display mod options window.
                    this.OptionsWindowRect = GUILayout.Window(
                        this.HashCode, this.OptionsWindowRect, OptionsWindow, ModStrings.OptionsWindowTitle);
                }
                else if (this.ShowColorOptions)
                {
                    // Display mod colors options window.
                    this.ColorsWindowRect = GUILayout.Window(
                        this.HashCode + 1, this.ColorsWindowRect, ColorOptionsWindow, ModStrings.ColorOptionsWindowTitle);
                }
                else if (this.ShowPriorityOptions)
                {
                    // Display mod colors options window.
                    this.PrioritiesWindowRect = GUILayout.Window(
                        this.HashCode + 2, this.PrioritiesWindowRect, PrioritiesOptionsWindow, ModStrings.PrioritiesOptionsWindowTitle);
                }
            }
        }

        /// <summary>
        /// Called once per frame, after Update has finished.
        /// </summary>
        public void LateUpdate()
        {
            // Ghost Markers
            if (this.NBGhostMarkersEnabled && FlightGlobals.ActiveVessel != null)
            {
                this.UpdateGhostMarkers();
            }

            // Markers Priorities
            if (this.NBMarkersPrioritiesEnabled)
            {
                var maxIndex = this.NBMarkersPriorities.Count - 1;

                for (var i = 0; i < this.NBMarkersPriorities.Count; i++)
                {
                    float increment = (maxIndex - i) * -10f;

                    switch (this.NBMarkersPriorities[i])
                    {
                        case MarkerType.Prograde:
                            if (NavBallPrograde != null)
                            {
                                NavBallPrograde.localPosition = NavBallPrograde.localPosition.z > 0
                                    ? NavBallPrograde.localPosition.AddZ(increment)
                                    : NavBallPrograde.localPosition.SetZ(113f);
                                NavBallRetrograde.localPosition = NavBallRetrograde.localPosition.z > 0
                                    ? NavBallRetrograde.localPosition.AddZ(increment)
                                    : NavBallRetrograde.localPosition.SetZ(113f);
                            }
                            break;
                        case MarkerType.Radial:
                            if (NavBallRadialIn != null)
                            {
                                NavBallRadialIn.localPosition = NavBallRadialIn.localPosition.z > 0
                                    ? NavBallRadialIn.localPosition.AddZ(increment)
                                    : NavBallRadialIn.localPosition.SetZ(113f);
                                NavBallRadialOut.localPosition = NavBallRadialOut.localPosition.z > 0
                                    ? NavBallRadialOut.localPosition.AddZ(increment)
                                    : NavBallRadialOut.localPosition.SetZ(113f);
                            }
                            break;
                        case MarkerType.Normal:
                            if (NavBallNormal != null)
                            {
                                NavBallNormal.localPosition = NavBallNormal.localPosition.z > 0
                                    ? NavBallNormal.localPosition.AddZ(increment)
                                    : NavBallNormal.localPosition.SetZ(113f);
                                NavBallAntiNormal.localPosition = NavBallAntiNormal.localPosition.z > 0
                                    ? NavBallAntiNormal.localPosition.AddZ(increment)
                                    : NavBallAntiNormal.localPosition.SetZ(113f);
                            }
                            break;
                        case MarkerType.Target:
                            if (NavBallTarget != null)
                            {
                                NavBallTarget.localPosition = NavBallTarget.localPosition.z > 0
                                    ? NavBallTarget.localPosition.AddZ(increment)
                                    : NavBallTarget.localPosition.SetZ(113f);
                                NavBallAntiTarget.localPosition = NavBallAntiTarget.localPosition.z > 0
                                    ? NavBallAntiTarget.localPosition.AddZ(increment)
                                    : NavBallAntiTarget.localPosition.SetZ(113f);
                            }
                            break;
                        case MarkerType.Burn:
                            if (NavBallBurn != null)
                            {
                                NavBallBurn.localPosition = NavBallBurn.localPosition.z > 0
                                    ? NavBallBurn.localPosition.AddZ(increment)
                                    : NavBallBurn.localPosition.SetZ(113f);
                            }
                            break;
                        case MarkerType.Waypoint:
                            if (NavBallNavWaypoint != null)
                            {
                                NavBallNavWaypoint.localPosition = NavBallNavWaypoint.localPosition.z > 0
                                    ? NavBallNavWaypoint.localPosition.AddZ(increment)
                                    : NavBallNavWaypoint.localPosition.SetZ(113f);
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region Private_Methods
        /// <summary>
        /// Called when enter map.
        /// </summary>
        private void OnEnterMap()
        {
            if (this.NBAlwaysHideOnMap)
            {
                if (NavBallToggle.Instance.panel.expanded)
                {
                    NavBallToggle.Instance.panel.Collapse();
                }
            }
            else if (!this.NBHideOnMap && !NavBallToggle.Instance.ManeuverModeActive)
            {
                NavBallToggle.Instance.overrideButton.onClick.Invoke();
            }
        }

        /// <summary>
        /// Loads the mod configuration.
        /// </summary>
        public void LoadConfig()
        {
            if (File.Exists<NavBallAdjustor>(ModStrings.ConfigFile))
            {
                ConfigNode config = ConfigNode.Load(IOUtils.GetFilePathFor(this.GetType(), ModStrings.ConfigFile));
                ConfigNode.LoadObjectFromConfig(this, config);
            }
        }

        /// <summary>
        /// Saves the mod configuration.
        /// </summary>
        private void SaveConfig()
        {
            ConfigNode node = new ConfigNode(ModStrings.ModName);
            ConfigNode.CreateConfigFromObject(this, node);
            node.Save(IOUtils.GetFilePathFor(this.GetType(), ModStrings.ConfigFile));
        }

        /// <summary>
        /// Called when level loaded.
        /// </summary>
        /// <param name="data">The data.</param>
        private void OnLevelLoaded(GameScenes data)
        {
            if (HighLogic.LoadedScene != GameScenes.FLIGHT)
                return;

            this.NBall = new NavBallHelper();
            this.PopulateNavBallMarkers();
            
            if (this.NavBallCursor != null)
            {
                this.NBCursorInitialScale = this.NavBallCursor.localScale;
                this.NBCursorInitialPosition = this.NavBallCursor.localPosition;
                this.NBVectorsInitialScale = this.NavBallPrograde.localScale;

                this.ApplyLoadedScales();
                this.ApplyLoadedColors();
            }

            // Load toggle details.
            this.Toggle = new OptionsToggle();
        }

        /// <summary>
        /// Builds options window.
        /// </summary>
        /// <param name="windowID">The window identifier.</param>
        private void OptionsWindow(int windowID)
        {
            GUILayout.BeginVertical();

            if (IsDebug)
            {
                GUILayout.Box("Debug", GUILayout.ExpandWidth(true));
                this.CreateScaleOption(true, "ToggleLeftOffset", this.ToggleLeftOffset, x => this.ToggleLeftOffset = x, minScale: -100f, maxScale: 0f);
                this.CreateScaleOption(true, "CursorOffsetX", this.CursorOffsetX, x => this.CursorOffsetX = x, minScale: 0f, maxScale: 10f);
                this.CreateScaleOption(true, "CursorOffsetY", this.CursorOffsetY, x => this.CursorOffsetY = x, minScale: 0f, maxScale: 20f);
            }

            bool flightVectorsActive = FlightGlobals.ActiveVessel != null && FlightGlobals.ActiveVessel.speed > 0.1;
            bool orbitVectorsActive = FlightGlobals.speedDisplayMode == FlightGlobals.SpeedDisplayModes.Orbit;

            // Markers Scale
            GUILayout.Box(ModStrings.OptionLabel.MarkersScaleSection, GUILayout.ExpandWidth(true));
            this.CreateScaleOption(this.NavBallCursor.gameObject.activeSelf, ModStrings.OptionLabel.Cursor,
                this.NavBallCursorScale, x => this.NavBallCursorScale = x);
            this.CreateScaleOption(flightVectorsActive, ModStrings.OptionLabel.ProgradeRetrograde,
                this.NavBallProgradeScale, x => this.NavBallProgradeScale = x);
            this.CreateScaleOption(orbitVectorsActive, ModStrings.OptionLabel.RadialInOut,
                this.NavBallRadialScale, x => this.NavBallRadialScale = x);
            this.CreateScaleOption(orbitVectorsActive, ModStrings.OptionLabel.NormalAntiNormal,
                this.NavBallNormalScale, x => this.NavBallNormalScale = x);
            this.CreateScaleOption(HasNonZeroManeuverNode, ModStrings.OptionLabel.Burn,
                this.NavBallBurnScale, x => this.NavBallBurnScale = x);
            this.CreateScaleOption(FlightGlobals.ActiveVessel?.targetObject != null, ModStrings.OptionLabel.TargetAntiTarget,
                this.NavBallTargetScale, x => this.NavBallTargetScale = x);
            this.CreateScaleOption(HasNavWaypoint, ModStrings.OptionLabel.NavWaypoint,
                this.NavBallNavWaypointScale, x => this.NavBallNavWaypointScale = x);

            // Ghost Markers
            GUILayout.Box(ModStrings.OptionLabel.GhostMarkersSection, GUILayout.ExpandWidth(true));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            bool ghostMarkersEnabled = GUILayout.Toggle(this.NBGhostMarkersEnabled, ModStrings.OptionLabel.EnableGhostMarkers);
            if (this.NBGhostMarkersEnabled && !ghostMarkersEnabled)
            {
                // fix maneuver markers on disable ghost markers
                this.BurnArrowMaterial.SetFloat(PropertyIDs._Opacity, 1f);
                this.BurnMaterial.SetFloat(PropertyIDs._Opacity, 1f);
            }
            this.NBGhostMarkersEnabled = ghostMarkersEnabled;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            this.CreateScaleOption(this.NBGhostMarkersEnabled, ModStrings.OptionLabel.GhostMarkersOpacity,
                this.NBGhostMarkersOpacity, x => this.NBGhostMarkersOpacity = x, minScale: 0f, maxScale: 1f, defaultValue: 0.3f);
            this.CreateScaleOption(this.NBGhostMarkersEnabled, ModStrings.OptionLabel.GhostMarkersScale,
                this.NBGhostMarkersScale, x => this.NBGhostMarkersScale = x, defaultValue: 0.5f);

            // Miscellaneous
            GUILayout.Box(ModStrings.OptionLabel.MiscellaneousSection, GUILayout.ExpandWidth(true));

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(ModStrings.Button.EditColors, GUILayout.MinWidth(266f)))
            {
                this.ProgradeSelected = true;
                this.SendColorToPicker(this.ProgradeMaterial.GetColor(PropertyIDs._TintColor));

                this.ShowOptions = false;
                this.ShowColorOptions = true;
                this.ShowPriorityOptions = false;

                return;
            }
            if (GUILayout.Button(ModStrings.Button.EditPriorities, GUILayout.MinWidth(266f)))
            {
                this.ShowOptions = false;
                this.ShowColorOptions = false;
                this.ShowPriorityOptions = true;

                return;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            this.NBHideOnMap = GUILayout.Toggle(this.NBHideOnMap, ModStrings.OptionLabel.HideOnMap, GUILayout.MaxWidth(285f));
            this.NBAfraidMouseOnMapView = GUILayout.Toggle(this.NBAfraidMouseOnMapView, ModStrings.OptionLabel.AfraidMouseOnMap);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            this.NBAlwaysHideOnMap = GUILayout.Toggle(this.NBAlwaysHideOnMap, ModStrings.OptionLabel.AlwaysHideOnMap, GUILayout.MaxWidth(285f));            
            this.NBAfraidMouseOnFlightView = GUILayout.Toggle(this.NBAfraidMouseOnFlightView, ModStrings.OptionLabel.AfraidMouseOnFlight);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(ModStrings.Button.Save, GUILayout.MinWidth(266f)))
            {
                this.ShowOptions = false;
                this.SaveConfig();
            }
            if (GUILayout.Button(ModStrings.Button.Cancel, GUILayout.MinWidth(266f)))
            {
                this.ShowOptions = false;
                this.LoadConfig();
                this.ApplyLoadedScales();
                this.ApplyLoadedColors();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        /// <summary>
        /// Creates the scale option.
        /// </summary>
        /// <param name="isActive">If set to <c>true</c> - marker is shown.</param>
        /// <param name="label">The option label.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="setValue">The set value action.</param>
        /// <param name="minScale">The minimum scale.</param>
        /// <param name="maxScale">The maximum scale.</param>
        private void CreateScaleOption(bool isActive, string label, float scale, Action<float> setValue,
            float minScale = 0f, float maxScale = 3f, float defaultValue = 1f)
        {
            GUILayout.BeginHorizontal();            

            // Build option label.
            if (!isActive)
            {
                GUI.contentColor = Color.gray;
            }
            GUILayout.Label(label, GUILayout.MinWidth(150f));
            GUI.contentColor = this.DefaultContentColor;

            // Build slider to change option value.
            setValue(GUILayout.HorizontalSlider(scale, minScale, maxScale, GUILayout.Width(300f)));
            
            // Build option value label.
            if (IsDebug)
            {
                GUILayout.Label(scale.ToString(), GUILayout.MinWidth(100f));
            }
            else
            {
                GUILayout.Label(scale.ToString("F2"), GUILayout.MinWidth(30f));
            }

            // Build option Reset button.
            if (GUILayout.Button(ModStrings.Button.Reset))
            {
                setValue(defaultValue);          
            }
            
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Populates the NavBall markers.
        /// </summary>
        private void PopulateNavBallMarkers()
        {
            Transform navBall = FlightUIModeController.Instance.navBall.transform.Find(ModStrings.NavBallTransform.NavBall);

            if (navBall != null)
            {
                Transform navBallVectors = navBall.Find(ModStrings.NavBallTransform.Vectors);

                this.NavBallCursor = navBall.Find(ModStrings.NavBallTransform.Cursor);
                this.NavBallPrograde = navBallVectors.Find(ModStrings.NavBallTransform.Prograde);
                this.NavBallRetrograde = navBallVectors.Find(ModStrings.NavBallTransform.Retrograde);
                this.NavBallRadialIn = navBallVectors.Find(ModStrings.NavBallTransform.RadialIn);
                this.NavBallRadialOut = navBallVectors.Find(ModStrings.NavBallTransform.RadialOut);
                this.NavBallNormal = navBallVectors.Find(ModStrings.NavBallTransform.Normal);
                this.NavBallAntiNormal = navBallVectors.Find(ModStrings.NavBallTransform.AntiNormal);
                this.NavBallBurn = navBallVectors.Find(ModStrings.NavBallTransform.Burn);
                this.NavBallBurnArrow = navBallVectors.Find(ModStrings.NavBallTransform.BurnArrow);
                this.NavBallTarget = navBallVectors.Find(ModStrings.NavBallTransform.Target);
                this.NavBallAntiTarget = navBallVectors.Find(ModStrings.NavBallTransform.AntiTarget);
                
                this.ProgradeMaterial = this.NavBallPrograde.GetComponent<MeshRenderer>().materials[0];
                this.RetrogradeMaterial = this.NavBallRetrograde.GetComponent<MeshRenderer>().materials[0];
                this.RadialInMaterial = this.NavBallRadialIn.GetComponent<MeshRenderer>().materials[0];
                this.RadialOutMaterial = this.NavBallRadialOut.GetComponent<MeshRenderer>().materials[0];
                this.NormalMaterial = this.NavBallNormal.GetComponent<MeshRenderer>().materials[0];
                this.AntiNormalMaterial = this.NavBallAntiNormal.GetComponent<MeshRenderer>().materials[0];
                this.BurnMaterial = this.NavBallBurn.GetComponent<MeshRenderer>().materials[0];
                this.BurnArrowMaterial = this.NavBallBurnArrow.GetComponent<MeshRenderer>().materials[0];
                this.TargetMaterial = this.NavBallTarget.GetComponent<MeshRenderer>().materials[0];
                this.AntiTargetMaterial = this.NavBallAntiTarget.GetComponent<MeshRenderer>().materials[0];
            }            
        }

        /// <summary>
        /// Populates the navigation waypoint marker.
        /// </summary>
        private void PopulateNavWaypointMarker()
        {
            Transform navBall = FlightUIModeController.Instance.navBall.transform.Find(ModStrings.NavBallTransform.NavBall);

            if (navBall != null)
            {
                Transform navBallVectors = navBall.Find(ModStrings.NavBallTransform.Vectors);

                this.NavBallNavWaypoint = navBallVectors.Find(ModStrings.NavBallTransform.NavWaypoint);

                if (this.NavBallNavWaypoint != null)
                {
                    this.NavWaypointMaterial = this.NavBallNavWaypoint.GetComponent<MeshRenderer>().materials[0];
                }
                else
                {
                    this.NavWaypointMaterial = null;
                }
            }
            else
            {
                this.NavBallNavWaypoint = null;
                this.NavWaypointMaterial = null;
            }
        }

        /// <summary>
        /// Scales the NavBall cursor.
        /// </summary>
        /// <param name="scale">The new scale.</param>
        private void ScaleNavBallCursor(float scale)
        {
            // Apply scale.
            this.NavBallCursor.localScale = this.NBCursorInitialScale * scale;
            
            // The scale for NavBall cursor works a little incorrectly, because of incorrect transform anchor.
            // So I should update cursor position as well.
            this.NavBallCursor.localPosition = new Vector3(
                this.NBCursorInitialPosition.x + (this.CursorOffsetX * scale) - this.CursorOffsetX,
                this.NBCursorInitialPosition.y - (this.CursorOffsetY * scale) + this.CursorOffsetY,
                this.NBCursorInitialPosition.z);
        }

        /// <summary>
        /// Determines whether the pixel located on NavBall texture.
        /// </summary>
        /// <param name="point">The pixel location.</param>
        /// <returns>True when pixel located on NavBall texture, otherwise False.</returns>
        private bool IsPixelOnNavBall(Vector2 point)
        {
            return (point.y > GameSettings.SCREEN_RESOLUTION_HEIGHT - this.NBall.TextureCurrentHeight) &&
                (point.x > this.NBall.TransformScreenCenterX - (NavBallHelper.TextureHalfWidth * this.NBall.Scale.x * GameSettings.UI_SCALE)) &&
                (point.x < this.NBall.TransformScreenCenterX + (NavBallHelper.TextureHalfWidth * this.NBall.Scale.y * GameSettings.UI_SCALE));
        }

        /// <summary>
        /// Determines whether the camera is turned on.
        /// </summary>
        /// <returns>True when camera is turned on and screen is not black.</returns>
        private bool IsCameraTurnedOn()
        {
            return UIVectorCamera.Camera == Camera.current;
        }

        /// <summary>
        /// Applies the loaded scales.
        /// </summary>
        private void ApplyLoadedScales()
        {
            this.ScaleNavBallCursor(this.NavBallCursorScale);
            this.NavBallProgradeScale = this.NBProgradeScale;
            this.NavBallRadialScale = this.NBRadialScale;
            this.NavBallNormalScale = this.NBNormalScale;
            this.NavBallBurnScale = this.NBBurnScale;
            this.NavBallTargetScale = this.NBTargetScale;
        }

        /// <summary>
        /// Updates Ghosts Markers visibility.
        /// </summary>
        private void UpdateGhostMarkers()
        {
            if (FlightGlobals.ActiveVessel.speed > 0.1)
            {
                this.UpdateGhostMarker(this.ProgradeMaterial, this.NavBallPrograde, this.NBProgradeScale);
                this.UpdateGhostMarker(this.RetrogradeMaterial, this.NavBallRetrograde, this.NBProgradeScale);
                
                if (FlightGlobals.speedDisplayMode == FlightGlobals.SpeedDisplayModes.Orbit)
                {
                    this.UpdateGhostMarker(this.RadialInMaterial, this.NavBallRadialIn, this.NBRadialScale);
                    this.UpdateGhostMarker(this.RadialOutMaterial, this.NavBallRadialOut, this.NBRadialScale);
                    this.UpdateGhostMarker(this.NormalMaterial, this.NavBallNormal, this.NBNormalScale);
                    this.UpdateGhostMarker(this.AntiNormalMaterial, this.NavBallAntiNormal, this.NBNormalScale);
                }
            }

            if (this.HasNonZeroManeuverNode)
            {
                this.UpdateGhostMarker(this.BurnMaterial, this.NavBallBurn, this.NBBurnScale, isBurnMarker: true);
            }

            if (this.HasManeuverNode)
            {
                this.BurnArrowMaterial.SetFloat(PropertyIDs._Opacity, 0.0001f);
            }

            if (FlightGlobals.ActiveVessel.targetObject != null)
            {
                this.UpdateGhostMarker(this.TargetMaterial, this.NavBallTarget, this.NBTargetScale);
                this.UpdateGhostMarker(this.AntiTargetMaterial, this.NavBallAntiTarget, this.NBTargetScale);
            }

            if (HasNavWaypoint)
            {
                this.UpdateGhostMarker(this.NavWaypointMaterial, this.NavBallNavWaypoint, this.NBNavWaypointScale);
            }
        }

        /// <summary>
        /// Updates Ghost Marker opacity and activity.
        /// </summary>
        /// <param name="material">Marker material.</param>
        /// <param name="transform">Marker transform.</param>
        /// <param name="currentScale">Marker current scale.</param>
        private void UpdateGhostMarker(Material material, Transform transform, float? currentScale = null, bool isBurnMarker = false)
        {
            if (material != null && transform != null)
            {
                if (isBurnMarker)
                {
                    material.SetFloat(PropertyIDs._Opacity, this.NavBallBurnArrow.gameObject.activeSelf ? this.NBGhostMarkersOpacity : 1f);
                }
                else if (material.GetFloat(PropertyIDs._Opacity) < this.NBGhostMarkersOpacity)
                {
                    material.SetFloat(PropertyIDs._Opacity, this.NBGhostMarkersOpacity);
                }

                if (currentScale != null)
                {
                    float multiplier = (1.00001f - material.GetFloat(PropertyIDs._Opacity)) / (1.00001f - this.NBGhostMarkersOpacity);
                    // =1-((1-A1)^(1/2))
                    multiplier = 1 - (float)Math.Sqrt(1 - multiplier);
                    float ghostScale = 1 - (multiplier * (1 - this.NBGhostMarkersScale));

                    transform.localScale = this.NBVectorsInitialScale * currentScale.Value * ghostScale;
                }

                if (!transform.gameObject.activeSelf)
                {
                    transform.gameObject.SetActive(true);
                }
            }
        }
        #endregion
    }
}
