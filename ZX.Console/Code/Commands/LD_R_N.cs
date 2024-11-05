namespace ZX.Console.Code.Commands;

public class LD_R_N : Cmd{
    private Reg8Code _code;

    public override byte[] Range => [
        0b00_000_110,
        0b00_001_110,
        0b00_010_110,
        0b00_011_110,
        0b00_100_110,
        0b00_101_110,
        0b00_110_110,
        0b00_111_110,
    ];
    public override void Execute(Z80 cpu)
    {
        var val = ReadByte(cpu);
        Set(cpu, _code, val);
    }

    public override Cmd Init(byte shift) => new LD_R_N{Ticks = 7,_code = (Reg8Code)shift};
    public override string ToString() => "LD " + _code + ", N";

}