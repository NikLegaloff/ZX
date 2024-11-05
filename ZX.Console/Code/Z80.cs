using ZX.Console.Code.Commands;
// ReSharper disable InconsistentNaming

namespace ZX.Console.Code;

public class ZXSpectrum
{
    private readonly Z80 _z80;
    private Memory _memory;
    private readonly bool _isDebug;

    public ZXSpectrum(Memory memory, int freq, bool isDebug=false)
    {
        _z80 = new Z80(memory, freq, isDebug);
        _memory = memory;
        _isDebug = isDebug;
    }

    public void Init()
    {
        _z80.Init();
    }

    public void Int(byte num)
    {
        _int = num;
    }

    private byte? _int = null;
    

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

        } while (true);
    }
}
public class Z80
{
    public readonly Registers Reg =new ();
    public readonly Memory Memory;

    private readonly Dictionary<byte,Cmd> _commands = new();
    private readonly Dictionary<byte,Cmd> _commandsDD = new();
    private readonly Dictionary<byte,Cmd> _commandsFD = new();
    private readonly Dictionary<byte,Cmd> _commandsED = new();
    private readonly Dictionary<byte,Cmd> _commandsCB = new();
    private readonly Dictionary<byte,Cmd> _commandsDDCB = new();

    private readonly int _freq;
    private readonly bool _isDebug;

    public Z80(Memory memory, int freq, bool isDebug=false)
    {
        Memory=memory;
        _freq=freq;
        _isDebug = isDebug;
    }

    public void Init()
    {
        Init(_commands, new Nop());
        Init(_commands, new Ex_Af_Afp());
        Init(_commands, new DJNZ());
        Init(_commands, new JR_S());
        Init(_commands, new JR_CC_S());
        Init(_commands, new LD_RR_NN());
        Init(_commands, new ADD_HL_RR());
        Init(_commands, new Halt());
    }

    private void Init(Dictionary<byte, Cmd> cmds, Cmd cmd)
    {
        byte i = 0;
        foreach (var b in cmd.Range) cmds.Add(b, cmd.Init(i++));
    }

    public void Tick()
    {
        var cmdCode = Memory[Reg.PC++];
        Cmd cmd;
        if (cmdCode == 0xDD)
        {
            cmdCode = Memory[Reg.PC++];
            if (cmdCode == 0xCB)
            {
                cmdCode = Memory[Reg.PC++];
                cmd = _commandsDDCB[cmdCode];
            } else cmd = _commandsDD[cmdCode];
        }
        else if (cmdCode == 0xFD)
        {
            cmdCode = Memory[Reg.PC++];
            cmd = _commandsFD[cmdCode];
        }
        else if (cmdCode == 0xED)
        {
            cmdCode = Memory[Reg.PC++];
            cmd = _commandsED[cmdCode];
        }
        else if (cmdCode == 0xCB)
        {
            cmdCode = Memory[Reg.PC++];
            cmd = _commandsCB[cmdCode];
        }
        else cmd = _commands[cmdCode];

        if (_isDebug)
        {
            System.Console.Clear();
            System.Console.WriteLine(cmd.ToString());
            System.Console.WriteLine(Reg.ToString());
        }
        cmd.Execute(this);
        
        double sleep= cmd.Ticks*1_000_000.0 /_freq;
        Thread.Sleep(TimeSpan.FromMicroseconds(sleep));
    }
}