using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using XNATools.Services.Interfaces;
using XNATools.Managers;

namespace XNATools.Services
{
    /// <summary>
    /// Not used for the moment
    /// </summary>
    public sealed class CameraService //: GameComponent//, ICameraService
    {
        /*Matrix projection;
        Matrix view;
        Matrix world;

        Vector3 cameraPosition;
        Vector3 cameraTarget = Vector3.Zero;
        Vector3 cameraUpVector = Vector3.Up;

        float horizontalRotation;
        float verticalRotation;
        Viewport viewPort;
        MouseState originalMouseState;

        const float rotationSpeed = 0.005f;

        public CameraService(Game game, Vector3 position)
            : base(game)
        {
            cameraPosition = position;
            //ServiceManager.Add<ICameraService>(this);
        }

        public override void Initialize()
        {
            this.horizontalRotation = 0;
            this.verticalRotation = 0;
            this.viewPort = Game.GraphicsDevice.Viewport;

            float viewAngle = MathHelper.PiOver4;
            float nearPlane = 0.5f;
            float farPlane = 1000.0f;

            Matrix.CreatePerspectiveFieldOfView(viewAngle, viewPort.AspectRatio, nearPlane, farPlane, out projection);
            UpdateViewMatrix();

#if !XBOX
            Mouse.SetPosition(viewPort.Width / 2, viewPort.Height / 2);
            originalMouseState = Mouse.GetState();
#endif
        }

        // Old Update
        //public override void Update(GameTime gameTime)
        //{
        //    IKeyboardService KBState = ServiceHelper.Get<IKeyboardService>();

        //    bool cameraUpdate = false;

        //    if (KBState.KeyPressed(Keys.Q))
        //    {
        //        cameraPosition.X -= 0.5f;
        //        cameraTarget.X -= 0.5f;
        //        cameraUpdate = true;
        //    }

        //    if (KBState.KeyPressed(Keys.D))
        //    {
        //        cameraPosition.X += 0.5f;
        //        cameraTarget.X += 0.5f;
        //        cameraUpdate = true;
        //    }

        //    if (KBState.KeyPressed(Keys.Z))
        //    {
        //        cameraPosition.Z -= 0.5f;
        //        cameraTarget.Z -= 0.5f;
        //        cameraUpdate = true;
        //    }

        //    if (KBState.KeyPressed(Keys.S))
        //    {
        //        cameraPosition.Z += 0.5f;
        //        cameraTarget.Z += 0.5f;
        //        cameraUpdate = true;
        //    }

        //    if (KBState.KeyPressed(Keys.PageUp))
        //    {
        //        cameraPosition.Y += 0.5f;
        //        cameraTarget.Y += 0.5f;
        //        cameraUpdate = true;
        //    }

        //    if (KBState.KeyPressed(Keys.PageDown))
        //    {
        //        cameraPosition.Y -= 0.5f;
        //        cameraTarget.Y -= 0.5f;
        //        cameraUpdate = true;
        //    }

        //    if (KBState.KeyPressed(Keys.NumPad8))
        //    {
        //        cameraTarget.Y += 0.5f;
        //        cameraUpdate = true;
        //    }

        //    if (KBState.KeyPressed(Keys.NumPad2))
        //    {
        //        cameraTarget.Y -= 0.5f;
        //        cameraUpdate = true;
        //    }




        //    //IMouseService mouse = ServiceHelper.Get<IMouseService>();

        //    //cameraTarget.X += mouse.GetOffsetX();
        //    //cameraTarget.Y -= mouse.GetOffsetY();
        //    //cameraUpdate = true;


        //    if (cameraUpdate)
        //        Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out view);

        //    base.Update(gameTime);
        //}
        
        
        public override void Update(GameTime gameTime)
        {
            IKeyboardService KBState = ServiceManager.Get<IKeyboardService>();
            IMouseService mouse = ServiceManager.Get<IMouseService>();

#if XBOX            
            horizontalRotation -= rotationSpeed * gamePadState.ThumbSticks.Left.X * 5.0f;
            verticalRotation += rotationSpeed * gamePadState.ThumbSticks.Left.Y * 5.0f;

            UpdateViewMatrix();

            float moveUp = gamePadState.Triggers.Right - gamePadState.Triggers.Left;
            AddToCameraPosition(new Vector3(gamePadState.ThumbSticks.Right.X, moveUp, -gamePadState.ThumbSticks.Right.Y));
#else

            horizontalRotation -= rotationSpeed * mouse.OffsetFromCenter.X;
            verticalRotation -= rotationSpeed * mouse.OffsetFromCenter.Y;
            mouse.Center();
            UpdateViewMatrix();

            if (KBState.IsKeyDown(Keys.Up) || KBState.IsKeyDown(Keys.W))      //Forward
                AddToCameraPosition(new Vector3(0, 0, -1));

            if (KBState.IsKeyDown(Keys.Down) || KBState.IsKeyDown(Keys.S))    //Backward
                AddToCameraPosition(Vector3.UnitZ);

            if (KBState.IsKeyDown(Keys.Right) || KBState.IsKeyDown(Keys.D))   //Right
                AddToCameraPosition(Vector3.UnitX);

            if (KBState.IsKeyDown(Keys.Left) || KBState.IsKeyDown(Keys.A))    //Left
                AddToCameraPosition(new Vector3(-1, 0, 0));

            if (KBState.IsKeyDown(Keys.Q))                                     //Up
                AddToCameraPosition(Vector3.UnitY);

            if (KBState.IsKeyDown(Keys.Z))                                     //Down
                AddToCameraPosition(new Vector3(0, -1, 0));
#endif
        }



        #region Work Properties

        Vector3 _cameraOriginalTarget = new Vector3(0, 0, -1);
        Vector3 _cameraOriginalUpVector = Vector3.Up;
        Vector3 _cameraRotatedTarget = new Vector3();
        Vector3 _cameraRotatedUpVector = new Vector3();
        Vector3 _cameraFinalTarget = new Vector3();

        #endregion

        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            float moveSpeed = 0.5f;

            Matrix cameraRotation = Matrix.CreateRotationX(verticalRotation) * Matrix.CreateRotationY(horizontalRotation);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            cameraPosition += moveSpeed * rotatedVector;
            UpdateViewMatrix();
        }

        private void UpdateViewMatrix()
        {
            Matrix cameraRotation = Matrix.CreateRotationX(verticalRotation) * Matrix.CreateRotationY(horizontalRotation);

            Vector3.Transform(ref _cameraOriginalTarget, ref cameraRotation, out _cameraRotatedTarget);
            Vector3.Transform(ref _cameraOriginalUpVector, ref cameraRotation, out _cameraRotatedUpVector);

            _cameraFinalTarget = cameraPosition + _cameraRotatedTarget;

            Matrix.CreateLookAt(ref cameraPosition, ref _cameraFinalTarget, ref _cameraRotatedUpVector, out view);
        }

        #region ICameraService Membres

        public Matrix Projection
        {
            get { return projection; }
        }

        public Matrix View
        {
            get { return view; }
        }

        public Matrix World
        {
            get { return world; }
        }

        #endregion*/
    }
}
