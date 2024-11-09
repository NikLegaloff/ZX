namespace ZX.Console.Code.Commands;

public class JP_NN : Cmd
{
    public override byte[] Range => [0b_11_000_011];
    

    public override void Execute(Z80 cpu)
    {
        cpu.Reg.PC = ReadShort(cpu);
    }

    public override Cmd Init(byte shift) => new JP_NN() {  Ticks = 10 };
    public override string ToString() => "JP  NN";
}