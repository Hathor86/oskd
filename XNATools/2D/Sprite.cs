using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XNATools.Managers;
using XNATools.Enums;
using XNATools.Services;
using XNATools.Services.Interfaces;
using XNATools.Exceptions;

namespace XNATools.Engine2D
{   
    /// <summary>
    /// Class for a Sprite
    /// </summary>
    public class Sprite : Microsoft.Xna.Framework.DrawableGameComponent
    {        
        #region Fields

        private const int _UpperBound = 0;
        private const int _LeftBound = 0; 
        /// <summary>
        /// Texture's delfault color.
        /// Sets to Color.White
        /// </summary>
        public static readonly Color DefaultColor = Color.White;

        private static SpriteBatch _SpriteBatch;
        //private static int _RightBound;
        //private static int _LowerBound;

        private bool _Initialized = false;
        private string _AssetName;
        private int _DrawOrder;
        private Texture2D _Texture;
        private Rectangle _SourceRectangle = Rectangle.Empty;
        private Vector2 _Position = Vector2.Zero;
        private Vector3 _Position3 = Vector3.Zero;
        private Rectangle _DefaultBounds;
        private Rectangle _TransformedBounds = Rectangle.Empty;
        private Vector2 _LeftTop = Vector2.Zero;
        private Vector2 _RightTop = Vector2.Zero;
        private Vector2 _LeftBottom = Vector2.Zero;
        private Vector2 _RightBottom = Vector2.Zero;
        private Vector2 _Scale = Vector2.One;
        private Vector3 _Scale3 = Vector3.One;
        private Vector2 _Center = Vector2.Zero;
        private Vector3 _Center3 = Vector3.Zero;
        private Matrix _Transform = Matrix.Identity;
        private float _Rotation = 0;
        private Vector2 _RotationCenter = Vector2.Zero;
        private Vector3 _RotationCenter3 = Vector3.Zero;
        private Layer _Layer;
        private Vector2 _BaseSize;
        private bool _StoreTextureInCache = false;

        /// <summary>
        /// Texture's color
        /// </summary>
        public Color Color = DefaultColor;
        /// <summary>
        /// Texture's effect
        /// </summary>
        public SpriteEffects Effect = SpriteEffects.None;

        #endregion        

        #region cTor(s)

        /// <summary>
        /// New Sprite
        /// </summary>
        /// <param name="game">Game who use the sprite</param>
        /// <param name="assetName">Texture name</param>
        /// <param name="layer">Layer where draw the sprite</param>
        /// <param name="drawOrder">Draw order in layer</param>  
        /// <param name="storeTextureInCache">Determine if texture must be stored in cache</param>
        public Sprite(Game game, string assetName, Layer layer, int drawOrder, bool storeTextureInCache)
            : base(game)
        {
            _AssetName = assetName;
            _Layer = layer;
            _DrawOrder = drawOrder;
            _StoreTextureInCache = storeTextureInCache;
            DrawOrder = (int)layer + drawOrder;
            if (_SpriteBatch == null)
            {
                _SpriteBatch = ServiceManager.Get<SpriteBatch>();
            }
           
            game.Components.Add(this);
        }

        /// <summary>
        /// New Sprite
        /// </summary>
        /// <param name="game">Game who use the sprite</param>
        /// <param name="assetName">Texture name</param>
        /// <param name="layer">Layer where draw the sprite</param>
        /// <param name="drawOrder">Draw order in layer</param>       
        public Sprite(Game game, string assetName, Layer layer, int drawOrder)
            : this(game, assetName, layer, drawOrder, true)
        { }

        /// <summary>
        /// New Sprite
        /// </summary>
        /// <param name="game">Game who use the sprite</param>
        /// <param name="assetName">Texture name</param>
        /// <param name="layer">Layer where draw the sprite</param>
        public Sprite(Game game, string assetName, Layer layer)
            : this(game, assetName, layer, 0, true)
        { }


        #endregion

