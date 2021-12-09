using Model_Zupree.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model_Zupree
{
    public class SupplierViewModel
    {
        [Display(Name = "Supplier Id")]
        [Key]
        public int SupplierId { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "You must provide a Company Name !")]
        [DataType(DataType.Text)]
        public string CompanyName { get; set; }

        [Display(Name = "Contact Name")]
        [Required(ErrorMessage = "You must provide a Contact Person Name !")]
        [DataType(DataType.Text)]
        public string ContactName { get; set; }

        [Display(Name = "Company Address")]
        [Required(ErrorMessage = "You must provide a Company Address !")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "You must provide a Phone Number !")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Supplier Email")]
        [Required(ErrorMessage = "You must provide Supplier Email Address !")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Supplier Logo")]
        public string ImageName { get; set; }

        [Display(Name = "Supplier Logo")]
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
    public class CategoryViewModel
    {
        [Display(Name = "Catagory Id")]
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "You must provide a Category Name")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }
    }
    public class ProductViewModel
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
        [ValidatePurchaseDate]
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
    public partial class SalesViewModel 
    {
        [Display(Name = "Sales Id")]
        [Key]
        public int SalesNumber { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "You must provide a Customer Name")]
        [DataType(DataType.Text)]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Phone")]
        [Required(ErrorMessage = "You must provide Customer Phone Number")]
        [DataType(DataType.Text)]
        public string CustomerPhone { get; set; }

        [Display(Name = "Supplier Email")]
        [Required(ErrorMessage = "You must provide Customer Email Address !")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Please Select a Product")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Available Quantity")]
        public int AvailableQuantity { get; set; }

        [Display(Name = "Payment Type")]
        [Required(ErrorMessage = "Please Select a Payment Type")]
        public int PaymentId { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }


        [Display(Name = "Sales Unit Price")]
        public decimal SalesUnitPrice { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Require Quantity")]
        [Range(1, 100, ErrorMessage = "Enter a getter than or Equal Avliable Product")]
        [DataType(DataType.Text)]
        public int Quantity { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Product Image")]
        public string ImageName { get; set; }

        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }

    }
    public partial class PaymentViewModel
    {
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }

    }
}
