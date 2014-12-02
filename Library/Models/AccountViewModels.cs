namespace Library.Models
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

	using Library.Resources;

	public class ExternalLoginConfirmationViewModel
    {
		[Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Translate))]
		[DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[Display(Name = "Email", Prompt = "Email", ResourceType = typeof(Translate))]
		[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,3})$", ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[EmailAddress(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate), ErrorMessage = null)]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
		[Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Translate))]
		[DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[Display(Name = "Email", Prompt = "Email", ResourceType = typeof(Translate))]
		[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,3})$", ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[EmailAddress(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate), ErrorMessage = null)]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
		[Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Translate))]
		[DataType(DataType.EmailAddress, ErrorMessageResourceName="EmailError", ErrorMessageResourceType = typeof(Translate))]
        [Display(Name = "Email", Prompt = "Email", ResourceType = typeof(Translate))]
		[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,3})$", ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[EmailAddress(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate), ErrorMessage = null)]
        public string Email { get; set; }

		[Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Translate))]
        [DataType(DataType.Password, ErrorMessageResourceName="PasswordError", ErrorMessageResourceType = typeof(Translate))]
        [Display(Name = "Password", Prompt = "Password", ResourceType = typeof(Translate))]
        public string Password { get; set; }

		//[DisplayName("Remember me?")]
		[Display(Name = "RememberMe", ResourceType = typeof(Translate))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
		[Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Translate))]
		[DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[Display(Name = "Email", Prompt = "Email", ResourceType = typeof(Translate))]
		[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,3})$", ErrorMessageResourceName = "LoginError", ErrorMessageResourceType = typeof(Translate))]
		[EmailAddress(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate), ErrorMessage = null)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Translate))]
		[StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "PasswordError", ErrorMessageResourceType = typeof(Translate))]
		[DataType(DataType.Password, ErrorMessageResourceName = "PasswordError", ErrorMessageResourceType = typeof(Translate))]
		[Display(Name = "Password", Prompt = "Password", ResourceType = typeof(Translate))]
        public string Password { get; set; }

		[DataType(DataType.Password, ErrorMessageResourceName = "PasswordError", ErrorMessageResourceType = typeof(Translate))]
		[Display(Name = "ConfirmPassword", Prompt = "ConfirmPassword", ResourceType = typeof(Translate))]
		[Compare("Password", ErrorMessageResourceName = "ConfirmPasswordError", ErrorMessageResourceType = typeof(Translate))]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
		[Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Translate))]
		[DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[Display(Name = "Email", Prompt = "Email", ResourceType = typeof(Translate))]
		[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,3})$", ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[EmailAddress(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate), ErrorMessage = null)]
        public string Email { get; set; }

		[Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Translate))]			
		[StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "PasswordError", ErrorMessageResourceType = typeof(Translate))]
		[DataType(DataType.Password, ErrorMessageResourceName = "PasswordError", ErrorMessageResourceType = typeof(Translate))]
		[Display(Name = "Password", Prompt = "Password", ResourceType = typeof(Translate))]
        public string Password { get; set; }

		[DataType(DataType.Password, ErrorMessageResourceName = "PasswordError", ErrorMessageResourceType = typeof(Translate))]
		[Display(Name = "ConfirmPassword", Prompt = "ConfirmPassword", ResourceType = typeof(Translate))]
		[Compare("Password", ErrorMessageResourceName = "ConfirmPasswordError", ErrorMessageResourceType = typeof(Translate))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
		[Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Translate))]
		[DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[Display(Name = "Email", Prompt = "Email", ResourceType = typeof(Translate))]
		[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,3})$", ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate))]
		[EmailAddress(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Translate), ErrorMessage = null)]
		public string Email { get; set; }
    }
	/*
	public class RoleViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Email", Prompt = "Email")]
		[RegularExpression("^[a-zA-Z][^\\s\\W\\d]+$", ErrorMessage = "Role should not contain spaces.")]
		public string Name { get; set; }
	}*/
}
