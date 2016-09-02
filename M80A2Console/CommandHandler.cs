using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M80A2Console
{
    public interface CommandHadler
    {
        byte[] handle(byte[] command);
    }
}
