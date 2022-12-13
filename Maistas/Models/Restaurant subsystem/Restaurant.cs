using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SendGrid.Helpers.Mail;

public class Restaurant : IEntityTypeConfiguration<Restaurant>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PhoneNumber { get; set; }
    public string Website { get; set; }
    public string WorkTime { get; set; }
    public double MinimumOrderPrice { get; set; }
    public MaistasUser User { get; set; }
    public List<Dish> Dishes { get; set; }
    [Required]
    public int UserId { get; set; }
    
    [NotMapped]
    public IList<SelectListItem> AvailableUsers { get; set; }

    public Restaurant()
    {
        this.Dishes = new List<Dish>();
    }
    
    public async Task LoadAvailableDropdowns(FoodDbContext context, UserManager<MaistasUser> userManager)
    {
        //  var users = await context.MaistasUser
        //    .Where(x => x.Role != "Restaurant")
        //  .ToListAsync();

        //var users = await context.MaistasUser.Where(x => userManager.IsInRoleAsync(x,"user").Result == true)
        //.ToListAsync();
        /*var users = await context.MaistasUser.ToListAsync();

        //RoleManager<IdentityRole> roleManager;
        AvailableUsers = users.Select(x =>
        {
           
                return new SelectListItem()
                {

                    Value = Convert.ToString(x.Id),
                    Text = x.UserName
                };
        })
            .ToList();*/
        var users = await context.MaistasUser.ToListAsync();
        var query =
            from user in context.MaistasUser
            join userRole in context.UserRoles on user.Id equals userRole.UserId
            where (userRole.RoleId == 2)
            select new SelectListItem
            {
                Value = Convert.ToString(user.Id),
                Text = user.UserName
            };

        AvailableUsers = query.ToList();

}
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(255);
        builder.Property(x => x.PhoneNumber).HasMaxLength(12);
        builder.Property(x => x.Website).HasMaxLength(255);
        builder.HasMany(x => x.Dishes).WithOne(x => x.Restaurant);
        builder.HasOne(x => x.User).WithOne(x => x.Restaurant);
    }
}