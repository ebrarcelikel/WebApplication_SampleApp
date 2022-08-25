using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{

    [Required, StringLength(30), Display(Name = "Username")]

    public string Username { get; set; }
    [Required, StringLength(16, MinimumLength = 6), Display(Name = "Password")]

    public string Password { get; set; }


}