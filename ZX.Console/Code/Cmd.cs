namespace ZX.Console.Code;

public abstract class Cmd
{
    public byte Ticks = 4;
    public abstract byte[] Range { get; }
    public abstract void Execute(Z80 cpu, byte cmd);

    protected byte ReadByte(Z80 cpu) => cpu.Memory[cpu.Reg.PC++];
    protected ushort ReadShort(Z80 cpu) => (ushort)(ReadByte(cpu) +  ReadByte(cpu)*256);
    protected byte GetBits45(byte b) => (byte)((b >> 3) % 4);
    protected byte GetBits56(byte b) => (byte)((b >> 4) % 4);
}