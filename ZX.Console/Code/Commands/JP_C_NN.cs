namespace ZX.Console.Code.Commands;

public class JP_C_NN : Cmd
{
    public override byte[] Range => [
        0b_11_000_010,
        0b_11_001_010,
        0b_11_010_010,
        0b_11_011_010,
        0b_11_100_010,
        0b_11_101_010,
        0b_11_110_010,
        0b_11_111_010,
    ];
    private FullConditionCode _code;

    public override void Execute(Z80 cpu)
    {
        var dest = ReadShort(cpu);
        if (IsJump(cpu, _code)) cpu.Reg.PC = dest;
    }

    public override Cmd Init(byte shift) => new JP_C_NN() { _code = (FullConditionCode)shift,Ticks = 10};
    public override string ToString() => "JP " + _code + ", NN";
}