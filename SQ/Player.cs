using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SQ
{

    public class Player : Sprite
    {
   

        #region PlayerVariables
        int MovementSpeed;

        int WidthGapBetweenFrame;
        int HeightGapBetweenFrame;
        Vector2 PositionOnGrid;
        Vector2 GridPositionOffset;
        bool Moving;
        #endregion

        #region PlayerSpecificFunctions
        //this is the constructor for the player
        public Player(Texture2D spriteTexture, Rectangle spritePOS, Rectangle sourceRect, int amountOfFrames, double timeBetweenFrames, int amountOfRows, int movementSpeed, int widthGapBetweenFrame, int heightGapBetweenFrame)
        {
            Moving = false;
            PositionOnGrid = new Vector2(1,1);
            MovementSpeed = movementSpeed;
            SpriteTexture = spriteTexture;
            SpritePOS = spritePOS;
            GridPositionOffset = new Vector2((SpritePOS.Width / 2) - 16, (SpritePOS.Height) - 16);
            SpritePOS.X = (int)PositionOnGrid.X * 32 - (int)GridPositionOffset.X;
            SpritePOS.Y = (int)PositionOnGrid.Y * 32 - (int)GridPositionOffset.Y;
            SourceRectangle = sourceRect;
            AmountOfFramesOnEachRow = amountOfFrames + 1;
            TimeBetweenFrames = timeBetweenFrames;
            AmountOfRows = amountOfRows;
            WidthGapBetweenFrame = widthGapBetweenFrame;
            HeightGapBetweenFrame = heightGapBetweenFrame;
        }
       
        public override void Animation(GameTime gameTime)
        {
            base.Animation(gameTime);

            ElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ElapsedTime >= TimeBetweenFrames){

                CurrentFrame += 1;
                

                if (CurrentFrame >= AmountOfFramesOnEachRow )
                    CurrentFrame = 1;
                SourceRectangle.X = (SourceRectangle.Width * CurrentFrame) + (WidthGapBetweenFrame * CurrentFrame);
                ElapsedTime -= TimeBetweenFrames;
            }
           
        }
        #endregion

        #region BaseFunctions(UPDATE,DRAW)
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState KB = Keyboard.GetState();
            if (KB.IsKeyDown(Keys.W) && Moving == false)
            {
                PositionOnGrid.Y -= 1;
                SourceRectangle.Y = SourceRectangle.Height;
                Moving = true;
                
            }
            if (KB.IsKeyDown(Keys.S) && Moving == false)
            {
                PositionOnGrid.Y += 1;
                SourceRectangle.Y = 0;
                Moving = true;
                
            }
            if (KB.IsKeyDown(Keys.A) && Moving == false)
            {
                PositionOnGrid.X -= 1;
                SourceRectangle.Y = SourceRectangle.Height * 2;
                Moving = true;

            }
            if(KB.IsKeyDown(Keys.D) && Moving == false)
            {
                PositionOnGrid.X += 1;
                SourceRectangle.Y = SourceRectangle.Height * 3;
                Moving = true;

            }

            if (Moving)
            {
                Animation(gameTime);
                
                if (SpritePOS.X > PositionOnGrid.X * 32 - GridPositionOffset.X )
                {
                    SpritePOS.X -= 2;
                }
                else if (SpritePOS.X < PositionOnGrid.X * 32 - GridPositionOffset.X)
                {
                    SpritePOS.X += 2;
                }
                else if (SpritePOS.Y < PositionOnGrid.Y * 32 - GridPositionOffset.Y)
                {
                    SpritePOS.Y += 2;
                }
                else if(SpritePOS.Y > PositionOnGrid.Y * 32 - GridPositionOffset.Y)
                {
                    SpritePOS.Y -= 2;
                }
                else
                {
                    Moving = false;
                }
                
            }
            else
            {
                SourceRectangle.X = 0;
            }

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(SpriteTexture, SpritePOS, SourceRectangle, Color.White);
        }
        #endregion
    }



}
