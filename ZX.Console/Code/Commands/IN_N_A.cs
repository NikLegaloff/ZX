namespace ZX.Console.Code.Commands;

public class IN_N_A : Cmd
{
    public override byte[] Range => [0b11_011_011];

    public override void Execute(Z80 cpu)
    {
        cpu.Reg.A = cpu.Bus.Get((ushort)(cpu.Reg.A*256 + ReadByte(cpu)));
    }
    public override Cmd Init(byte shift) => new IN_N_A{Ticks = 11};
    public override string ToString() => "IN A, (N)";

}