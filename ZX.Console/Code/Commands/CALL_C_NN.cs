namespace ZX.Console.Code.Commands;

public class CALL_C_NN : Cmd
{
    public override byte[] Range => [
        0b_11_000_100,
        0b_11_001_100,
        0b_11_010_100,
        0b_11_011_100,
        0b_11_100_100,
        0b_11_101_100,
        0b_11_110_100,
        0b_11_111_100,
    ];
    private FullConditionCode _code;

    public override void Execute(Z80 cpu)
    {
        var dest = ReadShort(cpu);
        if (IsJump(cpu, _code))
        {
            Push16(cpu, (ushort)(cpu.Reg.PC+3));
            cpu.Reg.PC = dest;
            Ticks = 17;
        } else Ticks = 10;
    }

    public override Cmd Init(byte shift) => new CALL_C_NN() { _code = (FullConditionCode)shift,Ticks = 10};
    public override string ToString() => "CALL " + _code + ", NN";
}