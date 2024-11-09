namespace ZX.Console.Code.Commands;

public class DI : Cmd
{
    public override byte[] Range => [0b11_111_011];

    public override void Execute(Z80 cpu)
    {
        cpu.Reg.F.Iff1 = false;
        cpu.Reg.F.Iff2 = false;
    }
    public override Cmd Init(byte shift) => new DI( );
    public override string ToString() => "DI";

}