//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoCatalog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class CarModels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarModels()
        {
            this.Autos = new HashSet<Autos>();
        }
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int MakeID { get; set; }
        [Display(Name = "Модель")]
        public string CarModel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Autos> Autos { get; set; }
        public virtual Makes Makes { get; set; }
    }
}
