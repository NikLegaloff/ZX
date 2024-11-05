namespace ZX.Console.Code.Commands;

public enum RotationType
{
    RLCA,RRCA,RLA,RRA
}
public class RLRCA : Cmd
{
    public override byte[] Range => [
        0b00_000_111,
        0b00_001_111,
        0b00_010_111,
        0b00_011_111
    ];

    private RotationType _type;

    public override void Execute(Z80 cpu)
    {
        bool left = _type == RotationType.RLA || _type == RotationType.RLCA;
        bool without_c = _type == RotationType.RLA || _type == RotationType.RRA;
        var a = cpu.Reg.A;
        var fc = cpu.Reg.F.C;
        bool extra;
        if (left)
        {
            extra = (a & 0x80)>0;
            a = (byte)(a << 1);
            if ((!without_c && fc) || (without_c && extra)) a |= 0x01;
        }
        else
        {
            extra = (a & 0x01)>0;
            a = (byte)(a >> 1);
            if ((!without_c && fc) || (without_c && extra)) a |= 0x80;
        }
        
        cpu.Reg.A=a;
        cpu.Reg.F.Set53(a);
        cpu.Reg.F.C=extra;
        cpu.Reg.F.H=false;
        cpu.Reg.F.N=false;
    }

    public override Cmd Init(byte shift) => new RLRCA(){_type = (RotationType)shift};

    public override string ToString() => _type.ToString();

}