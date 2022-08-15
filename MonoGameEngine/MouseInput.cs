using GameRules;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameEngine;

public class MouseInput : IInput
{
    private readonly GraphicsDevice _graphicsDevice;
    private bool _leftButtonWasPressed;

    public MouseInput(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
    }

    public Spot? GetSpot()
    {
        if (Mouse.GetState().LeftButton == ButtonState.Pressed && !_leftButtonWasPressed)
        {
            _leftButtonWasPressed = true;
        }

        if (Mouse.GetState().LeftButton == ButtonState.Released && _leftButtonWasPressed)
        {
            _leftButtonWasPressed = false;
            if (!_graphicsDevice.Viewport.Bounds.Contains(Mouse.GetState().Position))
            {
                return null;
            }

            var x = Mouse.GetState().Position.X / (512 / 3);
            var y = Mouse.GetState().Position.Y / (512 / 3);

            return new Spot(x, y);
        }
        
        return null;
    }
}