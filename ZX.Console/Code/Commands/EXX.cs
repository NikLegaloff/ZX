namespace ZX.Console.Code.Commands;

public class EXX : Cmd
{
    public override byte[] Range => [0b11_011_001];
    public override void Execute(Z80 cpu)
    {
        var alt = cpu.Reg.Alt;
        cpu.Reg.Alt.BC= cpu.Reg.BC;
        cpu.Reg.Alt.DE= cpu.Reg.DE;
        cpu.Reg.Alt.HL= cpu.Reg.HL;
        cpu.Reg.BC = alt.BC;
        cpu.Reg.DE = alt.DE;
        cpu.Reg.HL = alt.HL;
    }

    public override Cmd Init(byte shift) => new EXX();
    public override string ToString() => "EXX";

}