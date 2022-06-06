using Microsoft.AspNetCore.Authorization;

namespace Wordle.Api.Identity;
public static class Policies
{
    public const string RandomAdmin = "RandomAdmin";
    public const string MotuWordAccess = "MotuWordAccess";

    public static void RandomAdminPolicy(AuthorizationPolicyBuilder policy)
    {
        policy.RequireRole(Roles.Admin);
        policy.RequireAssertion(context =>
        {
            var random = context.User.Claims.FirstOrDefault(c => c.Type == Claims.Random);
            if (Double.TryParse(random?.Value, out double result))
            {
                return result > 0.5;
            }
            return false;
        });
    }

    public static void MotuWordAccessPolicy(AuthorizationPolicyBuilder policy)
    {
        policy.RequireRole(Roles.MasterOfTheUniverse);

        policy.RequireAssertion(context =>
        {
            var dob = context.User.Claims.FirstOrDefault(x => x.Type == Claims.Dob);
            if (DateTime.TryParse(dob?.Value, out DateTime birthDate))
            {
                DateTime maturity = birthDate.AddYears(21);
                return (maturity < DateTime.UtcNow);
            }
            return false;

        });
    }
}