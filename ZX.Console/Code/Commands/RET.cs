namespace ZX.Console.Code.Commands;

public class RET : Cmd
{
    public override byte[] Range => [0b11_001_001];
    public override void Execute(Z80 cpu)
    {
        cpu.Reg.PC = Pop16(cpu);
    }

    public override Cmd Init(byte shift) => new RET() { Ticks = 10 };
    public override string ToString() => "RET";
}