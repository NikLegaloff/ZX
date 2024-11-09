namespace ZX.Console.Code.Commands;

public class OUT_N_A : Cmd
{
    public override byte[] Range => [0b11_010_011];

    public override void Execute(Z80 cpu)
    {
        cpu.Bus.Set((ushort)(256* cpu.Reg.A +  ReadByte(cpu)),cpu.Reg.A);
    }
    public override Cmd Init(byte shift) => new OUT_N_A(){Ticks = 11};
    public override string ToString() => "OUT (N), A";

}