namespace ZX.Console.Code.Commands;

public enum ShortConditionCode
{
    NZ,Z,NC,C
}

public class JR_S : Cmd
{
    public JR_S() => Ticks = 12;

    public override byte[] Range => [0b00011000];

    public override void Execute(Z80 cpu, byte cmd)
    {
        var shift = (sbyte)ReadByte(cpu);
        cpu.Reg.PC = (ushort)(cpu.Reg.PC + shift);
    }
}