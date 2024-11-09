namespace ZX.Console.Code.Commands;

public class CALL_NN : Cmd
{
    public override byte[] Range => [0b_11_001_101];
    
    public override void Execute(Z80 cpu)
    {
        Push16(cpu, (ushort)(cpu.Reg.PC+1));
        cpu.Reg.PC = ReadShort(cpu);
    }

    public override Cmd Init(byte shift) => new CALL_NN { Ticks = 17};
    public override string ToString() => "CALL NN";
}

public enum RST_Address
{
    A0,A8,A16,A24,A32,A40,A48,A56
}

public class RST_P : Cmd
{
    public override byte[] Range => [
        0b_11_000_111,
        0b_11_001_111,
        0b_11_010_111,
        0b_11_011_111,
        0b_11_100_111,
        0b_11_101_111,
        0b_11_110_111,
        0b_11_111_111,
    ];
    private RST_Address _address;
    public override void Execute(Z80 cpu)
    {
        Push16(cpu, (ushort)(cpu.Reg.PC+1));
        cpu.Reg.PC = (ushort)((ushort)_address*8);
    }

    public override Cmd Init(byte shift) => new RST_P { Ticks = 11,_address = (RST_Address)shift};
    public override string ToString() => "RST P";
}