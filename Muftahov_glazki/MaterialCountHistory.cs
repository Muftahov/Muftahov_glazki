//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Muftahov_glazki
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialCountHistory
    {
        public int ID { get; set; }
        public int MaterialID { get; set; }
        public System.DateTime ChangeDate { get; set; }
        public double CountValue { get; set; }
    
        public virtual Material Material { get; set; }
    }
}
