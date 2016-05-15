using UnityEngine;

namespace NavBallAdjustor
{
    /// <summary>
    /// The NavBall options toggle details.
    /// </summary>
    public class OptionsToggle
    {
        #region Private_Fields
        /// <summary>
        /// The "toggle off" texture.
        /// </summary>
        private static Texture2D OffTexture;

        /// <summary>
        /// The "toggle on" texture.
        /// </summary>
        private static Texture2D OnTexture;

        /// <summary>
        /// The "toggle push" texture.
        /// </summary>
        private static Texture2D PushTexture;

        /// <summary>
        /// The custom width.
        /// </summary>
        private float CustomWidth;

        /// <summary>
        /// The custom height.
        /// </summary>
        private float CustomHeight;
        #endregion

        #region Public_Fields
        /// <summary>
        /// The default toggle width.
        /// </summary>
        public const float DefaultWidth = 44f;

        /// <summary>
        /// The default toggle height.
        /// </summary>
        public const float DefaultHeight = 13f;

        /// <summary>
        /// The toggle rectangle.
        /// </summary>
        public Rect Rectangle;

        /// <summary>
        /// The toggle GUI style.
        /// </summary>
        public GUIStyle Style;
        #endregion

        #region Public_Properties
        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public float Left
        {
            get
            {
                return this.Rectangle.xMin;
            }
            set
            {
                this.Rectangle.xMin = value;
                this.Rectangle.width = this.CustomWidth;
            }
        }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        public float Top
        {
            get
            {
                return this.Rectangle.yMin;
            }
            set
            {
                this.Rectangle.yMin = value;
                this.Rectangle.height = this.CustomHeight;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public float Width
        {
            get
            {
                return this.Rectangle.width;
            }
            set
            {
                this.CustomWidth = value;
                this.Rectangle.width = value;
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public float Height
        {
            get
            {
                return this.Rectangle.height;
            }
            set
            {
                this.CustomHeight = value;
                this.Rectangle.height = value;
            }
        }

        /// <summary>
        /// Gets the width of the texture.
        /// </summary>
        public float OriginalTextureWidth
        {
            get
            {
                return OffTexture.width;
            }
        }

        /// <summary>
        /// Gets the height of the texture.
        /// </summary>
        public float OriginalTextureHeight
        {
            get
            {
                return OffTexture.height;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsToggle" /> class.
        /// </summary>
        public OptionsToggle()
        {
            OffTexture = GameDatabase.Instance.GetTexture(ModStrings.OptionsToggle.IconOffPath, false);
            OnTexture = GameDatabase.Instance.GetTexture(ModStrings.OptionsToggle.IconOnPath, false);
            PushTexture = GameDatabase.Instance.GetTexture(ModStrings.OptionsToggle.IconPushPath, false);
            
            this.Style = new GUIStyle();
            this.Style.normal.background = OffTexture;
            this.Style.hover.background = OnTexture;
            this.Style.active.background = PushTexture;
            this.Style.onNormal.background = OnTexture;
            this.Style.onActive.background = PushTexture;

            this.CustomWidth = this.CustomHeight = 1;
            this.Rectangle = new Rect(0, 0, this.CustomWidth, this.CustomHeight);
        }
        #endregion
    }
}
