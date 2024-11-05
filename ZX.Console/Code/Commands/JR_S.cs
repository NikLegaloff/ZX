namespace ZX.Console.Code.Commands;


public class JR_S : Cmd
{
    public JR_S() => Ticks = 12;

    public override byte[] Range => [0b00011000];

    public override void Execute(Z80 cpu)
    {
        var shift = (sbyte)ReadByte(cpu);
        cpu.Reg.PC = (ushort)(cpu.Reg.PC + shift);
    }
    public override string ToString() => $"JR s";

    public override Cmd Init(byte shift) => new JR_S();

}