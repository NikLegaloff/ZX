namespace ZX.Console.Code.Commands;

public class Nop : Cmd
{
    public override byte[] Range => [0];
    public override void Execute(Z80 cpu) { }
    public override Cmd Init(byte shift) => new Nop();
    public override string ToString() => "NOP";

}