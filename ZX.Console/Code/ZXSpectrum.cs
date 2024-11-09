using SFML.Window;
using ZX.Console.Code.Commands;

namespace ZX.Console.Code;

public class ZXSpectrum
{
    private readonly Z80 _z80;
    private Memory _memory;
    private readonly bool _isDebug;
    private ZXSpectrumDisplay _display;
    private Z80Bus _bus;

    public ZXSpectrum(Memory memory, Z80Bus bus, int freq, bool isDebug=false)
    {
        _bus=bus;
        _z80 = new Z80(memory,bus, freq, isDebug);
        _memory = memory;
        _isDebug = isDebug;
    }

    public Z80 Z80 => _z80;
    public Z80Bus Bus=> _bus;

    public Memory Memory => _memory;

    public void InitAndStart(bool showDisplay=true)
    {

        _z80.Init();
        
        if (showDisplay)
        {
            _display = new ZXSpectrumDisplay(this);
            _display.Init();
            new Thread(Run).Start();
            _display.Run();
        }
        else Run();
    }

    public void Int(byte num)
    {
        _int = num;
    }

    private byte? _int = null;
    private bool _running=true;

    
    public void Run()
    {
        do
        {
            try
            {
                _z80.Tick();
            }
            catch (HaltException)
            {
                System.Console.WriteLine("HALT");
                do
                {
                    Thread.Sleep(TimeSpan.FromMicroseconds(1));
                } while (_int == null);
                System.Console.WriteLine("RESUMED");
            }

        } while (_running);
    }

    public void Stop()
    {
        _running = false;
    }

    public void KeyPressed(Keyboard.Key сode)
    {
        
    }
}