        #region Methods

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.GameComponent.Initialize"/>
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            _DefaultBounds = new Rectangle(0, 0, _Texture.Width, _Texture.Height);
            _BaseSize = new Vector2(_DefaultBounds.Width, _DefaultBounds.Height);
            _SourceRectangle = new Rectangle(0, 0, _Texture.Width, _Texture.Height);
            GetCenter();
         
            _Initialized = true;
        }

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.DrawableGameComponent.LoadContent"/>
        /// </summary>
        protected override void LoadContent()
        {
            LoadContent(_StoreTextureInCache);
            base.LoadContent();                  
        }

        /// <summary>
        /// Load Sprite's content
        /// </summary>        
        /// <param name="useCache">Define if cached must be used</param>
        private void LoadContent(bool useCache)
        {
            if (useCache)
            {
                ICacheManagerService<Texture2D> service = ServiceManager.Get<ICacheManagerService<Texture2D>>();
                if (service != null)
                {
                    _Texture = service[_AssetName];
                }
                else
                {
                    _Texture = Game.Content.Load<Texture2D>(_AssetName);
                }
            }
            else
            {
                _Texture = Game.Content.Load<Texture2D>(_AssetName);
            }           
        }

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.GameComponent.Update"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            GetTransform();
            TransformBounds();
            base.Update(gameTime);
        }

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.DrawableGameComponent.Draw"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _SpriteBatch.Draw(_Texture, _Position, _SourceRectangle, Color, _Rotation, _RotationCenter, _Scale, Effect, 0);
            //Texture2D debugTex = Game.Content.Load<Texture2D>("debug");
            //_SpriteBatch.Draw(debugTex, Bounds, DefaultColor);            

            base.Draw(gameTime);
        }

        /// <summary>
        /// Get the transformation applied
        /// </summary>
        private void GetTransform()
        {
            //Get tranformation applied            
            _Transform = Matrix.CreateTranslation(-_RotationCenter3) * Matrix.CreateRotationZ(_Rotation) * Matrix.CreateScale(_Scale3) * Matrix.CreateTranslation(_Position3);
        }

        /// <summary>
        /// Sets the center of the sprite
        /// </summary>
        private void GetCenter()
        {
            _Center = new Vector2(_SourceRectangle.Width * _Scale.X / 2, _SourceRectangle.Height * _Scale.Y / 2);
            if (_Center.X == 0)
                _Center.X = 1;
            if (_Center.Y == 0)
                _Center.Y = 1;

            _Center3.X = _Center.X;
            _Center3.Y = _Center.Y;
        }       

        /// <summary>
        /// Update the rectangle that holdings sprite's bounds
        /// </summary>
        private void UpdateBounds()
        {
            float f = _SourceRectangle.Width * _Scale.X;
            f = (float)Math.Round(f, 0);            
            _DefaultBounds.Width = f == 0 ? 1 : (int)f;
            f = _SourceRectangle.Height * _Scale.Y;
            f = (float)Math.Round(f, 0);
            _DefaultBounds.Height = f == 0 ? 1 : (int)f;
            ForceTransformBounds();
            GetCenter();
        }

        /// <summary>
        /// Update bounds when rotate the sprite
        /// </summary>
        private void TransformBounds()
        {            
            //Initialize bounds
            _TransformedBounds.X = 0;
            _TransformedBounds.Y = 0;
            _TransformedBounds.Width = _SourceRectangle.Width;
            _TransformedBounds.Height = _SourceRectangle.Height;

            // Get all four corners in local space 
            _LeftTop.X = _TransformedBounds.Left;
            _LeftTop.Y = _TransformedBounds.Top;
            _RightTop.X = _TransformedBounds.Right;
            _RightTop.Y = _TransformedBounds.Top;
            _LeftBottom.X = _TransformedBounds.Left;
            _LeftBottom.Y = _TransformedBounds.Bottom;
            _RightBottom.X = _TransformedBounds.Right;
            _RightBottom.Y = _TransformedBounds.Bottom;

            // Transform all four corners into work space
            Vector2.Transform(ref _LeftTop, ref _Transform, out _LeftTop);
            Vector2.Transform(ref _RightTop, ref _Transform, out _RightTop);
            Vector2.Transform(ref _LeftBottom, ref _Transform, out _LeftBottom);
            Vector2.Transform(ref _RightBottom, ref _Transform, out _RightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(_LeftTop, _RightTop), Vector2.Min(_LeftBottom, _RightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(_LeftTop, _RightTop), Vector2.Max(_LeftBottom, _RightBottom));

            //Finally sets correct data  
            _TransformedBounds.X = (int)min.X;
            _TransformedBounds.Y = (int)min.Y;
            _TransformedBounds.Width = (int)(max.X - min.X);
            _TransformedBounds.Height = (int)(max.Y - min.Y);
        }

        /// <summary>
        /// Returns a bool that define if sprite is out of the bound(Screen)
        /// <remarks>Doesn't care of texture width by default</remarks>
        /// </summary>
        /// <returns>True if Sprite exits of the bounds; otherwise, false</returns>
        /*protected bool IsOutOfBounds(out OutBorder outBorder)
        {
            return IsOutOfBounds(out outBorder, OutOfBoundsBehaviour.None);
        }*/

        /// <summary>
        /// Returns a bool that define if sprites is out of the bounds(GameWindow)
        /// </summary>
        /// <param name="outBorder">Border where Sprite is out</param>
        /// <param name="behaviour">Define behaviour with texture</param>        
        /// <returns>True if Sprite exits of the bounds; otherwise, false</returns>
        /*protected bool IsOutOfBounds(out OutBorder outBorder, OutOfBoundsBehaviour behaviour)
        {
            bool upTest;
            bool downTest;
            bool leftTest;
            bool rightTest;
            outBorder = OutBorder.None;

            switch (behaviour)
            {
                case OutOfBoundsBehaviour.TextureHitsBounds:
                    leftTest = _TransformedBounds.Left < _LeftBound;
                    rightTest = _TransformedBounds.Right > _RightBound;
                    upTest = _TransformedBounds.Top < _UpperBound;
                    downTest = _TransformedBounds.Bottom > _LowerBound;
                    break;

                case OutOfBoundsBehaviour.TextureIsHidden:
                    leftTest = _TransformedBounds.Right < _LeftBound;
                    rightTest = _TransformedBounds.Left > _RightBound;
                    upTest = _TransformedBounds.Bottom < _UpperBound;
                    downTest = _TransformedBounds.Top > _LowerBound;
                    break;

                case OutOfBoundsBehaviour.None:
                default:
                    leftTest = _Position.X < _LeftBound;
                    rightTest = _Position.X > _RightBound;
                    upTest = _Position.Y < _UpperBound;
                    downTest = _Position.Y > _LowerBound;
                    break;
            }

            if (leftTest)
                outBorder = OutBorder.Left;
            if (rightTest)
                outBorder = OutBorder.Right;
            if (upTest)
                outBorder = OutBorder.Up;
            if (downTest)
                outBorder = OutBorder.Down;

            return leftTest || rightTest || upTest || downTest;
        }*/

        /// <summary>
        /// Force bounds tranformation
        /// <remarks>Necessary if sets properties before update. Use with caution</remarks>
        /// </summary>
        protected void ForceTransformBounds()
        {
            GetTransform();
            TransformBounds();
        }      

        /// <summary>
        /// Determine whether a specied sprite intersects another.
        /// </summary>
        /// <param name="other">The sprite to evaluate</param>
        /// <returns>True if intersection found; otherwise, false</returns>
        public bool Intersects(Sprite other)
        {
            return this._TransformedBounds.Intersects(other._TransformedBounds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets (or sets) texture's internal asset name
        /// </summary>
        public string AssetName
        {
            get
            {
                return _AssetName;
            }
            protected set
            {
                if (_Initialized)
                {
                    throw new Exception("Cannot change asset name after LoadContent has been called");
                }
                else
                {
                    _AssetName = value;
                }
            }
        }

        /// <summary>
        /// Gets or Sets Position
        /// </summary>
        public Vector2 Position
        {
            get
            {                
                return _Position;
            }

            set
            {
                if (_Initialized)
                {
                    _Position = value;
                    _Position3.X = value.X;
                    _Position3.Y = value.Y;
                }
                else
                    throw new CallBeforeLoadContentException("Position");
            }
        }

        /// <summary>
        /// Gets or sets sprite scale
        /// <remarks>
        /// If sets before update, be sure that transormation is applied.
        /// Scale must always be the last properties changed
        /// </remarks>
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                return _Scale;
            }
            protected set
            {
                if (_Initialized)
                {
                    _Scale = value;
                    _Scale3.X = value.X;
                    _Scale3.Y = value.Y;
                    UpdateBounds();                    
                }
                else
                    throw new CallBeforeLoadContentException("Scale");
            }
        }

        /// <summary>
        /// Gets or sets source from texture to display
        /// </summary>
        public Rectangle Source
        {
            get
            {
                return _SourceRectangle;
            }

            protected set
            {
                if (_Initialized)
                {
                    _SourceRectangle = value;                    
                    UpdateBounds();
                }
                else
                    throw new CallBeforeLoadContentException("Source");

            }
        }

        /// <summary>
        /// Gets Texture
        /// </summary>
        public Texture2D Texture
        {
            get
            {
                return _Texture;
            }
            protected set
            {
                _Texture = value;               
            }
        }        

        /// <summary>
        /// Gets Sprite center
        /// </summary>
        public Vector2 Center
        {
            get
            {                
                return _Center;
            }
        }

        /// <summary>
        /// Gets or sets rotation angle
        /// </summary>
        public float Rotation
        {
            get
            {
                return _Rotation;
            }
            protected set
            {
                _Rotation = value;
            }
        }

        /// <summary>
        /// Gets or sets rotation center
        /// </summary>
        public Vector2 RotationCenter
        {
            get
            {
                return _RotationCenter;
            }
            protected set
            {
                _RotationCenter = value;
                _RotationCenter3.X = value.X;
                _RotationCenter3.Y = value.Y; 
            }
        }

        /// <summary>
        /// Gets transformation matrix
        /// </summary>
        public Matrix TransformationMatrix
        {
            get
            {
                return _Transform;
            }
        }

        /// <summary>
        /// Gets a rectangle containing the sprite
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return _TransformedBounds;
            }
        }


        /// <summary>
        /// Gets UpperBound
        /// </summary>
        public int UpperBound
        {
            get
            {
                return _UpperBound;
            }
        }

        /// <summary>
        /// Gets LowerBound
        /// </summary>
        /*public int LowerBound
        {
            get
            {
                return _LowerBound;
            }
        }

        /// <summary>
        /// Gets RightBound
        /// </summary>
        public int RightBound
        {
            get
            {
                return _RightBound;
            }
        }*/

        /// <summary>
        /// Gets LeftBound
        /// </summary>
        public int LeftBound
        {
            get
            {
                return _LeftBound;
            }
        }

        /// <summary>
        /// Gets layer
        /// </summary>
        public Layer Layer
        {
            get
            {
                return _Layer;
            }
        }

        /// <summary>
        /// Gets draw order in layer
        /// </summary>
        public int DrawOrderInLayer
        {
            get
            {
                return _DrawOrder;
            }
        }

        /// <summary>
        /// Gets Position as Vector3
        /// </summary>
        internal Vector3 Position3
        {
            get
            {
                return _Position3;
            }
        }

        /// <summary>
        /// Gets Center as Vector3
        /// </summary>
        internal Vector3 Center3
        {
            get
            {
                return _Center3;
            }
        }

        /// <summary>
        /// Gets Scale as Vector3
        /// </summary>
        internal Vector3 Scale3
        {
            get
            {
                return _Scale3;
            }
        }

        /// <summary>
        /// Gets rotation center as vector3
        /// </summary>
        internal Vector3 RotationCenter3
        {
            get
            {
                return _RotationCenter3;
            }
        }

        #endregion
    }
}