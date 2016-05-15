using KSP.UI.Screens.Flight;
using UnityEngine;

namespace NavBallAdjustor
{
    /// <summary>
    /// Provides NavBall screen properties.
    /// </summary>
    public class NavBallHelper
    {
        /// <summary>
        /// The NavBall transform height with x1 scale.
        /// </summary>
        private const float TransformHeight = 260f;

        /// <summary>
        /// The NavBall texture height ratio.
        /// Is TextureHeight / TransformHeight
        /// </summary>
        private const float TextureHeightRatio = 0.911538461f;

        /// <summary>
        /// The NavBall texture height with x1 scale.
        /// </summary>
        public const float TextureHeight = 237f;

        /// <summary>
        /// The NavBall texture width/2 with x1 scale.
        /// </summary>
        public const float TextureHalfWidth = 143f;

        /// <summary>
        /// The NavBall.
        /// </summary>
        public NavBall NavBall;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavBallHelper"/> class.
        /// </summary>
        public NavBallHelper()
        {
            this.NavBall = GameObject.FindObjectOfType<NavBall>();
        }

        /// <summary>
        /// Gets the NavBall scale.
        /// </summary>
        public Vector3 Scale
        {
            get
            {
                return FlightUIModeController.Instance.navBall.panelTransform.localScale;
            }
        }

        /// <summary>
        /// Gets the NavBall position.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return FlightUIModeController.Instance.navBall.panelTransform.position;
            }
        }

        /// <summary>
        /// Gets the current height of the NavBall texture.
        /// </summary>
        public float TextureCurrentHeight
        {
            get
            {
                return TransformHeight * this.Scale.y * GameSettings.UI_SCALE * TextureHeightRatio;
            }
        }

        /// <summary>
        /// Gets the NavBall transform center X on screen.
        /// </summary>
        public float TransformScreenCenterX
        {
            get
            {
                return GameSettings.SCREEN_RESOLUTION_WIDTH * 0.5f + this.NavBall.transform.position.x;
            }
        }

        /// <summary>
        /// Gets the NavBall transform center Y on screen.
        /// </summary>
        public float TransformScreenCenterY
        {
            get
            {
                return GameSettings.SCREEN_RESOLUTION_HEIGHT * 0.5f - this.NavBall.transform.position.y;
            }
        }

        /// <summary>
        /// Gets the NavBall texture bottom Y on screen.
        /// </summary>
        public float TextureBottomScreenY
        {
            get
            {
                return this.TransformScreenCenterY + (TransformHeight * 0.5f * this.Scale.y * GameSettings.UI_SCALE);
            }
        }

        /// <summary>
        /// Gets the NavBall texture top Y on screen.
        /// </summary>
        public float TextureTopScreenY
        {
            get
            {
                return this.TextureBottomScreenY - this.TextureCurrentHeight;
            }
        }
    }
}
