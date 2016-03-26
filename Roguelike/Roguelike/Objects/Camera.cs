using Microsoft.Xna.Framework;

namespace Roguelike
{
    public class Camera
    {
        public Camera()
        {
            Zoom = 1.0f;
        }

        public Vector2 Position { get; private set; }
        public float Zoom { get; private set; }
        public float Rotation { get; private set; }

        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }

        public Vector2 ViewportCenter => new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);

        public Matrix TranslationMatrix => 
            Matrix.CreateTranslation(-(int)Position.X,
                -(int)Position.Y, 0) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));

        public void AdjustZoom(float amount)
        {
            Zoom += amount;
            if (Zoom < .25f)
                Zoom = .25f;
        }

        public void MoveCamera(Vector2 cameraMovement, bool clampToMap = false)
        {
            var _newPosition = Position + cameraMovement;

            Position = clampToMap ? MapClampedPosition(_newPosition) : _newPosition;
        }

        public Rectangle ViewportWorldBoundry()
        {
            var _viewPortCorner = ScreenToWorld(new Vector2(0, 0));
            var _viewPortBottomCorner = ScreenToWorld(new Vector2(ViewportWidth, ViewportHeight));

            return new Rectangle((int)_viewPortCorner.X, (int)_viewPortCorner.Y, (int)_viewPortBottomCorner.X, (int)_viewPortBottomCorner.Y);
        }

        public void CenterOn(Vector2 position)
        {
            Position = position;
        }

        public void CenterOn(RogueSharp.Cell cell)
        {
            Position = CenteredPosition(cell, true);
        }

        private Vector2 CenteredPosition(RogueSharp.Cell cell, bool clampToMap = false)
        {
            var _cameraPosition = new Vector2(cell.X * Statics.SpriteWidth, cell.Y * Statics.SpriteHeight);
            var _cameraCenteredOnTilePosition = new Vector2(_cameraPosition.X + Statics.SpriteWidth / 2, _cameraPosition.Y + Statics.SpriteHeight / 2);
            return clampToMap ? MapClampedPosition(_cameraCenteredOnTilePosition) : _cameraCenteredOnTilePosition;
        }

        private Vector2 MapClampedPosition(Vector2 position)
        {
            var _cameraMax = new Vector2(Statics.MapWidth * Statics.SpriteWidth - ViewportWidth / Zoom / 2,
                Statics.MapHeight * Statics.SpriteHeight - ViewportHeight / Zoom / 2);

            return Vector2.Clamp(position, new Vector2(ViewportWidth / Zoom / 2, ViewportHeight / Zoom / 2), _cameraMax);
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(TranslationMatrix));
        }

        public void Update(InputState inputState, PlayerIndex? controllingPlayer)
        {
            var _cameraMovement = Vector2.Zero;

            if (inputState.IsScrollLeft(controllingPlayer))
                _cameraMovement.X = -1;
            else if (inputState.IsScrollRight(controllingPlayer))
                _cameraMovement.X = 1;
            if (inputState.IsScrollDown(controllingPlayer))
                _cameraMovement.Y = 1;
            else if (inputState.IsScrollUp(controllingPlayer))
                _cameraMovement.Y = -1;
            if (inputState.IsZoomIn(controllingPlayer))
                AdjustZoom(.25f);
            else if (inputState.IsZoomOut(controllingPlayer))
                AdjustZoom(-.25f);

            if (_cameraMovement != Vector2.Zero)
            {
                _cameraMovement.Normalize();
            }

            _cameraMovement *= 25f;

            MoveCamera(_cameraMovement, true);
        }
    }
}