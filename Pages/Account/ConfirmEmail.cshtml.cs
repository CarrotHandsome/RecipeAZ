using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RecipeAZ.Models;

public class ConfirmEmailModel : PageModel {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public ConfirmEmailModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> OnGetAsync(string userId, string code) {
        if (userId == null || code == null) {
            return RedirectToPage("/Index");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded) {
            TempData["StatusMessage"] = "Thank you for confirming your email.";
            return RedirectToPage("/Account/Login");
        } else {
            TempData["StatusMessage"] = "Error confirming your email.";
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Error confirming email for user with ID '{userId}': {errors}");
        }

        //return Page();
    }
}
