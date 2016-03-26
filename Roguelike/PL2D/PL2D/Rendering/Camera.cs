using Microsoft.Xna.Framework;
using PL2D.Rendering.Textured_Objects;

namespace PL2D
{
    internal class Camera
    {
        public Camera()
        {
            Zoom = 1.0f;
        }

        #region Here Until Better Place Found

        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }
        public int WorldWidth { get; set; }
        public int WorldHeight { get; set; }

        #endregion Here Until Better Place Found

        public Vector2 Position { get; private set; }
        public float Zoom { get; private set; }
        public float Rotation { get; private set; }

        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }

        public Vector2 ViewportCenter => new Vector2(ViewportWidth* 0.5f, ViewportHeight* 0.5f);

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

        public void CenterOn(Cell cell)
        {
            Position = CenteredPosition(cell, true);
        }

        private Vector2 CenteredPosition(Cell cell, bool clampToMap = false)
        {
            var _cameraPosition = new Vector2(cell.X * SpriteWidth, cell.Y * SpriteHeight);
            var _cameraCenteredOnTilePosition = new Vector2(_cameraPosition.X + SpriteWidth / 2, _cameraPosition.Y + SpriteHeight / 2);
            return clampToMap ? MapClampedPosition(_cameraCenteredOnTilePosition) : _cameraCenteredOnTilePosition;
        }

        private Vector2 MapClampedPosition(Vector2 position)
        {
            var _cameraMax = new Vector2(WorldWidth * SpriteWidth - ViewportWidth / Zoom / 2,
                WorldHeight * SpriteHeight - ViewportHeight / Zoom / 2);

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
    }
}