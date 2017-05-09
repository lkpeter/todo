using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TODO.Models
{
    public class EntryViewModel
    {
        public EntryViewModel() {  }

        public EntryViewModel(string title, string desc, DateTime dt)
        {
            Title = title;
            Desc = desc;
            ExpireDate = dt;
        }

        public EntryViewModel(int id,string title, string desc, DateTime? created,DateTime expire)
        {
            Id = id;
            Title = title;
            Desc = desc;
            Created = created;
            ExpireDate = expire;
        }

        public EntryViewModel(int id, string title, string desc, DateTime? created, DateTime expired, DateTime? doneDate)
        {
            Id = id;
            Title = title;
            Desc = desc;
            Created = created;
            DoneDate = doneDate;
            Desc = desc;
            ExpireDate = expired;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Member { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Owner { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime? Created { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime? DoneDate { get; set; }

        private IEnumerable<SelectListItem> members;
        public IEnumerable<SelectListItem> Members
        {
            get
            {
                return members.Select(l => new SelectListItem { Selected = (l.Value == Member), Text = l.Text, Value = l.Value });
            }
            set
            {
                members = value;
            }
        }

        [Display(Name = "Címe:")]
        [Required(ErrorMessage = "A cím megadása kötelező.")]
        [StringLength(256, ErrorMessage = "A cím maximum 256 karakter lehet.")]
        public String Title { get; set; }

        [Display(Name = "Leírás:")]
        [StringLength(1024, ErrorMessage = "A leírás maximum 1024 karakter lehet.")]
        [DataType(DataType.MultilineText)]
        public String Desc { get; set; }

        [Display(Name = "Határidő:")]
        [Required(ErrorMessage = "Határidő megadása kötelező.")]
        [DataType(DataType.DateTime)]
        public DateTime ExpireDate { get; set; }
    }
}