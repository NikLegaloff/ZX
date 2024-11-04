using ZX.Console.Code.Commands;
// ReSharper disable InconsistentNaming

namespace ZX.Console.Code;

public class ZXSpectrum
{
    private Z80 _z80;
    private Memory _memory;

    public ZXSpectrum(Memory memory, int freq)
    {
        _z80 = new Z80(memory, freq);
        _memory = memory;
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
    

    public void Run(bool debudMode=false)
    {
        do
        {
            try
            {
                _z80.Tick();
            }
            catch (HALTException)
            {
                System.Console.WriteLine("HALT");
                do
                {
                    Thread.Sleep(TimeSpan.FromMicroseconds(1));
                } while (_int == null);
                System.Console.WriteLine("RESUMED");
            }

            if (debudMode)
            {
                System.Console.Clear();
                System.Console.WriteLine(_z80.Reg.ToString());
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

    private  int _freq;

    public Z80(Memory memory, int freq)
    {
        Memory=memory;
        _freq=freq;
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
        Init(_commands, new HALT());
    }

    private void Init(Dictionary<byte, Cmd> cmds, Cmd cmd) { foreach (var b in cmd.Range) cmds.Add(b, cmd); }

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

        cmd.Execute(this,cmdCode);
        
        double sleep= cmd.Ticks*1_000_000.0 /_freq;
        Thread.Sleep(TimeSpan.FromMicroseconds(sleep));
    }
}