using System.Reflection.Metadata.Ecma335;

namespace ZX.Console.Code.Commands;

public class Ex_Af_Afp : Cmd
{
    public override byte[] Range => [0b00001000];
    public override void Execute(Z80 cpu)
    {
        var a = cpu.Reg.A;
        var f = cpu.Reg.F;
        cpu.Reg.A = cpu.Reg.Alt.A;
        cpu.Reg.F = cpu.Reg.Alt.F;
        cpu.Reg.Alt.A = a;
        cpu.Reg.Alt.F = f;
    }

    public override string ToString() => "EX AF,AF'";

    public override Cmd Init(byte shift) => new Ex_Af_Afp();
}