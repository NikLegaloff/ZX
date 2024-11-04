namespace ZX.Console.Code.Commands;

public class Ex_Af_Afp : Cmd
{
    public override byte[] Range => [0b00001000];
    public override void Execute(Z80 cpu, byte cmd)
    {
        var a = cpu.Reg.A;
        var f = cpu.Reg.F;
        cpu.Reg.A = cpu.Reg.Alt.A;
        cpu.Reg.F = cpu.Reg.Alt.F;
        cpu.Reg.Alt.A = a;
        cpu.Reg.Alt.F = f;
    }
}