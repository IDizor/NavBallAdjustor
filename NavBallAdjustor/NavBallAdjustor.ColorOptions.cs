using UnityEngine;

namespace NavBallAdjustor
{
    /// <summary>
    /// Implements functionality for ColorPicker.
    /// </summary>
    public partial class NavBallAdjustor : MonoBehaviour
    {
        /// <summary>
        /// The prograde color.
        /// </summary>
        [Persistent]
        private Color ProgradeColor;

        /// <summary>
        /// The retrograde color.
        /// </summary>
        [Persistent]
        private Color RetrogradeColor;

        /// <summary>
        /// The radial-in color.
        /// </summary>
        [Persistent]
        private Color RadialInColor;

        /// <summary>
        /// The radial-out color.
        /// </summary>
        [Persistent]
        private Color RadialOutColor;

        /// <summary>
        /// The normal color.
        /// </summary>
        [Persistent]
        private Color NormalColor;

        /// <summary>
        /// The anti-normal color.
        /// </summary>
        [Persistent]
        private Color AntiNormalColor;

        /// <summary>
        /// The burn color.
        /// </summary>
        [Persistent]
        private Color BurnColor;

        /// <summary>
        /// The target color.
        /// </summary>
        [Persistent]
        private Color TargetColor;

        /// <summary>
        /// The anti-target color.
        /// </summary>
        [Persistent]
        private Color AntiTargetColor;

        /// <summary>
        /// The nav waypoint color.
        /// </summary>
        [Persistent]
        private Color NavWaypointColor;

        /// <summary>
        /// The zero color.
        /// </summary>
        private Color ZeroColor = new Color();

        /// <summary>
        /// The colors window rectangle.
        /// </summary>
        private Rect ColorsWindowRect = new Rect(300, 200, 500, 295);

        /// <summary>
        /// The color picker rectangle.
        /// </summary>
        private Rect ColorPickerRect = new Rect(14, 25, 198, 198);

        /// <summary>
        /// The previous color rectangle.
        /// </summary>
        private Rect PrevColorRect = new Rect(14, 235, 99, 50);

        /// <summary>
        /// The new color rectangle.
        /// </summary>
        private Rect NewColorRect = new Rect(113, 235, 99, 50);

        /// <summary>
        /// The color picker texture.
        /// </summary>
        private Texture2D ColorPickerTexture;

        /// <summary>
        /// The previous color texture.
        /// </summary>
        private Texture2D PrevColorTexture = new Texture2D(99, 50);

        /// <summary>
        /// The new color texture.
        /// </summary>
        private Texture2D NewColorTexture = new Texture2D(99, 50);

        /// <summary>
        /// The color picker previous color.
        /// </summary>
        private Color PrevColor;

        /// <summary>
        /// The color picker new color.
        /// </summary>
        private Color NewColor;

        /// <summary>
        /// The default cursor color.
        /// </summary>
        private Color DefaultCursorColor;

        /// <summary>
        /// The default prograde color.
        /// </summary>
        private Color DefaultProgradeColor = new Color(0.847f, 1f, 0f, 1f);

        /// <summary>
        /// The default radial color.
        /// </summary>
        private Color DefaultRadialColor = new Color(0f, 1f, 0.957f, 1f);

        /// <summary>
        /// The default normal color.
        /// </summary>
        private Color DefaultNormalColor = new Color(0.925f, 0f, 1f, 1f);

        /// <summary>
        /// The default burn color.
        /// </summary>
        private Color DefaultBurnColor = new Color(0f, 0.161f, 1f, 1f);

        /// <summary>
        /// The default target color.
        /// </summary>
        private Color DefaultTargetColor = new Color(0.966f, 0.329f, 0.957f, 1f);

        /// <summary>
        /// The default nav waypoint color 1.
        /// </summary>
        private Color DefaultNavWaypointColor1 = new Color(0.722f, 0.792f, 0.447f, 1f);

        /// <summary>
        /// The default nav waypoint color 2.
        /// </summary>
        private Color DefaultNavWaypointColor2 = new Color(1f, 0f, 0.725f, 1f);

