﻿using LetsCreatePokemon.Battle.Phases;
using LetsCreatePokemon.Services.Content;
using LetsCreatePokemon.Services.Screens;
using LetsCreatePokemon.Services.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LetsCreatePokemon.Screens
{
    internal class ScreenBattle : Screen
    {
        private IContentLoader contentLoader;
        private IPhase currentPhase;
        private WindowBattle windowBattle;
        private Texture2D backgroundTexture; 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ScreenBattle(IScreenLoader screenLoader, IPhase startPhase) : base(screenLoader)
        {
            currentPhase = startPhase;
            windowBattle = new WindowBattle(new Vector2(0, 113), 240, 45);
        }

        public override void LoadContent(IContentLoader contentLoader)
        {
            backgroundTexture = contentLoader.LoadTexture("Battle/Backgrounds/background");
            windowBattle.LoadContent(contentLoader);
            currentPhase.LoadContent(contentLoader);
            this.contentLoader = contentLoader;
        }

        public override void Update(double gameTime)
        {
            currentPhase.Update(gameTime);
            if (currentPhase.IsDone)
            {
                currentPhase = currentPhase.GetNextPhase();
                currentPhase.LoadContent(contentLoader);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Vector2(0), Color.White);
            windowBattle.Draw(spriteBatch);
            currentPhase.Draw(spriteBatch);
        }
    }
}