using System.Collections.Generic;

namespace MaReB.Data
{
    public class RoleData
    {
        public static List<string> AppRoles { get; set; } = new List<string>
                                                            {
                                                                "Administrador",
                                                                "Editor",
                                                                "Invitado"
                                                            };
    }
}
