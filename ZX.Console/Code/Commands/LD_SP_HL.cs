namespace ZX.Console.Code.Commands;

public class LD_SP_HL : Cmd
{
    public override byte[] Range => [0b11_111_001];
    public override void Execute(Z80 cpu) => cpu.Reg.SP = cpu.Reg.HL;
    public override Cmd Init(byte shift) => new RET { Ticks = 6 };
    public override string ToString() => "LD SP, HL";
}