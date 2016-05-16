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
    public class NavBallAdjustor : MonoBehaviour
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
        /// The NavBall prograde.
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
        /// The NavBall target marker.
        /// </summary>
        private Transform NavBallTarget = null;

        /// <summary>
        /// The NavBall antitarget marker.
        /// </summary>
        private Transform NavBallAntiTarget = null;

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
        /// The mod options window display state.
        /// </summary>
        private bool ShowOptions = false;

        /// <summary>
        /// The mod options window rectangle.
        /// </summary>
        private Rect ModOptionsRect = new Rect(300, 200, 558, 80);

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
        /// Indicates whether the NavBall is scared now.
        /// </summary>
        private bool IsNavBallScared = false;
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
        #endregion

        #region Public_Methods
        /// <summary>
        /// Called on flight begin.
        /// </summary>
        public void Start()
        {
            this.HashCode = ModStrings.ModName.GetHashCode();

            // Load mod configuration.
            if (File.Exists<NavBallAdjustor>(ModStrings.ConfigFile))
            {
                ConfigNode config = ConfigNode.Load(IOUtils.GetFilePathFor(this.GetType(), ModStrings.ConfigFile));
                ConfigNode.LoadObjectFromConfig(this, config);
            }

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
        /// Called on update MonoBehaviour frame.
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
            }

            // Afraid mouse cursor.
            if (!FlightDriver.Pause)
            {
                if ((this.NBAfraidMouseOnMapView && MapView.MapIsEnabled) ||
                    (this.NBAfraidMouseOnFlightView && !MapView.MapIsEnabled))
                {
                    if (this.IsPixelAboveNavBall(Mouse.screenPos))
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
        }

        /// <summary>
        /// Called on GUI tick.
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
                || this.PrevNavBallPosY != this.NBall.Position.y
                || IsDebug)
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
                this.SliderStyle = GUI.skin.horizontalSlider;
                this.SliderStyle.margin = new RectOffset(0, 0, 9, 0); // add top margin for better look
            }

            if (HighLogic.LoadedSceneIsFlight)
            {
                this.ShowOptions = !FlightDriver.Pause && GUI.Toggle(
                    this.Toggle.Rectangle,
                    this.ShowOptions,
                    GUIContent.none,
                    this.Toggle.Style);
            }

            if (this.ShowOptions && CameraManager.Instance.currentCameraMode != CameraManager.CameraMode.IVA)
            {
                // Display mod options window.
                this.ModOptionsRect = GUILayout.Window(
                    this.HashCode, this.ModOptionsRect, ModOptionsWindow, ModStrings.OptionsWindowTitle);
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
            else if (!this.NBHideOnMap)
            {
                NavBallToggle.Instance.panel.Expand();
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

                // Apply scale
                this.ScaleNavBallCursor(this.NavBallCursorScale);
                this.NavBallProgradeScale = this.NBProgradeScale;
                this.NavBallRadialScale = this.NBRadialScale;
                this.NavBallNormalScale = this.NBNormalScale;
                this.NavBallBurnScale = this.NBBurnScale;
                this.NavBallTargetScale = this.NBTargetScale;
            }

            // Load toggle details.
            this.Toggle = new OptionsToggle();
        }

        /// <summary>
        /// Builds mod options window.
        /// </summary>
        /// <param name="windowID">The window identifier.</param>
        private void ModOptionsWindow(int windowID)
        {
            GUILayout.BeginVertical();

            #if DEBUG
            GUILayout.Box("Debug", GUILayout.ExpandWidth(true));
            CreateScaleOption("ToggleLeftOffset", this.ToggleLeftOffset, x => this.ToggleLeftOffset = x, minScale: -100f, maxScale: 0f);
            CreateScaleOption("CursorOffsetX", this.CursorOffsetX, x => this.CursorOffsetX = x, minScale: 0f, maxScale: 10f);
            CreateScaleOption("CursorOffsetY", this.CursorOffsetY, x => this.CursorOffsetY = x, minScale: 0f, maxScale: 20f);
            #endif
            
            GUILayout.Box(ModStrings.OptionLabel.Markers, GUILayout.ExpandWidth(true));
            CreateScaleOption(ModStrings.OptionLabel.Cursor, this.NavBallCursorScale, x => this.NavBallCursorScale = x);
            CreateScaleOption(ModStrings.OptionLabel.Prograde, this.NavBallProgradeScale, x => this.NavBallProgradeScale = x);
            CreateScaleOption(ModStrings.OptionLabel.Radial, this.NavBallRadialScale, x => this.NavBallRadialScale = x);
            CreateScaleOption(ModStrings.OptionLabel.Normal, this.NavBallNormalScale, x => this.NavBallNormalScale = x);
            CreateScaleOption(ModStrings.OptionLabel.Burn, this.NavBallBurnScale, x => this.NavBallBurnScale = x);
            CreateScaleOption(ModStrings.OptionLabel.Target, this.NavBallTargetScale, x => this.NavBallTargetScale = x);

            GUILayout.Box(ModStrings.OptionLabel.Miscellaneous, GUILayout.ExpandWidth(true));
            GUILayout.BeginHorizontal();
            this.NBHideOnMap = GUILayout.Toggle(this.NBHideOnMap, ModStrings.OptionLabel.HideOnMap, GUILayout.MaxWidth(300f));
            this.NBAfraidMouseOnMapView = GUILayout.Toggle(this.NBAfraidMouseOnMapView, ModStrings.OptionLabel.AfraidMouseOnMap);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            this.NBAlwaysHideOnMap = GUILayout.Toggle(this.NBAlwaysHideOnMap, ModStrings.OptionLabel.AlwaysHideOnMap, GUILayout.MaxWidth(300f));            
            this.NBAfraidMouseOnFlightView = GUILayout.Toggle(this.NBAfraidMouseOnFlightView, ModStrings.OptionLabel.AfraidMouseOnFlight);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(ModStrings.Button.Save)) SaveConfig();
            this.ShowOptions = !GUILayout.Button(ModStrings.Button.Close);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        /// <summary>
        /// Creates the scale option.
        /// </summary>
        /// <param name="label">The option label.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="setValue">The set value action.</param>
        /// <param name="minScale">The minimum scale.</param>
        /// <param name="maxScale">The maximum scale.</param>
        private void CreateScaleOption(string label, float scale, Action<float> setValue,
            float minScale = 0f, float maxScale = 3f)
        {
            GUILayout.BeginHorizontal();

            // Build option label.
            GUILayout.Label(label, GUILayout.MinWidth(150f));

            // Build slider to change option value.
            setValue(GUILayout.HorizontalSlider(scale, minScale, maxScale, this.SliderStyle, GUI.skin.horizontalSliderThumb, GUILayout.Width(300f)));
            
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
                setValue(1f);          
            }

            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Populates the NavBall markers.
        /// </summary>
        private void PopulateNavBallMarkers()
        {
            Transform navBall = FlightUIModeController.Instance.navBall.transform.FindChild(ModStrings.NavBallTransform.NavBall);
            Transform navBallVectors = navBall.FindChild(ModStrings.NavBallTransform.Vectors);

            if (navBall != null)
            {
                this.NavBallCursor = navBall.FindChild(ModStrings.NavBallTransform.Cursor);
                this.NavBallPrograde = navBallVectors.FindChild(ModStrings.NavBallTransform.Prograde);
                this.NavBallRetrograde = navBallVectors.FindChild(ModStrings.NavBallTransform.Retrograde);
                this.NavBallRadialIn = navBallVectors.FindChild(ModStrings.NavBallTransform.RadialIn);
                this.NavBallRadialOut = navBallVectors.FindChild(ModStrings.NavBallTransform.RadialOut);
                this.NavBallNormal = navBallVectors.FindChild(ModStrings.NavBallTransform.Normal);
                this.NavBallAntiNormal = navBallVectors.FindChild(ModStrings.NavBallTransform.AntiNormal);
                this.NavBallBurn = navBallVectors.FindChild(ModStrings.NavBallTransform.Burn);
                this.NavBallTarget = navBallVectors.FindChild(ModStrings.NavBallTransform.Target);
                this.NavBallAntiTarget = navBallVectors.FindChild(ModStrings.NavBallTransform.AntiTarget);
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
        /// Determines whether the pixel located above NavBall texture.
        /// </summary>
        /// <param name="point">The pixel location.</param>
        /// <returns>True when pixel located above NavBall texture, otherwise False.</returns>
        private bool IsPixelAboveNavBall(Vector2 point)
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
        #endregion
    }
}
