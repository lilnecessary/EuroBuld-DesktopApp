//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EuroBuld.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditLog
    {
        public int ID_Audit { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserType { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public Nullable<System.DateTime> ActionTime { get; set; }
    }
}
