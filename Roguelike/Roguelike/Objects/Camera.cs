using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
            }
        }

        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-(int)Position.X,
                    -(int)Position.Y, 0) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                    Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }

        public void AdjustZoom(float amount)
        {
            Zoom += amount;
            if (Zoom < .25f)
                Zoom = .25f;
        }

        public void MoveCamera(Vector2 cameraMovement, bool clampToMap = false)
        {
            var newPosition = Position + cameraMovement;

            Position = clampToMap ? MapClampedPosition(newPosition) : newPosition;
        }

        public Rectangle ViewportWorldBoundry()
        {
            var viewPortCorner = ScreenToWorld(new Vector2(0, 0));
            var viewPortBottomCorner = ScreenToWorld(new Vector2(ViewportWidth, ViewportHeight));

            return new Rectangle((int)viewPortCorner.X, (int)viewPortCorner.Y, (int)viewPortBottomCorner.X, (int)viewPortBottomCorner.Y);
        }

        public void CenterOn(Vector2 position)
        {
            Position = position;
        }

        public void CenterOn(RogueSharp.Cell cell)
        {
            Position = CenteredPosition(cell, true);
        }

        private Vector2 CenteredPosition( RogueSharp.Cell cell, bool clampToMap = false)
        {
            var cameraPosition = new Vector2(cell.X * Statics.spriteWidth, cell.Y * Statics.spriteHeight);
            var cameraCenteredOnTilePosition = new Vector2(cameraPosition.X + Statics.spriteWidth / 2, cameraPosition.Y + Statics.spriteHeight / 2);
            return clampToMap ? MapClampedPosition(cameraCenteredOnTilePosition) : cameraCenteredOnTilePosition;
        }

        private Vector2 MapClampedPosition (Vector2 position)
        {
            var CameraMax = new Vector2(Statics.mapWidth * Statics.spriteWidth - (ViewportWidth / Zoom / 2),
                Statics.mapHeight * Statics.spriteHeight - (ViewportHeight / Zoom / 2));

            return Vector2.Clamp(position, new Vector2(ViewportWidth / Zoom / 2, ViewportHeight / Zoom / 2), CameraMax);
        }

        public Vector2 WorldToScreen( Vector2 worldPosition )
        {
            return Vector2.Transform(worldPosition, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(TranslationMatrix));
        }

        public void Update(InputState inputState, PlayerIndex? controllingPlayer)
        {
            var cameraMovement = Vector2.Zero;

            if (inputState.IsScrollLeft(controllingPlayer))
                cameraMovement.X = -1;
            else if (inputState.IsScrollRight(controllingPlayer))
                cameraMovement.X = 1;
            if (inputState.IsScrollDown(controllingPlayer))
                cameraMovement.Y = 1;
            else if (inputState.IsScrollUp(controllingPlayer))
                cameraMovement.Y = -1;
            if (inputState.IsZoomIn(controllingPlayer))
                AdjustZoom(.25f);
            else if (inputState.IsZoomOut(controllingPlayer))
                AdjustZoom(-.25f);

            if(cameraMovement!= Vector2.Zero)
            {
                cameraMovement.Normalize();
            }

            cameraMovement *= 25f;

            MoveCamera(cameraMovement, true);
        }
    }
}
