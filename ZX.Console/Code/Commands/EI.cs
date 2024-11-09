namespace ZX.Console.Code.Commands;

public class EI : Cmd
{
    public override byte[] Range => [0b11_110_011];

    public override void Execute(Z80 cpu)
    {
        cpu.Reg.F.Iff1 = true;
        cpu.Reg.F.Iff2 = true;
    }
    public override Cmd Init(byte shift) => new EI ();
    public override string ToString() => "EI";

}