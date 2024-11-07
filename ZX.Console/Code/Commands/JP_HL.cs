namespace ZX.Console.Code.Commands;

public class JP_HL : Cmd
{
    public override byte[] Range => [0b11_101_001];
    public override void Execute(Z80 cpu) => cpu.Reg.PC = cpu.Reg.HL;
    public override Cmd Init(byte shift) => new RET { Ticks = 10 };
    public override string ToString() => "JP HL";
}