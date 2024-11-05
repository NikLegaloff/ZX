namespace ZX.Console.Code.Commands;
public enum Reg8Code { B, C, D, E, H, L, HLm, A }

public class INC_R : Cmd
{
    private Reg8Code _code;
    public override byte[] Range => [
        0b00_000_100,0b00_001_100,0b00_010_100,0b00_011_100,
        0b00_100_100,0b00_101_100,0b00_110_100,0b00_111_100,
    ];
    public override void Execute(Z80 cpu)
    {
        var val = Get(cpu, _code);
        Set(cpu, _code, (byte)(val + 1));
        cpu.Reg.F.SetZS53(val);
        cpu.Reg.F.H= (val & 0x0f) == 0x0f;
        cpu.Reg.F.PV= val == 0x7f;

    }

    public override Cmd Init(byte shift) => new INC_R { _code = (Reg8Code)shift};
    public override string ToString() => "INC " + _code;
}

public class DEC_R : Cmd
{
    private Reg8Code _code;
    public override byte[] Range => [
        0b00_000_101, 0b00_001_101, 0b00_010_101, 0b00_011_101,
        0b00_100_101, 0b00_101_101, 0b00_110_101, 0b00_111_101,
    ];
    public override void Execute(Z80 cpu)
    {
        var val = Get(cpu, _code);
        Set(cpu, _code, (byte)(val - 1));
        cpu.Reg.F.SetZS53(val, true);
        cpu.Reg.F.H = (val & 0x0f) == 0x0f;
        cpu.Reg.F.PV = val == 0x80;
    }

    public override Cmd Init(byte shift) => new DEC_R { _code = (Reg8Code)shift};
    public override string ToString() => "DEC " + _code;

}