        /// <summary>
        /// The NavBall cursor material.
        /// </summary>
        private Material CursorMaterial = null;

        /// <summary>
        /// The NavBall prograde marker material.
        /// </summary>
        private Material ProgradeMaterial = null;

        /// <summary>
        /// The NavBall retrograde marker material.
        /// </summary>
        private Material RetrogradeMaterial = null;

        /// <summary>
        /// The NavBall radial-in marker material.
        /// </summary>
        private Material RadialInMaterial = null;

        /// <summary>
        /// The NavBall radial-out marker material.
        /// </summary>
        private Material RadialOutMaterial = null;

        /// <summary>
        /// The NavBall normal marker material.
        /// </summary>
        private Material NormalMaterial = null;

        /// <summary>
        /// The NavBall antinormal marker material.
        /// </summary>
        private Material AntiNormalMaterial = null;

        /// <summary>
        /// The NavBall burn marker material.
        /// </summary>
        private Material BurnMaterial = null;

        /// <summary>
        /// The NavBall burn arrow material.
        /// </summary>
        private Material BurnArrowMaterial = null;

        /// <summary>
        /// The NavBall target marker material.
        /// </summary>
        private Material TargetMaterial = null;

        /// <summary>
        /// The NavBall antitarget marker material.
        /// </summary>
        private Material AntiTargetMaterial = null;

        /// <summary>
        /// The NavBall survey navigation waypoint marker material.
        /// </summary>
        private Material NavWaypointMaterial = null;

        /// <summary>
        /// The variables for selected markers properties.
        /// </summary>
        private bool _ProgradeSelected = false;
        private bool _RetrogradeSelected = false;
        private bool _RadialInSelected = false;
        private bool _RadialOutSelected = false;
        private bool _NormalSelected = false;
        private bool _AntiNormalSelected = false;
        private bool _BurnSelected = false;
        private bool _TargetSelected = false;
        private bool _AntiTargetSelected = false;
        private bool _NavWaypointSelected = false;

