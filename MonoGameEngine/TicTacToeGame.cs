using GameRules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game = Microsoft.Xna.Framework.Game;

namespace MonoGameEngine;

public class TicTacToeGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Texture2D _xTexture;
    private Texture2D _oTexture;
    private Texture2D _gridTexture;

    private GameSession _gameSession;

    public TicTacToeGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _gameSession = CreateGameSession();
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 512;
        _graphics.PreferredBackBufferHeight = 512;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _gridTexture = Content.Load<Texture2D>("tic-tac-toe-thick-grid");
        _xTexture = Content.Load<Texture2D>("tic-tac-toe-x");
        _oTexture = Content.Load<Texture2D>("tic-tac-toe-o");
        _font = Content.Load<SpriteFont>("Font");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_gameSession.GetState() == State.Ongoing)
        {
            _gameSession.MakeTurn();
        }

        if (Keyboard.GetState().IsKeyDown(Keys.R))
        {
            _gameSession = CreateGameSession();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_gridTexture, Vector2.Zero, Color.White);

        var board = _gameSession.GetBoard();
        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                switch (board[y, x])
                {
                    case 'x':
                        _spriteBatch.Draw(_xTexture, new Vector2(x * 170, y * 170), Color.White);
                        break;
                    case 'o':
                        _spriteBatch.Draw(_oTexture, new Vector2(x * 170, y * 170), Color.White);
                        break;
                }
            }
        }

        if (_gameSession.GetState() != State.Ongoing)
        {
            _spriteBatch.DrawString(_font, _gameSession.GetState().ToString(), new Vector2(200, 200), Color.White);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private GameSession CreateGameSession()
    {
        return new GameSession(
            // new MinimaxPlayer(),
            new RealPlayer(new MouseInput(GraphicsDevice)),
            new RealPlayer(new MouseInput(GraphicsDevice))
        );
    }
}