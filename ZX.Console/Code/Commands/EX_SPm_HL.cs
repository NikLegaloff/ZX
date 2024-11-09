namespace ZX.Console.Code.Commands;

public class EX_SPm_HL : Cmd
{
    public override byte[] Range => [0b11_100_011];

    public override void Execute(Z80 cpu)
    {
        var newHL = Pop16(cpu);
        Push16(cpu, cpu.Reg.HL);
        cpu.Reg.HL=newHL;
    }
    public override Cmd Init(byte shift) => new EX_SPm_HL { Ticks = 19 };
    public override string ToString() => "EX (SP), HL";

}
public class EX_DE_HL : Cmd
{
    public override byte[] Range => [0b11_101_011];

    public override void Execute(Z80 cpu)
    {
        (cpu.Reg.DE, cpu.Reg.HL) = (cpu.Reg.HL, cpu.Reg.DE);
    }
    public override Cmd Init(byte shift) => new EX_SPm_HL { Ticks = 4 };
    public override string ToString() => "EX DE,HL";

}