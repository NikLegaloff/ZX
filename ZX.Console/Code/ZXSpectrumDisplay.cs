namespace ZX.Console.Code;
using SFML.Graphics;
using SFML.System;

using SFML.Window;
using Window = SFML.Window.Window;
public class ZXSpectrumDisplay
{
    private RenderWindow _window;
    private ZXSpectrum _spectrum;
    private Image _img;
    private Sprite _sprite;

    public ZXSpectrumDisplay(ZXSpectrum spectrum)
    {
        _spectrum = spectrum;
    }

    public void Init()
    {
        var mode = new VideoMode(256,192);
        _window = new RenderWindow(mode, "ZX", Styles.Default);
        if (true)
        {
            _window.Size = new Vector2u(256*4, 192*4);
            _window.Position = new Vector2i(100, 100);
        }

        _window.KeyReleased += Window_KeyPressed;
        _window.SetKeyRepeatEnabled(false);
        _window.SetVerticalSyncEnabled(true);
        _window.SetFramerateLimit(25);
    }

    private void Window_KeyPressed(object? sender, KeyEventArgs e)
    {
        var window = (Window)sender;
        if (e.Code == Keyboard.Key.Escape)
        {
            window.Close();
        }
        else
        {
            _spectrum.KeyPressed(e.Code);
        }
    }

    public void Run()
    {
        _window.Clear(Color.Black);
        _window.Display();
        _img = new Image(256, 192);
        _sprite = new Sprite(new Texture(_img));
        while (_window.IsOpen)
        {
            _window.DispatchEvents();
            Draw();
            _window.Display();
            Thread.Sleep(40);
        }
        _spectrum.Stop();
    }

    
    private void Draw()
    {

        var img = _img;

        for (ushort i = 16384; i < 16384+6144; i++)
        {
            var b = _spectrum.Memory[i];

            var h = i / 256;
            var l = i % 256;
            var col = l % 32;
            var y02 = h % 8;
            var y35 = l >> 5;
            var y67 = (h & 0b00011000) >> 3;
            var y = (uint)(y67 * 64 + y35 * 8 + y02);
            var x = (uint)col * 8;
            Color c;
            c = (b&0b00000001)>0 ? Color.White : Color.Black; img.SetPixel(x+7,y,c);
            c = (b&0b00000010)>0 ? Color.White : Color.Black; img.SetPixel(x+6,y,c);
            c = (b&0b00000100)>0 ? Color.White : Color.Black; img.SetPixel(x+5,y,c);
            c = (b&0b00001000)>0 ? Color.White : Color.Black; img.SetPixel(x+4,y,c);
            c = (b&0b00010000)>0 ? Color.White : Color.Black; img.SetPixel(x+3,y,c);
            c = (b&0b00100000)>0 ? Color.White : Color.Black; img.SetPixel(x+2,y,c);
            c = (b&0b01000000)>0 ? Color.White : Color.Black; img.SetPixel(x+1,y,c);
            c = (b&0b10000000)>0 ? Color.White : Color.Black; img.SetPixel(x+0,y,c);
        }
        _sprite.Texture.Update(img);
        _window.Draw(_sprite);
    }
}