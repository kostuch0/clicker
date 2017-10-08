using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Clicker
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Clicker.Muzyka play = new Clicker.Muzyka();
        SpriteBatch spriteBatch, hpbar, menu, pokazpotwor;
        Texture2D tlo, texhud, hp, hp1, potwor, ostatnipotwor;
        Vector2 vecpos, textpos, nazwapotworapos, hppotworapos;
        SpriteFont Font1, nazwapotworafont;
        KeyboardState keystate, lastKeyState;
        MouseState mouse, currentMouseState, lastMouseState;
        Random rnd = new Random();
        Song song;

        Rectangle rect;
        string[,] potwory = new string[,] { {"mob_1","mob_2", "anime_girl", "anime_girl2", "anime_girl3", "anime_girl4", "anime_girl5" },
            {"Lodowy golem","Wkurwiony golem","to","rak", "XDD", "chuj","sparky" } };
        string[] stages = new string[] { "tlo", "tlo_1" };
        int x = 0, y = 0, ty = 0, hpx = 0, hpy = 0, killed = 0, mousex = 0, mousey = 0, stage = 0, totalkilled = 0, chosenmob = 0;
        double current = 4, total = 5, dmg = 10f, mnoznik = 0.25, money = 0;
        double frameRate = 0.0;
        bool addingx = true, addingy = true, napis = true, menub = false, test;
        string text;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 1024;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Window.Title = "Jakiś clicker";
            this.IsMouseVisible = true;
            textpos = new Vector2(0, 0);
        }
        private void click()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            mousex = currentMouseState.X;
            mousey = currentMouseState.Y;
            if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (rect.Contains(mousex, mousey))
                {
                    zadajobrazenia(true);
                    //System.Windows.Forms.MessageBox.Show(mousex.ToString()+" "+mousey.ToString());
                }
            }
        }
        private void otworzmenu()
        {
            if (lastKeyState.IsKeyUp(Keys.F11) && keystate.IsKeyDown(Keys.F11))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
            }
        }
        private void nowymob()
        {
            //if(tlo.Height-10< GraphicsDevice.Viewport.Height/2 && tlo.Width-10 < GraphicsDevice.Viewport.Width / 2) { rect = new Rectangle(GraphicsDevice.Viewport.Width/2 - potwor.Width/2, GraphicsDevice.Viewport.Height - potwor.Height, potwor.Width, potwor.Height); } else
            //{
            //    rect = new Rectangle(GraphicsDevice.Viewport.Width - potwor.Width, GraphicsDevice.Viewport.Height - potwor.Height, potwor.Width, potwor.Height);
            //}

            if (current <= 0)
            {
                current = 0;
                total = total * 1.05;
                current = total;
                killed++;
                totalkilled++;
                money += rnd.Next(stage, (stage + killed));

                if (killed == 9)
                {
                    killed = 0;
                    stage++;
                    if (stage > stages.Length - 1) { tlo = Content.Load<Texture2D>(stages[stages.Length - 1]); } else { tlo = Content.Load<Texture2D>(stages[stage]); }

                }
                ostatnipotwor = potwor;
                mnoznik += (mnoznik * 0.01);
                while (ostatnipotwor == potwor)
                {
                    chosenmob = rnd.Next(0, potwory.GetLength(1));
                    potwor = Content.Load<Texture2D>(potwory[0, chosenmob]);
                    if (potwor.Height > GraphicsDevice.Viewport.Height)
                    {
                        rect = new Rectangle(GraphicsDevice.Viewport.Width - potwor.Width, 0, potwor.Width, potwor.Height);
                    }
                    else if (potwor.Height == GraphicsDevice.Viewport.Height)
                    {
                        rect = new Rectangle(GraphicsDevice.Viewport.Width / 2 - potwor.Width / 2, GraphicsDevice.Viewport.Height / 2 - potwor.Height / 2, potwor.Width, potwor.Height);
                    }
                    else { rect = new Rectangle(GraphicsDevice.Viewport.Width - potwor.Width, GraphicsDevice.Viewport.Height - potwor.Height, potwor.Width, potwor.Height); }

                }

            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            vecpos = new Vector2(0, 0);
            text = "Wkurwiles golema.";
            hpx = (GraphicsDevice.Viewport.Width / 2) - 250;
            hpy = (GraphicsDevice.Viewport.Height) - hp1.Height;
            nazwapotworapos = new Vector2(hpx, hpy);
            hppotworapos = new Vector2(hpx + 400, hpy);
            rect = new Rectangle(GraphicsDevice.Viewport.Width - (potwor.Width + potwor.Width / 4), (GraphicsDevice.Viewport.Height - potwor.Height) - 200, potwor.Width, potwor.Height);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pokazpotwor = new SpriteBatch(GraphicsDevice);
            hpbar = new SpriteBatch(GraphicsDevice);
            menu = new SpriteBatch(GraphicsDevice);
            potwor = Content.Load<Texture2D>("mob_1");
            tlo = Content.Load<Texture2D>("tlo");
            Font1 = Content.Load<SpriteFont>("SpriteFont1");
            nazwapotworafont = Content.Load<SpriteFont>("SpriteFont2");
            texhud = Content.Load<Texture2D>("hp");
            hp = Content.Load<Texture2D>("hp");
            hp1 = Content.Load<Texture2D>("hp_1");
            song = Content.Load<Song>("zbuku");
            play.graj(song);

            // TODO: use this.Content to load your game content here
        }
        public int procent(double total, double current)
        {
            return (int)((current / total) * 100);
        }
        private void fullhd()
        {
            if (x < GraphicsDevice.Viewport.Width - 226 && addingx == true)
            {
                x += 2;
                if (x >= GraphicsDevice.Viewport.Width - 226) { addingx = false; }
            }
            if (y < GraphicsDevice.Viewport.Height - 191 && addingy == true)
            {
                y += 2;
                if (y >= GraphicsDevice.Viewport.Height - 191) { addingy = false; }
            }
            if (x > 0 && addingx == false)
            {
                x -= 2;
                if (x == 0) { addingx = true; }
            }
            if (y > 0 && addingy == false)
            {
                y -= 2;
                if (y == 0) { addingy = true; }
            }
        }
        private void updatenapis()
        {
            if (ty < GraphicsDevice.Viewport.Height - 48 && napis == true)
            {
                ty += 10;
                textpos = new Vector2(0, ty);
                if (ty >= GraphicsDevice.Viewport.Height - 48) { napis = false; }
            }
            if (ty > 0 && napis == false)
            {
                ty -= 10;
                textpos = new Vector2(0, ty);
                if (ty <= 0) { napis = true; }
            }



        }
        private void zadajobrazenia(bool x)
        {
            switch (x)
            {
                case false:
                    current -= (dmg * mnoznik) / 500;
                    break;
                case true:
                    current -= dmg * mnoznik;
                    break;

            }
        }
        private void poruszanie()
        {
            if (keystate.IsKeyDown(Keys.W))
            {
                y -= 10;
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                y += 10;
            }
            if (keystate.IsKeyDown(Keys.A))
            {
                x -= 10;
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                x += 10;
            }
            if (keystate.IsKeyDown(Keys.F12))
            {
                menub = true;
            }
            if (keystate.IsKeyUp(Keys.F12))
            {
                menub = false;
            }
        }



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //fullhd();
            updatenapis();
            nowymob();
            // TODO: Add your update logic here
            lastKeyState = keystate;
            keystate = Keyboard.GetState();
            poruszanie();
            zadajobrazenia(false);
            click();
            otworzmenu();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            pokazpotwor.Begin();
            spriteBatch.Draw(tlo, new Rectangle(0, 0, 1280, 1024), new Rectangle(0, 0, tlo.Width, tlo.Height), Color.AntiqueWhite, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            //spriteBatch.DrawString(Font1, text, textpos, new Color(rnd.Next(0,255), rnd.Next(0, 255), rnd.Next(0, 255)));
            spriteBatch.DrawString(Font1, money.ToString(), new Vector2(500, 10), Color.Gold);
            //spriteBatch.DrawString(Font1, ((int)frameRate).ToString(), new Vector2(GraphicsDevice.Viewport.Width-100, 10), Color.Gold);
            if (test) { spriteBatch.DrawString(Font1, "Stage: " + stage.ToString(), new Vector2(10, 50), Color.Gold); }

            pokazpotwor.Draw(potwor, rect, Color.LightCyan);
            spriteBatch.Draw(texhud, new Rectangle(x, y, 390, GraphicsDevice.Viewport.Height), Color.Black);
            spriteBatch.End();


            pokazpotwor.End();

            hpbar.Begin();
            hpbar.Draw(hp, new Rectangle(hpx, hpy, 500, hp.Height), Color.Black * 0.5f);
            hpbar.Draw(hp1, new Rectangle(hpx + 1, hpy + 1, ((500 * procent(total, current)) / 100) - 1, hp.Height - 1), Color.White);
            hpbar.DrawString(nazwapotworafont, potwory[1, chosenmob], nazwapotworapos, Color.White * 0.5f);
            hpbar.DrawString(nazwapotworafont, ((int)current).ToString() + "/" + ((int)total).ToString(), hppotworapos, Color.Green);
            hpbar.End();
            if (menub)
            {

                menu.Begin();
                menu.Draw(hp, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Black);
                menu.DrawString(Font1, "dmg =" + (dmg * mnoznik).ToString(), new Vector2(0, 0), Color.Red);
                menu.DrawString(Font1, "dmg/s =" + ((dmg * mnoznik) / 500).ToString(), new Vector2(0, 60), Color.Red);
                menu.DrawString(Font1, "stage =" + stage.ToString(), new Vector2(0, 120), Color.Red);
                menu.DrawString(Font1, "dmg =" + (dmg * mnoznik).ToString(), new Vector2(0, 0), Color.Red);
                menu.DrawString(Font1, "killed =" + killed.ToString(), new Vector2(0, 180), Color.Red);
                menu.DrawString(Font1, "total killed =" + totalkilled.ToString(), new Vector2(0, 240), Color.Red);
                menu.End();
            }


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