        /// <summary>
        /// Gets or sets a value indicating whether [prograde selected].
        /// </summary>
        private bool ProgradeSelected
        {
            get { return this._ProgradeSelected; }
            set
            {
                if (value)
                {
                    RetrogradeSelected = RadialInSelected = RadialOutSelected = NormalSelected = AntiNormalSelected =
                        BurnSelected = TargetSelected = AntiTargetSelected = NavWaypointSelected = false;

                    this.SendColorToPicker(this.ProgradeMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._ProgradeSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [retrograde selected].
        /// </summary>
        private bool RetrogradeSelected
        {
            get { return this._RetrogradeSelected; }
            set
            {
                if (value)
                {
                    ProgradeSelected = RadialInSelected = RadialOutSelected = NormalSelected = AntiNormalSelected =
                        BurnSelected = TargetSelected = AntiTargetSelected = NavWaypointSelected = false;

                    this.SendColorToPicker(this.RetrogradeMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._RetrogradeSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [radial in selected].
        /// </summary>
        private bool RadialInSelected
        {
            get { return this._RadialInSelected; }
            set
            {
                if (value)
                {
                    ProgradeSelected = RetrogradeSelected = RadialOutSelected = NormalSelected = AntiNormalSelected =
                        BurnSelected = TargetSelected = AntiTargetSelected = NavWaypointSelected = false;

                    this.SendColorToPicker(this.RadialInMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._RadialInSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [radial out selected].
        /// </summary>
        private bool RadialOutSelected
        {
            get { return this._RadialOutSelected; }
            set
            {
                if (value)
                {
                    ProgradeSelected = RetrogradeSelected = RadialInSelected = NormalSelected = AntiNormalSelected =
                        BurnSelected = TargetSelected = AntiTargetSelected = NavWaypointSelected = false;

                    this.SendColorToPicker(this.RadialOutMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._RadialOutSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [normal selected].
        /// </summary>
        private bool NormalSelected
        {
            get { return this._NormalSelected; }
            set
            {
                if (value)
                {
                    ProgradeSelected = RetrogradeSelected = RadialInSelected = RadialOutSelected = AntiNormalSelected =
                        BurnSelected = TargetSelected = AntiTargetSelected = NavWaypointSelected = false;

                    this.SendColorToPicker(this.NormalMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._NormalSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [anti normal selected].
        /// </summary>
        private bool AntiNormalSelected
        {
            get { return this._AntiNormalSelected; }
            set
            {
                if (value)
                {
                    ProgradeSelected = RetrogradeSelected = RadialInSelected = RadialOutSelected = NormalSelected =
                        BurnSelected = TargetSelected = AntiTargetSelected = NavWaypointSelected = false;

                    this.SendColorToPicker(this.AntiNormalMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._AntiNormalSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [burn selected].
        /// </summary>
        private bool BurnSelected
        {
            get { return this._BurnSelected; }
            set
            {
                if (value)
                {
                    ProgradeSelected = RetrogradeSelected = RadialInSelected = RadialOutSelected = NormalSelected =
                        AntiNormalSelected = TargetSelected = AntiTargetSelected = NavWaypointSelected = false;

                    this.SendColorToPicker(this.BurnMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._BurnSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [target selected].
        /// </summary>
        private bool TargetSelected
        {
            get { return this._TargetSelected; }
            set
            {
                if (value)
                {
                    ProgradeSelected = RetrogradeSelected = RadialInSelected = RadialOutSelected = NormalSelected =
                        AntiNormalSelected = BurnSelected = AntiTargetSelected = NavWaypointSelected = false;

                    this.SendColorToPicker(this.TargetMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._TargetSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [anti target selected].
        /// </summary>
        private bool AntiTargetSelected
        {
            get { return this._AntiTargetSelected; }
            set
            {
                if (value)
                {
                    ProgradeSelected = RetrogradeSelected = RadialInSelected = RadialOutSelected = NormalSelected =
                        AntiNormalSelected = BurnSelected = TargetSelected = NavWaypointSelected = false;

                    this.SendColorToPicker(this.AntiTargetMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._AntiTargetSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [nav waypoint selected].
        /// </summary>
        private bool NavWaypointSelected
        {
            get { return this._NavWaypointSelected; }
            set
            {
                if (value)
                {
                    ProgradeSelected = RetrogradeSelected = RadialInSelected = RadialOutSelected = NormalSelected =
                        AntiNormalSelected = BurnSelected = TargetSelected = AntiTargetSelected = false;

                    this.SendColorToPicker(this.NavWaypointMaterial == null
                        ? this.NavWaypointColor
                        : this.NavWaypointMaterial.GetColor(PropertyIDs._TintColor));
                }

                this._NavWaypointSelected = value;
            }
        }

        /// <summary>
        /// The color options labels offset style.
        /// </summary>
        private GUIStyle ColorOptionsLabelsOffsetStyle;

        /// <summary>
        /// Fills the texture with color.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="color">The color.</param>
        private void FillTexture(ref Texture2D texture, Color color)
        {
            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply();
        }

        /// <summary>
        /// Displays colors options window.
        /// </summary>
        /// <param name="windowID">The window identifier.</param>
        private void ColorOptionsWindow(int windowID)
        {
            GUILayout.BeginVertical();

            GUILayout.BeginVertical(this.ColorOptionsLabelsOffsetStyle);
            
            // Prograde & Retrograde
            GUILayout.BeginHorizontal();            
            GUI.contentColor = this.ProgradeMaterial.GetColor(PropertyIDs._TintColor);
            if (GUILayout.Toggle(this.ProgradeSelected, ModStrings.OptionLabel.Prograde, GUILayout.MinWidth(100f)) && !this.ProgradeSelected)
            {
                this.ProgradeSelected = true;
            }
            GUI.contentColor = this.DefaultContentColor;
            if (GUILayout.Button(this.RetrogradeSelected ? "<" : ">", GUILayout.MaxWidth(20f)))
            {
                (this.RetrogradeSelected ? this.ProgradeMaterial : this.RetrogradeMaterial).SetColor(PropertyIDs._TintColor,
                (this.RetrogradeSelected ? this.RetrogradeMaterial : this.ProgradeMaterial).GetColor(PropertyIDs._TintColor));
            }
            GUI.contentColor = this.RetrogradeMaterial.GetColor(PropertyIDs._TintColor);
            if (GUILayout.Toggle(this.RetrogradeSelected, ModStrings.OptionLabel.Retrograde, GUILayout.MinWidth(100f)) && !this.RetrogradeSelected)
            {
                this.RetrogradeSelected = true;
            }
            GUI.contentColor = this.DefaultProgradeColor;
            if (GUILayout.Button(ModStrings.Button.Default))
            {
                this.ProgradeMaterial.SetColor(PropertyIDs._TintColor, this.DefaultProgradeColor);
                this.RetrogradeMaterial.SetColor(PropertyIDs._TintColor, this.DefaultProgradeColor);
                if (this.ProgradeSelected || this.RetrogradeSelected) this.SendColorToPicker(this.DefaultProgradeColor);
            }
            GUILayout.EndHorizontal();

            // Radial In & Radial Out
            GUILayout.BeginHorizontal();
            GUI.contentColor = this.RadialInMaterial.GetColor(PropertyIDs._TintColor);
            if (GUILayout.Toggle(this.RadialInSelected, ModStrings.OptionLabel.RadialIn, GUILayout.MinWidth(100f)) && !this.RadialInSelected)
            {
                this.RadialInSelected = true;
            }
            GUI.contentColor = this.DefaultContentColor;
            if (GUILayout.Button(this.RadialOutSelected ? "<" : ">", GUILayout.MaxWidth(20f)))
            {
                (this.RadialOutSelected ? this.RadialInMaterial : this.RadialOutMaterial).SetColor(PropertyIDs._TintColor,
                (this.RadialOutSelected ? this.RadialOutMaterial : this.RadialInMaterial).GetColor(PropertyIDs._TintColor));
            }
            GUI.contentColor = this.RadialOutMaterial.GetColor(PropertyIDs._TintColor);
            if (GUILayout.Toggle(this.RadialOutSelected, ModStrings.OptionLabel.RadialOut, GUILayout.MinWidth(100f)) && !this.RadialOutSelected)
            {
                this.RadialOutSelected = true;
            }
            GUI.contentColor = this.DefaultRadialColor;
            if (GUILayout.Button(ModStrings.Button.Default))
            {
                this.RadialInMaterial.SetColor(PropertyIDs._TintColor, this.DefaultRadialColor);
                this.RadialOutMaterial.SetColor(PropertyIDs._TintColor, this.DefaultRadialColor);
                if (this.RadialInSelected || this.RadialOutSelected) this.SendColorToPicker(this.DefaultRadialColor);
            }
            GUILayout.EndHorizontal();

            // Normal & AntiNormal
            GUILayout.BeginHorizontal();
            GUI.contentColor = this.NormalMaterial.GetColor(PropertyIDs._TintColor);
            if (GUILayout.Toggle(this.NormalSelected, ModStrings.OptionLabel.Normal, GUILayout.MinWidth(100f)) && !this.NormalSelected)
            {
                this.NormalSelected = true;
            }
            GUI.contentColor = this.DefaultContentColor;
            if (GUILayout.Button(this.AntiNormalSelected ? "<" : ">", GUILayout.MaxWidth(20f)))
            {
                (this.AntiNormalSelected ? this.NormalMaterial : this.AntiNormalMaterial).SetColor(PropertyIDs._TintColor,
                (this.AntiNormalSelected ? this.AntiNormalMaterial : this.NormalMaterial).GetColor(PropertyIDs._TintColor));
            }
            GUI.contentColor = this.AntiNormalMaterial.GetColor(PropertyIDs._TintColor);
            if (GUILayout.Toggle(this.AntiNormalSelected, ModStrings.OptionLabel.AntiNormal, GUILayout.MinWidth(100f)) && !this.AntiNormalSelected)
            {
                this.AntiNormalSelected = true;
            }
            GUI.contentColor = this.DefaultNormalColor;
            if (GUILayout.Button(ModStrings.Button.Default))
            {
                this.NormalMaterial.SetColor(PropertyIDs._TintColor, this.DefaultNormalColor);
                this.AntiNormalMaterial.SetColor(PropertyIDs._TintColor, this.DefaultNormalColor);
                if (this.NormalSelected || this.AntiNormalSelected) this.SendColorToPicker(this.DefaultNormalColor);
            }
            GUILayout.EndHorizontal();

            // Target & AntiTarget
            GUILayout.BeginHorizontal();
            GUI.contentColor = this.TargetMaterial.GetColor(PropertyIDs._TintColor);
            if (GUILayout.Toggle(this.TargetSelected, ModStrings.OptionLabel.Target, GUILayout.MinWidth(100f)) && !this.TargetSelected)
            {
                this.TargetSelected = true;
            }
            GUI.contentColor = this.DefaultContentColor;
            if (GUILayout.Button(this.AntiTargetSelected ? "<" : ">", GUILayout.MaxWidth(20f)))
            {
                (this.AntiTargetSelected ? this.TargetMaterial : this.AntiTargetMaterial).SetColor(PropertyIDs._TintColor,
                (this.AntiTargetSelected ? this.AntiTargetMaterial : this.TargetMaterial).GetColor(PropertyIDs._TintColor));
            }
            GUI.contentColor = this.AntiTargetMaterial.GetColor(PropertyIDs._TintColor);
            if (GUILayout.Toggle(this.AntiTargetSelected, ModStrings.OptionLabel.AntiTarget, GUILayout.MinWidth(100f)) && !this.AntiTargetSelected)
            {
                this.AntiTargetSelected = true;
            }
            GUI.contentColor = this.DefaultTargetColor;
            if (GUILayout.Button(ModStrings.Button.Default))
            {
                this.TargetMaterial.SetColor(PropertyIDs._TintColor, this.DefaultTargetColor);
                this.AntiTargetMaterial.SetColor(PropertyIDs._TintColor, this.DefaultTargetColor);
                if (this.TargetSelected || this.AntiTargetSelected) this.SendColorToPicker(this.DefaultTargetColor);
            }
            GUILayout.EndHorizontal();

            // Maneuver
            GUILayout.BeginHorizontal();
            GUI.contentColor = this.BurnMaterial.GetColor(PropertyIDs._TintColor);
            if (GUILayout.Toggle(this.BurnSelected, ModStrings.OptionLabel.Burn, GUILayout.MinWidth(229f)) && !this.BurnSelected)
            {
                this.BurnSelected = true;
            }
            GUI.contentColor = this.DefaultBurnColor;
            if (GUILayout.Button(ModStrings.Button.Default))
            {
                this.BurnMaterial.SetColor(PropertyIDs._TintColor, this.DefaultBurnColor);
                this.BurnArrowMaterial.SetColor(PropertyIDs._TintColor, this.DefaultBurnColor);
                if (this.BurnSelected) this.SendColorToPicker(this.DefaultBurnColor);
            }
            GUILayout.EndHorizontal();

            // Nav Waypoint
            GUILayout.BeginHorizontal();
            GUI.contentColor = HasNavWaypoint
                ? this.NavWaypointMaterial.GetColor(PropertyIDs._TintColor)
                : this.NavWaypointColor;

            if (GUILayout.Toggle(this.NavWaypointSelected, ModStrings.OptionLabel.NavWaypoint, GUILayout.MinWidth(147f)) && !this.NavWaypointSelected)
            {
                this.NavWaypointSelected = true;
            }
            GUI.contentColor = this.DefaultNavWaypointColor1;
            if (GUILayout.Button(ModStrings.Button.Default1))
            {
                if (this.NavWaypointMaterial != null)
                {
                    this.NavWaypointMaterial.SetColor(PropertyIDs._TintColor, this.DefaultNavWaypointColor1);
                }

                this.NavWaypointColor = this.DefaultNavWaypointColor1;
                if (this.NavWaypointSelected) this.SendColorToPicker(this.DefaultNavWaypointColor1);
            }
            GUI.contentColor = this.DefaultNavWaypointColor2;
            if (GUILayout.Button(ModStrings.Button.Default2))
            {
                if (this.NavWaypointMaterial != null)
                {
                    this.NavWaypointMaterial.SetColor(PropertyIDs._TintColor, this.DefaultNavWaypointColor2);
                }

                this.NavWaypointColor = this.DefaultNavWaypointColor2;
                if (this.NavWaypointSelected) this.SendColorToPicker(this.DefaultNavWaypointColor2);
            }
            GUILayout.EndHorizontal();

            GUI.contentColor = this.DefaultContentColor;

            GUILayout.Space(90f);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(ModStrings.Button.Save))
            {
                this.ProgradeColor = this.ProgradeMaterial.GetColor(PropertyIDs._TintColor);
                this.RetrogradeColor = this.RetrogradeMaterial.GetColor(PropertyIDs._TintColor);
                this.RadialInColor = this.RadialInMaterial.GetColor(PropertyIDs._TintColor);
                this.RadialOutColor = this.RadialOutMaterial.GetColor(PropertyIDs._TintColor);
                this.NormalColor = this.NormalMaterial.GetColor(PropertyIDs._TintColor);
                this.AntiNormalColor = this.AntiNormalMaterial.GetColor(PropertyIDs._TintColor);
                this.BurnColor = this.BurnMaterial.GetColor(PropertyIDs._TintColor);
                this.TargetColor = this.TargetMaterial.GetColor(PropertyIDs._TintColor);
                this.AntiTargetColor = this.AntiTargetMaterial.GetColor(PropertyIDs._TintColor);

                if (HasNavWaypoint && this.NavWaypointMaterial != null)
                {
                    this.NavWaypointColor = this.NavWaypointMaterial.GetColor(PropertyIDs._TintColor);
                }

                this.SaveConfig();
                this.ShowColorOptions = false;

                return;
            }
            if (GUILayout.Button(ModStrings.Button.Cancel))
            {
                this.ShowColorOptions = false;
                this.LoadConfig();
                this.ApplyLoadedScales();
                this.ApplyLoadedColors();

                return;
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            // Color picker
            GUI.Box(this.ColorPickerRect, string.Empty);

            if (GUI.RepeatButton(this.ColorPickerRect, this.ColorPickerTexture, GUIStyle.none))
            {
                int x = (int)(Mouse.screenPos.x - this.ColorsWindowRect.xMin - this.ColorPickerRect.xMin);
                int y = (int)(Mouse.screenPos.y - this.ColorsWindowRect.yMin - this.ColorPickerRect.yMin);

                this.NewColor = this.ColorPickerTexture.GetPixel(x, -y);
                this.FillTexture(ref this.NewColorTexture, this.NewColor);

                this.ApplyNewColor();
            }

            // Prev & New colors preview
            GUI.Box(this.PrevColorRect, this.PrevColorTexture, GUIStyle.none);
            GUI.Box(this.NewColorRect, this.NewColorTexture, GUIStyle.none);

            if (GUI.Button(this.PrevColorRect, string.Empty, GUIStyle.none))
            {
                this.NewColor = this.PrevColor;
                this.FillTexture(ref this.NewColorTexture, this.NewColor);
                this.ApplyNewColor();
            }

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        /// <summary>
        /// Sends color to the color picker.
        /// </summary>
        /// <param name="color">The color.</param>
        private void SendColorToPicker(Color color)
        {
            this.PrevColor = this.NewColor = color;
            this.FillTexture(ref this.PrevColorTexture, this.PrevColor);
            this.FillTexture(ref this.NewColorTexture, this.NewColor);
        }

        /// <summary>
        /// Applies the new color to selected marker.
        /// </summary>
        private void ApplyNewColor()
        {
            if (this.ProgradeSelected)
            {
                this.ProgradeMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
            }
            else if (this.RetrogradeSelected)
            {
                this.RetrogradeMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
            }
            else if (this.RadialInSelected)
            {
                this.RadialInMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
            }
            else if (this.RadialOutSelected)
            {
                this.RadialOutMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
            }
            else if (this.NormalSelected)
            {
                this.NormalMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
            }
            else if (this.AntiNormalSelected)
            {
                this.AntiNormalMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
            }
            else if (this.BurnSelected)
            {
                this.BurnMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
                this.BurnArrowMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
            }
            else if (this.TargetSelected)
            {
                this.TargetMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
            }
            else if (this.AntiTargetSelected)
            {
                this.AntiTargetMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
            }
            else if (this.NavWaypointSelected)
            {
                if (this.NavWaypointMaterial != null)
                {
                    this.NavWaypointMaterial.SetColor(PropertyIDs._TintColor, this.NewColor);
                }

                // For nav waypoint we should always set savable variable,
                // since the game does reset nav waypoint material color on set waypoint event.
                this.NavWaypointColor = this.NewColor;
            }
        }

        /// <summary>
        /// Applies the loaded colors.
        /// </summary>
        private void ApplyLoadedColors()
        {
            if (this.ProgradeColor != this.ZeroColor)
            {
                this.ProgradeMaterial.SetColor(PropertyIDs._TintColor, this.ProgradeColor);
            }
            else
            {
                this.ProgradeColor = this.ProgradeMaterial.GetColor(PropertyIDs._TintColor);
            }

            if (this.RetrogradeColor != this.ZeroColor)
            {
                this.RetrogradeMaterial.SetColor(PropertyIDs._TintColor, this.RetrogradeColor);
            }
            else
            {
                this.RetrogradeColor = this.RetrogradeMaterial.GetColor(PropertyIDs._TintColor);
            }

            if (this.RadialInColor != this.ZeroColor)
            {
                this.RadialInMaterial.SetColor(PropertyIDs._TintColor, this.RadialInColor);
            }
            else
            {
                this.RadialInColor = this.RadialInMaterial.GetColor(PropertyIDs._TintColor);
            }

            if (this.RadialOutColor != this.ZeroColor)
            {
                this.RadialOutMaterial.SetColor(PropertyIDs._TintColor, this.RadialOutColor);
            }
            else
            {
                this.RadialOutColor = this.RadialOutMaterial.GetColor(PropertyIDs._TintColor);
            }

            if (this.NormalColor != this.ZeroColor)
            {
                this.NormalMaterial.SetColor(PropertyIDs._TintColor, this.NormalColor);
            }
            else
            {
                this.NormalColor = this.NormalMaterial.GetColor(PropertyIDs._TintColor);
            }

            if (this.AntiNormalColor != this.ZeroColor)
            {
                this.AntiNormalMaterial.SetColor(PropertyIDs._TintColor, this.AntiNormalColor);
            }
            else
            {
                this.AntiNormalColor = this.AntiNormalMaterial.GetColor(PropertyIDs._TintColor);
            }

            if (this.BurnColor != this.ZeroColor)
            {
                this.BurnMaterial.SetColor(PropertyIDs._TintColor, this.BurnColor);
                this.BurnArrowMaterial.SetColor(PropertyIDs._TintColor, this.BurnColor);
            }
            else
            {
                this.BurnColor = this.BurnMaterial.GetColor(PropertyIDs._TintColor);
            }

            if (this.TargetColor != this.ZeroColor)
            {
                this.TargetMaterial.SetColor(PropertyIDs._TintColor, this.TargetColor);
            }
            else
            {
                this.TargetColor = this.TargetMaterial.GetColor(PropertyIDs._TintColor);
            }

            if (this.AntiTargetColor != this.ZeroColor)
            {
                this.AntiTargetMaterial.SetColor(PropertyIDs._TintColor, this.AntiTargetColor);
            }
            else
            {
                this.AntiTargetColor = this.AntiTargetMaterial.GetColor(PropertyIDs._TintColor);
            }

            if (HasNavWaypoint && this.NavWaypointMaterial != null)
            {
                if (this.NavWaypointColor != this.ZeroColor)
                {
                    this.NavWaypointMaterial.SetColor(PropertyIDs._TintColor, this.NavWaypointColor);
                }
                else
                {
                    this.NavWaypointColor = this.NavWaypointMaterial.GetColor(PropertyIDs._TintColor);
                }
            }
        }
    }
}
