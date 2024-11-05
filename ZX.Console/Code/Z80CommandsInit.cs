using ZX.Console.Code.Commands;

namespace ZX.Console.Code;

public class Z80CommandsInit(Dictionary<byte, Cmd> commands)
{
    public void Init()
    {

        var all = new Cmd[]
        {
            new Nop(),
            new Ex_Af_Afp(),
            new DJNZ(),
            new JR_S(),
            new JR_CC_S(),
            new LD_RR_NN(),
            new ADD_HL_RR(),
            new Halt(),
            new LD_BCm_A(), new LD_A_BCm(),
            new LD_DEm_A(), new LD_A_DEm(),
            new LD_NN_HL(),new LD_HL_NN(),
            new LD_NNm_A(),new LD_A_NNm(),
            new INC_RR(),new DEC_RR(),
            new INC_R(),new DEC_R(),

        };
        foreach(var cmd in all) Init(cmd);
    }

    private void Init(Cmd cmd)
    {
        byte i = 0;
        foreach (var b in cmd.Range) commands.Add(b, cmd.Init(i++));
    }

}