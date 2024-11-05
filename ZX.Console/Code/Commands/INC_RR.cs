namespace ZX.Console.Code.Commands;

public class INC_RR : Cmd
{
    private Reg16Code _code;
    public override byte[] Range => [0b00_000_011,0b00_010_011,0b00_100_011,0b00_110_011];
    public override void Execute(Z80 cpu)
    {
        var val = Get(cpu, _code);
        val++;
        Set(cpu, _code, val);
    }

    public override Cmd Init(byte shift) => new INC_RR { _code = (Reg16Code)shift,Ticks = 6};
    public override string ToString() => "INC " + _code;

}
public class DEC_RR : Cmd
{
    private Reg16Code _code;
    public override byte[] Range => [0b00_000_011,0b00_010_011,0b00_100_011,0b00_110_011];
    public override void Execute(Z80 cpu)
    {
        var val = Get(cpu, _code);
        val--;
        Set(cpu, _code, val);
    }

    public override Cmd Init(byte shift) => new DEC_RR { _code = (Reg16Code)shift,Ticks = 6};
    public override string ToString() => "DEC " + _code;

}