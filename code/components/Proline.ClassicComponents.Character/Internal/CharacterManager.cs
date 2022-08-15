using Proline.ClassicOnline.CGameLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CGameLogic.Internal
{
    internal static class CharacterManager
    {
        internal static int NextCharacterIndex { get; set; }
        internal static List<PlayerCharacter> Characters { get; set; }
    }
}
