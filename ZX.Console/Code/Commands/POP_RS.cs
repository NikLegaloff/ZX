using System.Security.Cryptography;

namespace ZX.Console.Code.Commands;
public enum StackRegisters{BC, DE, HL,AF}
public class POP_RS : Cmd
{
    private StackRegisters _code;
    public override byte[] Range => [
    0b11_000_001,
    0b11_010_001,
    0b11_100_001,
    0b11_110_001
    ];
    public override void Execute(Z80 cpu)
    {
        Set(cpu,_code, POP16(cpu)); 
    }

    public override Cmd Init(byte shift) => new POP_RS { _code = (StackRegisters)shift,Ticks = 10};
    public override string ToString() => "POP " + _code;

}