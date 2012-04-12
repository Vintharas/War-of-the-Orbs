using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Worbs.Entities
{
    class Font : DrawableGameComponent
    {
        SpriteFont font;
        SpriteBatch spriteBatch;

        public Font(Game game, String text, Vector2 position, Color color) : base(game)
        {
            this.Text = text;
            this.Position = position;
            this.Color = color;
        }

        public String Text { get; set; }

        public Vector2 Position { get; set; }

        public Color Color { get; set; }

        public Rectangle GetRectangle()
        {
            Vector2 WidthAndHeight = font.MeasureString(Text);
            return new Rectangle((int)Position.X, (int)Position.Y, 
                                 (int)WidthAndHeight.X, (int)WidthAndHeight.Y);

        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            font = Game.Content.Load<SpriteFont>("Fonts/MenuItem");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(font, this.Text, this.Position, this.Color);

            base.Draw(gameTime);
        }
    }
}
