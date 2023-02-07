using RPOnline.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPOnlineCore
{
    public class Character
    {
        public long BankBalance { get { return EngineAPI.GetCharacterBankBalance(); } set { EngineAPI.SetCharacterBankBalance(value); } }
    }
}
