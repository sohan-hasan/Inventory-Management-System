using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Zupree.Models
{
    public class ProductReportModel
    {
        [Display(Name = "Product Id")]
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "You must provide a Product Name")]
        [DataType(DataType.Text)]
        public string ProductName { get; set; }

        [Display(Name = "Purchase Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Require Purchase Date")]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please Select a Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Please Select a Supplier")]
        public int SupplierId { get; set; }

        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Require Quantity")]
        [Range(1, 100, ErrorMessage = "Enter Number between 1 to 100")]
        [DataType(DataType.Text)]
        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "Require Unit Price")]
        [DataType(DataType.Text)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "Please provide a Manufacturer's Suggested Retail Price")]
        public decimal MSRP { get; set; }

        [Display(Name = "Product Image")]
        public string ImageName { get; set; }

        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}